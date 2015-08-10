using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Virs_Client_Form
{
    class AesClient
    {
        public static void upload(UploadForm parentForm, string ip, string port, string clientData, string path)
        {
            UploadForm parent = parentForm;

            AesServiceProvider asp = new AesServiceProvider(true); //set to client mode

            byte[] wavFileBytes = null;
            byte[] encryptedWavFile = null;
            byte[] encryptedWavFileLength = new byte[4];
            bool wavExists = false;

            parent.appendStatus("Connecting to server...");

            try
            {
                if (File.Exists(Path.Combine(path, "steth.wav")))
                {
                    wavFileBytes = File.ReadAllBytes(Path.Combine(path, "steth.wav"));
                    wavExists = true;
                }

                using (TcpClient client = new TcpClient(ip, Int32.Parse(port)))
                using (var stream = client.GetStream())
                {
                    parent.appendStatus("Connected to server at: " + ip.ToString() + ":" + port.ToString());

                    parent.appendStatus("Encrypting connection...");

                    // send to the server the public key (client's public key)
                    stream.Write(asp.localPubKeyBlob, 0, asp.localPubKeyBlob.Length);

                    // now wait to receive the server's public key
                    stream.Read(asp.remotePubKeyBlob, 0, asp.remotePubKeyBlob.Length);

                    // Encrypt and send the symmetric key to the server using the server's public key
                    byte[] symmKeyBuffer = asp.getKeyExchangeInfo();
                    stream.Write(symmKeyBuffer, 0, symmKeyBuffer.Length);

                    parent.appendStatus("Sending data...");

                    // get data to encrypt
                    string clientDataAsJson = clientData;
                    byte[] data = new byte[System.Text.Encoding.UTF8.GetByteCount(clientDataAsJson)];
                    data = System.Text.Encoding.UTF8.GetBytes(clientDataAsJson);

                    //encrypt message using new symmetric key
                    byte[] encryptedData = asp.encryptData(data);
                    parent.appendStatus("Length of encryptedData: " + encryptedData.Length + " bytes");

                    //send to server int containing length of encrypted message in number of bytes
                    byte[] dataLength = new byte[4];
                    dataLength = BitConverter.GetBytes(encryptedData.Length);
                    stream.Write(dataLength, 0, dataLength.Length);

                    // once server knows message size, send the encrypted data to the server
                    stream.Write(encryptedData, 0, encryptedData.Length);


                    /////// write wav byte array


                    //if (wavExists)
                    //{
                    //    // encrypt wav file
                    //    encryptedWavFile = asp.encryptData(wavFileBytes);
                    //    parent.appendStatus("Length of encryptedWavFile: " + encryptedWavFile.Length + " bytes");
                    //    // get size of encrypted buffer
                    //    encryptedWavFileLength = BitConverter.GetBytes(encryptedWavFile.Length);
                    //    // write size of file buffer to stream
                    //    stream.Write(encryptedWavFileLength, 0, encryptedWavFileLength.Length);
                    //    // write encrypted wav file to stream
                    //    stream.Write(encryptedWavFile, 0, encryptedWavFile.Length);
                    //}

                    //else
                    //{
                    //    // send server encryptedWavFileLength array indicating zero bytes meaning no file will be sent
                    //    encryptedWavFileLength = BitConverter.GetBytes(0);
                    //    stream.Write(encryptedWavFileLength, 0, encryptedWavFileLength.Length);
                    //}

                    asp.releaseResources();
                }
            }

            catch (SocketException s)
            {
                parent.appendStatus(s.Message);
            }

            catch (ObjectDisposedException d)
            {
                parent.appendStatus(d.Message);
            }

            parent.appendStatus("Finished!");
        }
    }
}
