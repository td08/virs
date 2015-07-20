using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Security.Cryptography;

/**********     Client Control Flow     **********
 * 
 * 1. Generate and store public key material
 * 2. Connect to server
 * 3. Exchange public key information with server
 * 4. Generate a random symmetric key for data exchange between client/server
 * 5. Generate a random initialization vector for encryption
 * 6. Encrypt the symmetric key using server public key information and IV
 * 7. Transmit the unencrypted IV and encrypted symmetric key to the server
 * 8. Encrypt message using randomly generated symmetric key
 * 9. Transmit length of encrypted message to server
 * 10. Transmit encrypted message to server
 * 
 */
 

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

            // get server address from user
            Console.WriteLine("Enter IP address of server: ");
            string ip = Console.ReadLine();

            // connect to the server and open a stream in order to comunicate with it
            //TcpClient alice = new TcpClient("192.168.0.120", 54540);
            TcpClient alice = new TcpClient(ip, 54540);

            var stream = alice.GetStream();

            // send to the server the public key (client's public key)
            stream.Write(clientPubKeyBlob, 0, clientPubKeyBlob.Length);

            // now wait to receive the server's public key
            stream.Read(serverPubKeyBlob, 0, serverPubKeyBlob.Length);

            // create a random 256 bit symmetric key
            symmetricKey = new byte[32];
            Random r = new Random();
            r.NextBytes(symmetricKey);
            Console.WriteLine("Symmetric Key: " + System.Text.Encoding.UTF8.GetString(symmetricKey));
                        
            // Encrypt and send the symmetric key to the server using the server's public key
            byte[] symmKeyBuffer = AliceSendsData(symmetricKey);
            stream.Write(symmKeyBuffer, 0, symmKeyBuffer.Length);
            Console.WriteLine("Length of symmKeyBuffer: " + symmKeyBuffer.Length);

            // create message to encrypt
            Console.WriteLine("Enter message to encrypt: ");
            string test = Console.ReadLine();
            byte[] message = new byte[System.Text.Encoding.UTF8.GetByteCount(test)];
            message = System.Text.Encoding.UTF8.GetBytes(test);

            //encrypt message using new symmetric key
            byte[] encryptedData = EncryptSymmetric(message);
            Console.WriteLine("Length of encryptedData: " + encryptedData.Length);

            //send to server int containing length of encrypted message in number of byte
            byte[] messageLength = new byte[4];    
            messageLength = BitConverter.GetBytes(encryptedData.Length);
            stream.Write(messageLength, 0, messageLength.Length);
            Console.WriteLine("messageLength: " + BitConverter.ToInt32(messageLength, 0));

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

            using (var clientKeyExchange = new ECDiffieHellmanCng(clientPvtKey))
            using (CngKey serverPubKey = CngKey.Import(serverPubKeyBlob,
                  CngKeyBlobFormat.EccPublicBlob))
            {
                byte[] symmKey = clientKeyExchange.DeriveKeyMaterial(serverPubKey);
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

            Console.WriteLine("Encrypted message: {0}",
                  Convert.ToBase64String(encryptedData)); ;
            return encryptedData;
        }
    }
}
