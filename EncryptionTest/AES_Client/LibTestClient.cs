using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace AES_Client
{
    class LibTestClient
    {

        static void Main(string[] args)
        {
            // get server address from user
            Console.WriteLine("Starting LibTestClient...");
            Console.WriteLine("Enter IP address of server: ");
            string ip = Console.ReadLine();

            AesServiceProvider myaes = new AesServiceProvider(true); //set to client mode

            TcpClient client = new TcpClient(ip, 54540);

            var stream = client.GetStream();
            
            // send to the server the public key (client's public key)
            stream.Write(myaes.localPubKeyBlob, 0, myaes.localPubKeyBlob.Length);

            // now wait to receive the server's public key
            stream.Read(myaes.remotePubKeyBlob, 0, myaes.remotePubKeyBlob.Length);
                    
            // Encrypt and send the symmetric key to the server using the server's public key
            byte[] symmKeyBuffer = myaes.getKeyExchangeInfo();
            stream.Write(symmKeyBuffer, 0, symmKeyBuffer.Length);
            Console.WriteLine("Length of symmKeyBuffer: " + symmKeyBuffer.Length);

            // create message to encrypt
            Console.WriteLine("Enter message to encrypt: ");
            string test = Console.ReadLine();
            byte[] message = new byte[System.Text.Encoding.UTF8.GetByteCount(test)];
            message = System.Text.Encoding.UTF8.GetBytes(test);

            //encrypt message using new symmetric key
            byte[] encryptedData = myaes.encryptData(message);
            Console.WriteLine("Length of encryptedData: " + encryptedData.Length);

            //send to server int containing length of encrypted message in number of bytes
            byte[] messageLength = new byte[4];
            messageLength = BitConverter.GetBytes(encryptedData.Length);
            stream.Write(messageLength, 0, messageLength.Length);
            Console.WriteLine("messageLength: " + BitConverter.ToInt32(messageLength, 0));

            // once server knows message size, send the encrypted data to the server
            stream.Write(encryptedData, 0, encryptedData.Length);

            myaes.releaseResources();

            // not the server and client should have the same symetric key
            Console.WriteLine("Finished");
            Console.Read();
        }
    }
}
