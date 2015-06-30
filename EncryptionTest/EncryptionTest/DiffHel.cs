using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace EncryptionTest
{
    class DiffHel
    {






        private static void CreateKeys()
        {
            //aliceKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256);
            bobKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256);
            //alicePubKeyBlob = aliceKey.Export(CngKeyBlobFormat.EccPublicBlob);
            bobPubKeyBlob = bobKey.Export(CngKeyBlobFormat.EccPublicBlob);
        }

        private static AesCryptoServiceProvider CreateSymKey()
        {
            var aes = new AesCryptoServiceProvider();
            int IVSize = aes.BlockSize / 8;
            byte[] IV = new byte[IVSize];

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
