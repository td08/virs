using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Net;
using System.IO;

namespace EncryptionTest
{
    class BobServer
    {
        static TcpListener server;

        static CngKey bobKey;          // server private key
        static byte[] alicePubKeyBlob; // client public key
        static byte[] bobPubKeyBlob;   // server public key

        static byte[] symetricKey;     // the symetric key that will later be used to transfer data more efficeintly

        static void Main(string[] args)
        {

            // create server private and public keys
            CreateKeys(); 

            // start listening for new connections
            IPAddress ipAddress = IPAddress.Parse("192.168.0.120");
            server = new TcpListener(ipAddress, 54540);
            server.Start();
            var client = server.AcceptTcpClient();

            // once a connection is established open the stream
            var stream = client.GetStream();

            // we need the client public key so we need to instantiate it.
            alicePubKeyBlob = new byte[bobPubKeyBlob.Length];

            // waint until the client send us his public key
            stream.Read(alicePubKeyBlob, 0, alicePubKeyBlob.Length);

            // alicePubKeyBlob should now be the client's public key

            // now let's send this servers public key to the client
            stream.Write(bobPubKeyBlob, 0, bobPubKeyBlob.Length);

            // encrytpedData will be the data that server will recive encrypted from the client with the server's public key
            byte[] encrytpedData = new byte[1024];
            // wait until client sends that data
            stream.Read(encrytpedData, 0, encrytpedData.Length);

            // decrypt the symetric key with the private key of the server
            symetricKey = BobReceivesData(encrytpedData);

            // server and client should know have the same symetric key in order to send data more efficently and securely
            Console.Read();

        }

        private static void CreateKeys()
        {
            //aliceKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256);
            bobKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256);
            //alicePubKeyBlob = aliceKey.Export(CngKeyBlobFormat.EccPublicBlob);
            bobPubKeyBlob = bobKey.Export(CngKeyBlobFormat.EccPublicBlob);
        }

        private static byte[] BobReceivesData(byte[] encryptedData)
        {
            Console.WriteLine("Bob receives encrypted data");
            byte[] rawData = null;

            var aes = new AesCryptoServiceProvider();

            int nBytes = aes.BlockSize >> 3;
            byte[] iv = new byte[nBytes];
            for (int i = 0; i < iv.Length; i++)
                iv[i] = encryptedData[i];

            using (var bobAlgorithm = new ECDiffieHellmanCng(bobKey))
            using (CngKey alicePubKey = CngKey.Import(alicePubKeyBlob,
                  CngKeyBlobFormat.EccPublicBlob))
            {
                byte[] symmKey = bobAlgorithm.DeriveKeyMaterial(alicePubKey);
                Console.WriteLine("Bob creates this symmetric key with " +
                      "Alices public key information: {0}",
                      Convert.ToBase64String(symmKey));

                aes.Key = symmKey;
                aes.IV = iv;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                using (MemoryStream ms = new MemoryStream())
                {
                    var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write);
                    cs.Write(encryptedData, nBytes, encryptedData.Length - nBytes);
                    cs.Close();

                    rawData = ms.ToArray();

                    Console.WriteLine("Bob decrypts message to: {0}",
                          Encoding.UTF8.GetString(rawData));
                }
                aes.Clear();

                return rawData;
            }
        }
    }
}
