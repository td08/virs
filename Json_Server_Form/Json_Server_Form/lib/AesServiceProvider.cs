﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

/**********     Client Control Flow     **********
 * 
 * 1. Generate and store public/private key material    (generateKeys)
 * 2. Connect to server
 * 3. Exchange public key information with server   (get localPubKeyBlob, set remotePubKeyBlob)
 * 4. Generate a random symmetric key for data exchange between client/server   (generateSymmetricKey, done in constructor)
 * 5. Generate a random initialization vector for encryption    (getKeyExchangeInfo)
 * 6. Encrypt the symmetric key using server public key information and IV      (getKeyExchangeInfo)
 * 7. Transmit the unencrypted IV and encrypted symmetric key to the server
 * 8. Encrypt message using randomly generated symmetric key    (encryptData)
 * 9. Transmit length of encrypted message to server    (using cipherLength)
 * 10. Transmit encrypted message to server
 * 
 **********     Server Control Flow     **********
 * 
 * 1. Generate and store public/private key material    (generateKeys)
 * 2. Wait for client connection
 * 3. Exchange public key information with client   (get localPubKeyBlob, set remotePubKeyBlob)
 * 4. Receive unencrypted initialization vector and encrypted symmetric key from client     (set symmetricKeyBuffer)
 * 5. Decrypt symmetric key using IV and server private key information     (addSymmetricKey)
 * 6. Receive length of encrypted message from client
 * 7. Receive encrypted message from client
 * 8. Decrypt encrypted message using symmetric key     (decryptData)
 * 
 */

namespace Json_Server_Form
{
    class AesServiceProvider
    {
        AesCryptoServiceProvider aes;    // AesCryptoServiceProviderObject
        CngKey localPvtKey;              // local private key object
        public byte[] localPubKeyBlob { get; private set; }  // local public key blob 
        public byte[] remotePubKeyBlob { get; set; }       // remote public key blob 
        public byte[] symmetricKey { get; private set; }          // symmetric key array
        public byte[] symmetricKeyBuffer { get; set; }      // symmetric key buffer used during addSymmetricKey
        public byte[] cipherLength { get; set; }   // array used to express encrypted message length in bytes

        // AesServiceProvider constructor
        public AesServiceProvider(Boolean isClient)
        {
            aes = new AesCryptoServiceProvider();
            aes.Mode = CipherMode.CBC;          // set cipher mode to cipher block chaining
            aes.Padding = PaddingMode.PKCS7;    // set padding mode to PKCS7
            generateKeys();
            symmetricKeyBuffer = new byte[64];  // instantiate symmetricKeyBuffer for storing keyExchangeInfo
            cipherLength = new byte[4];     // instantiate cipherLength array to hold a 32 bit intger
            if (isClient)                       // if argument is passed as true, perform client context operations
            {
                generateSymmetricKey();
            }
        }

        // generates the public and private key information necessary for the Diffie-Hellman key exchange mechanism
        private void generateKeys()
        {
            localPvtKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256);
            localPubKeyBlob = localPvtKey.Export(CngKeyBlobFormat.EccPublicBlob);
            remotePubKeyBlob = new byte[localPubKeyBlob.Length];    // initializes remotePubKeyBlob
        }

        // generates a random 256 bit key to use for symmetric encrytption after key exchange
        private void generateSymmetricKey()
        {
            symmetricKey = new byte[32];        // initialize symmetricKey buffer to 32 bytes (256 bits)
            Random r = new Random();
            r.NextBytes(symmetricKey);
        }

        // returns keyExchangeInfo containing the unencrypted IV and encrypted symmetric key for the key exchange mechanism
        public byte[] getKeyExchangeInfo()
        {
            try
            {
                using (var keyExchange = new ECDiffieHellmanCng(localPvtKey))
                using (var remotePubKey = CngKey.Import(remotePubKeyBlob, CngKeyBlobFormat.EccPublicBlob))
                {
                    byte[] derivedKey = keyExchange.DeriveKeyMaterial(remotePubKey);
                    aes.Key = derivedKey;           // set aes encryption key to information derived from remote public key
                    aes.GenerateIV();               // generate random initialization vector for encryption

                    using (MemoryStream ms = new MemoryStream())
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                    {
                        ms.Write(aes.IV, 0, aes.IV.Length);     // write unencrypted IV to stream
                        cs.Write(symmetricKey, 0, symmetricKey.Length);     // write encrypted symmetricKey to stream
                        cs.FlushFinalBlock();   // ensures the ms buffer is updated with all data from the cryptostream
                        symmetricKeyBuffer = ms.ToArray();  // store keyExchangeInfo in symmetricKeyBuffer
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(e.StackTrace);
                return symmetricKeyBuffer;     // returns null
            }

            return symmetricKeyBuffer;
        }

        // decrypts and adds the 256-bit symmetric key to be used for all further encrypted communications
        public void addSymmetricKey()
        {
            if (symmetricKeyBuffer != null)
            {
                try
                {
                    int nBytes = aes.BlockSize >> 3;
                    byte[] iv = new byte[nBytes];
                    for (int i = 0; i < iv.Length; i++)
                        iv[i] = symmetricKeyBuffer[i];

                    using (var KeyExchange = new ECDiffieHellmanCng(localPvtKey))
                    using (CngKey remotePubKey = CngKey.Import(remotePubKeyBlob, CngKeyBlobFormat.EccPublicBlob))
                    {
                        byte[] derivedKey = KeyExchange.DeriveKeyMaterial(remotePubKey);
                        aes.Key = derivedKey;
                        aes.IV = iv;

                        using (MemoryStream ms = new MemoryStream())
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                        {
                            cs.Write(symmetricKeyBuffer, nBytes, symmetricKeyBuffer.Length - nBytes);
                            cs.FlushFinalBlock();   // ensures the ms buffer is updated with all data from the cryptostream
                            symmetricKey = ms.ToArray();
                        }
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        // returns cipherText containing data encrypted using the exchanged symmetric key
        public byte[] encryptData(byte[] data)
        {
            byte[] cipherText = null;

            try
            {
                aes.Key = symmetricKey;
                aes.GenerateIV();

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                {
                    ms.Write(aes.IV, 0, aes.IV.Length);
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                    cipherText = ms.ToArray();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(e.StackTrace);
                return cipherText;     // returns null
            }

            Array.Clear(cipherLength, 0, cipherLength.Length);      // clears cipherLength of old data  
            cipherLength = BitConverter.GetBytes(cipherText.Length);    // sets cipherLength with length of cipherText in bytes        
            return cipherText;
        }

        // returns data containing data decrypted using the exchanged symmetric key
        public byte[] decryptData(byte[] cipherText)
        {
            byte[] data = null;
            try
            {
                int nBytes = aes.BlockSize >> 3;
                byte[] iv = new byte[nBytes];
                for (int i = 0; i < iv.Length; i++)
                    iv[i] = cipherText[i];

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(symmetricKey, iv), CryptoStreamMode.Write))
                {
                    cs.Write(cipherText, nBytes, cipherText.Length - nBytes);
                    cs.FlushFinalBlock();
                    data = ms.ToArray();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(e.StackTrace);
                releaseResources();
                return data;     // returns null
            }

            return data;
        }

        // releases resources used by the AES object, zeroes out sensitive info, then marks object for garbage collection
        public void releaseResources()
        {
            aes.Clear();
            aes.Dispose();
        }
    }
}