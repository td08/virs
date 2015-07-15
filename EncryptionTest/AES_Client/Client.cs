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
    class Client
    {
        static CngKey clientPvtKey;  // client private key

        static byte[] clientPubKeyBlob; // client public key
        static byte[] serverPubKeyBlob;   // server public key
        static byte[] symmetricKey;     // the symetric key that we want to give to the server securely

        static void Main(string[] args)
        {
            //create the client private and public keys
            CreateKeys();

            // initialice the server's public key we will need it later
            serverPubKeyBlob = new byte[clientPubKeyBlob.Length];

            // connect to the server and open a stream in order to comunicate with it
            //TcpClient alice = new TcpClient("192.168.0.120", 54540);
            TcpClient alice = new TcpClient("localhost", 54540);

            var stream = alice.GetStream();

            // send to the server the public key (client's public key)
            stream.Write(clientPubKeyBlob, 0, clientPubKeyBlob.Length);

            // now wait to receive the server's public key
            stream.Read(serverPubKeyBlob, 0, serverPubKeyBlob.Length);

            // create a random symetric key
            symmetricKey = new byte[32];
            Random r = new Random();
            r.NextBytes(symmetricKey);
            Console.WriteLine("Symmetric Key: " + System.Text.Encoding.UTF8.GetString(symmetricKey));
                        
            // Encrypt and send the symmetric key to the server using the server's public key
            byte[] symmKeyBuffer = AliceSendsData(symmetricKey);
            stream.Write(symmKeyBuffer, 0, symmKeyBuffer.Length);
            Console.WriteLine("Length of symmKeyBuffer: " + symmKeyBuffer.Length);

            // create message to encrypt
            string test = "Howdy hello hello howdy my name is scout howdy hello! Howdy hello scout! Howdy howdy howdy hello!!!!";
            byte[] message = new byte[System.Text.Encoding.UTF8.GetByteCount(test)];
            message = System.Text.Encoding.UTF8.GetBytes(test);

            //encrypt message using new symmetric key
            byte[] encryptedData = EncryptSymmetric(message);
            Console.WriteLine("Length of encryptedData: " + encryptedData.Length);

            //send to server string containing length of encrypted message in number of bytes (up to 9999)
            //byte[] messageLength = new byte[3]; //works
            byte[] messageLength = new byte[4]; //having problems, encoding generating strange characters 
            messageLength = System.Text.Encoding.UTF8.GetBytes(encryptedData.Length.ToString());
            stream.Write(messageLength, 0, messageLength.Length);
            Console.WriteLine("messageLength: " + System.Text.Encoding.UTF8.GetString(messageLength));

            // once server knows message size, send the encrypted data to the server
            stream.Write(encryptedData, 0, encryptedData.Length);

            // not the server and client should have the same symetric key
            Console.WriteLine("Finished");
            Console.Read();
        }


        private static void CreateKeys()
        {
            clientPvtKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256);
            clientPubKeyBlob = clientPvtKey.Export(CngKeyBlobFormat.EccPublicBlob);
        }

        private static byte[] AliceSendsData(byte[] rawData)
        {

            byte[] encryptedData = null;

            using (var aliceAlgorithm = new ECDiffieHellmanCng(clientPvtKey))
            using (CngKey bobPubKey = CngKey.Import(serverPubKeyBlob,
                  CngKeyBlobFormat.EccPublicBlob))
            {
                byte[] symmKey = aliceAlgorithm.DeriveKeyMaterial(bobPubKey);
                //Console.WriteLine("Alice creates this symmetric key with " +
                //      "Bobs public key information: {0}",
                //      Convert.ToBase64String(symmKey));

                using (var aes = new AesCryptoServiceProvider())
                {
                    aes.Key = symmKey;
                    aes.GenerateIV();
                    aes.Padding = PaddingMode.PKCS7;
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
            //Console.WriteLine("Alice: message is encrypted: {0}",
            //      Convert.ToBase64String(encryptedData)); ;
            //Console.WriteLine();
            return encryptedData;
        }

        private static byte[] EncryptSymmetric(byte[] cipherText)
        {
            Console.WriteLine("Encrypting using symmetric key...");

            byte[] encryptedData = null;
            var aes = new AesCryptoServiceProvider();

            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = symmetricKey;
            aes.GenerateIV();

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                {
                    ms.Write(aes.IV, 0, aes.IV.Length);
                    cs.Write(cipherText, 0, cipherText.Length);
                }

                encryptedData = ms.ToArray();
            }

            aes.Clear();

            Console.WriteLine("Alice: message is encrypted: {0}",
                  Convert.ToBase64String(encryptedData)); ;
            return encryptedData;
        }
    }
}
