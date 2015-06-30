using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Security.Cryptography;

namespace EncryptionTest
{
    class AliceClient
    {
        static CngKey aliceKey;  // client private key

        static byte[] alicePubKeyBlob; // client public key
        static byte[] bobPubKeyBlob;   // server public key
        static byte[] symetricKey;     // the symetric key that we want to give to the server securely

        static void Main(string[] args)
        {
            //create the client private and public keys
            CreateKeys();

            // initialice the server's public key we will need it later
            bobPubKeyBlob = new byte[alicePubKeyBlob.Length];

            // connect to the server and open a stream in order to comunicate with it
            TcpClient alice = new TcpClient("192.168.0.120", 54540);
            var stream = alice.GetStream();

            // send to the server the public key (client's public key)
            stream.Write(alicePubKeyBlob, 0, alicePubKeyBlob.Length);

            // now wait to receive the server's public key
            stream.Read(bobPubKeyBlob, 0, bobPubKeyBlob.Length);

            // create a random symetric key
            symetricKey = new byte[1000];
            Random r = new Random();
            r.NextBytes(symetricKey);

            // Encrypt the symetric key with the server's public key
            byte[] encrytpedData = AliceSendsData(symetricKey);

            // once encrypted send that encrypted data to the server. The only one that is going to be able to unecrypt that will be the server
            stream.Write(encrytpedData, 0, encrytpedData.Length);

            // not the server and client should have the same symetric key
        }


        private static void CreateKeys()
        {
            aliceKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256);
            alicePubKeyBlob = aliceKey.Export(CngKeyBlobFormat.EccPublicBlob);
        }

        private static byte[] AliceSendsData(byte[] rawData)
        {

            byte[] encryptedData = null;

            using (var aliceAlgorithm = new ECDiffieHellmanCng(aliceKey))
            using (CngKey bobPubKey = CngKey.Import(bobPubKeyBlob,
                  CngKeyBlobFormat.EccPublicBlob))
            {
                byte[] symmKey = aliceAlgorithm.DeriveKeyMaterial(bobPubKey);
                Console.WriteLine("Alice creates this symmetric key with " +
                      "Bobs public key information: {0}",
                      Convert.ToBase64String(symmKey));

                using (var aes = new AesCryptoServiceProvider())
                {
                    aes.Key = symmKey;
                    aes.GenerateIV();
                    using (ICryptoTransform encryptor = aes.CreateEncryptor())
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // create CryptoStream and encrypt data to send
                        var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

                        // write initialization vector not encrypted
                        ms.Write(aes.IV, 0, aes.IV.Length);
                        cs.Write(rawData, 0, rawData.Length);
                        cs.Close();
                        encryptedData = ms.ToArray();
                    }
                    aes.Clear();
                }
            }
            Console.WriteLine("Alice: message is encrypted: {0}",
                  Convert.ToBase64String(encryptedData)); ;
            Console.WriteLine();
            return encryptedData;
        }
    }
}
