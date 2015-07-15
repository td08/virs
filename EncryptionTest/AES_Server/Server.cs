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
    class Server
    {
        static TcpListener server;

        static CngKey bobKey;          // server private key
        static byte[] alicePubKeyBlob; // client public key
        static byte[] bobPubKeyBlob;   // server public key

        static byte[] symmetricKey;     // the symetric key that will later be used to transfer data more efficeintly

        static void Main(string[] args)
        {

            // create server private and public keys
            CreateKeys(); 

            // start listening for new connections
            //IPAddress ipAddress = IPAddress.Parse("192.168.0.120");
            //server = new TcpListener(ipAddress, 54540);

            IPAddress ipAddress = null;
            foreach (IPAddress ip in Dns.GetHostEntry("localhost").AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip;
                }
            }
            server = new TcpListener(ipAddress, 54540);


            server.Start();
            Console.WriteLine("Starting server...");
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

            // wait for client to send encrypted symmetric key
            byte[] symmKeyBuffer = new byte[64];
            stream.Read(symmKeyBuffer, 0, symmKeyBuffer.Length);

            //decrypt the symmetric key with the private key of the server
            symmetricKey = BobReceivesData(symmKeyBuffer);
            Console.WriteLine("Symmetric Key: " + System.Text.Encoding.UTF8.GetString(symmetricKey));

            // wait for client to send encrypted message length for buffer size
            //byte[] dataLength = new byte[3]; //works
            byte[] dataLength = new byte[4]; //having problems, encoding generating strange characters 
            Console.WriteLine("dataLength: " + System.Text.Encoding.UTF8.GetString(dataLength));

            stream.Read(dataLength, 0, dataLength.Length);
            Console.WriteLine("dataLength: " + System.Text.Encoding.UTF8.GetString(dataLength));
            int messageLength = Int32.Parse(System.Text.Encoding.UTF8.GetString(dataLength));
            //int messageLength = Convert.ToInt32(System.Text.Encoding.UTF8.GetString(dataLength));
            Console.WriteLine("Client sends message length: " + messageLength);

            // encryptedData will be the data that server will recive encrypted from the client with the server's public key
            byte[] encryptedData = new byte[messageLength];

            // wait until client sends that data
            stream.Read(encryptedData, 0, encryptedData.Length);

            // decrypt message from client using symmetric key
            string message = DecryptSymmetric(encryptedData);
            Console.WriteLine("Decrypted message: " + message);

            // server and client should know have the same symetric key in order to send data more efficently and securely
            Console.WriteLine("Finished");
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
            Console.WriteLine("nBytes: " + nBytes + "\nBlock size: " + aes.BlockSize);
            byte[] iv = new byte[nBytes];
            for (int i = 0; i < iv.Length; i++)
                iv[i] = encryptedData[i];

            using (var bobAlgorithm = new ECDiffieHellmanCng(bobKey))
            using (CngKey alicePubKey = CngKey.Import(alicePubKeyBlob,
                  CngKeyBlobFormat.EccPublicBlob))
            {
                byte[] symmKey = bobAlgorithm.DeriveKeyMaterial(alicePubKey);
                //Console.WriteLine("Bob creates this symmetric key with " +
                //      "Alices public key information: {0}",
                //      Convert.ToBase64String(symmKey));

                aes.Key = symmKey;
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                using (MemoryStream ms = new MemoryStream())
                {
                    var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write);
                    cs.Write(encryptedData, nBytes, encryptedData.Length - nBytes);
                    cs.FlushFinalBlock();
                    cs.Close();

                    rawData = ms.ToArray();

                    //Console.WriteLine("Bob decrypts message to: {0}",
                    //      Encoding.UTF8.GetString(rawData));
                }
                aes.Clear();

                return rawData;
            }
        }

        private static string DecryptSymmetric(byte[] cipherText)
        {
            Console.WriteLine("Decrypting using symmetric key...");

            byte[] rawData = null;
            var aes = new AesCryptoServiceProvider();
            byte[] iv = new byte[16];
            for (int i = 0; i < iv.Length; i++)
                iv[i] = cipherText[i];

            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(symmetricKey, iv), CryptoStreamMode.Write))
                {
                    cs.Write(cipherText, 16, cipherText.Length - 16);
                    cs.FlushFinalBlock();
                }

                rawData = ms.ToArray();
            }

            aes.Clear();

            return System.Text.Encoding.UTF8.GetString(rawData);
        }
    }
}
