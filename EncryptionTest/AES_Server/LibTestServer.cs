using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace AES_Server
{
    class LibTestServer
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Start in localhost mode? (y or n)");
            //string mode = Console.ReadLine();
            string mode = "y";
            string hostName = Dns.GetHostName();
            if (mode.Equals("y"))
                hostName = "localhost";

            IPAddress ipAddress = null;
            foreach (IPAddress ip in Dns.GetHostEntry(hostName).AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip;
                }
            }

            AesServiceProvider myaes = new AesServiceProvider(false); //set to server mode

            TcpListener server = new TcpListener(ipAddress, 54540);
            server.Start();

            Console.WriteLine("Starting LibTestServer with IP address " + ipAddress.ToString());

            var client = server.AcceptTcpClient();

            // once a connection is established open the stream
            var stream = client.GetStream();

            // waint until the client sends public key
            stream.Read(myaes.remotePubKeyBlob, 0, myaes.remotePubKeyBlob.Length);

            // send this servers public key to the client
            stream.Write(myaes.localPubKeyBlob, 0, myaes.localPubKeyBlob.Length);

            // wait for client to send encrypted symmetric key
            stream.Read(myaes.symmetricKeyBuffer, 0, myaes.symmetricKeyBuffer.Length);

            //decrypt the symmetric key with the private key of the server
            myaes.addSymmetricKey();
            Console.WriteLine("Symmetric Key: " + System.Text.Encoding.UTF8.GetString(myaes.symmetricKey));

            // wait for client to send encrypted message length for buffer size
            byte[] dataLength = new byte[4];
            stream.Read(dataLength, 0, dataLength.Length);
            int messageLength = BitConverter.ToInt32(dataLength, 0);
            Console.WriteLine("Client sends message length: " + messageLength);
            
            // encryptedData will be the data that server will recive encrypted from the client with the server's public key
            byte[] encryptedData = new byte[messageLength];

            // wait until client sends that data
            stream.Read(encryptedData, 0, encryptedData.Length);
            Console.WriteLine("Encrypted message: " + Convert.ToBase64String(encryptedData));

            // decrypt message from client using symmetric key
            byte[] decryptedData = myaes.decryptData(encryptedData);
            string message = System.Text.Encoding.UTF8.GetString(decryptedData);
            Console.WriteLine("Decrypted message: " + message);

            // finished
            Console.WriteLine("Finished");
            Console.Read();
        }
    }
}
