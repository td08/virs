using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.Net.Sockets;

namespace ClientTest
{
    class Client
    {
        static TcpClient client;

	    static void Connect(string serverIP)
        {
                try
                {
                    TcpClient client = new TcpClient(serverIP, 8888);

                    NetworkStream stream = client.GetStream();

                    // Translate the passed message into ASCII and store it as a byte array.
                    Byte[] data = new Byte[20];
                    string message = "Testing 1, 2, 3...";
                    data = System.Text.Encoding.ASCII.GetBytes(message);

                    // Send the message to the connected TcpServer. 
                    stream.Write(data, 0, data.Length);

                    Console.WriteLine("Sent: " + message);

                    // Buffer to store the response bytes.
                    data = new Byte[256];
                    Console.WriteLine("ReceiveBufferSize: " + client.ReceiveBufferSize);
                    // Read the first batch of the TcpServer response bytes.
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    Console.WriteLine("Received: " + System.Text.Encoding.ASCII.GetString(data));

                    Console.WriteLine("Finished!");
                    Console.ReadLine();
                    // Close everything.
                    stream.Close();
                    client.Close();

                    Console.Read();
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("ArgumentNullException: " + e);
                    Console.Read();
                }
                catch (SocketException e)
                {
                    Console.WriteLine("SocketException: " + e.ToString());
                    Console.Read();
                }
            }

        private static void connectToServer(string serverIP)
        {
            client = new TcpClient(serverIP, 8888);
            Thread sendThread = new Thread(send);
            //Thread receiveThread = new Thread(receive);
            sendThread.Start();
            //receiveThread.Start();
        }

        private static void receive()
        {
            byte[] bytesFrom = new byte[64];
            string dataFromClient = null;

            while ((true))
            {
                try
                {
                    //Console.WriteLine("Receiving...");
                    NetworkStream networkStream = client.GetStream();
                    networkStream.Read(bytesFrom, 0, 20);

                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    Console.WriteLine("From server" + ": " + dataFromClient);
                    Array.Clear(bytesFrom, 0, bytesFrom.Length);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                }
            }
        }

        private static void send()
        {
            Byte[] sendBytes = null;
            NetworkStream networkStream = client.GetStream();

            while ((true))
            {
                try
                {
                    Console.Write(">> ");
                    string serverResponse = Console.ReadLine();
                    sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                    Array.Clear(sendBytes, 0, sendBytes.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                }
            }
        }

	    static void Main()
        {
            Console.WriteLine("Starting client...\nEnter IP Address: ");
            // In this code example, use a hard-coded 
            // IP address and message. 
            string serverIP = Console.ReadLine();
            connectToServer(serverIP);
        }
    }
}
