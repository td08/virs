using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace multiThreadServer
{
    class TcpServer
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start server in localhost mode? (y or n)");
            string localHost = Console.ReadLine();

            IPHostEntry ipHostInfo;
            IPAddress ipAddress = null;
            string host = Dns.GetHostName();

            if (localHost.Equals("y"))
                host = "localhost";

            ipHostInfo = Dns.GetHostEntry(host);
            foreach (IPAddress ip in ipHostInfo.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip;
                    Console.WriteLine("Server started with IP: " + ipAddress);
                }
            }

            TcpListener serverSocket = new TcpListener(ipAddress, 8888);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();
            Console.WriteLine(" >> " + "Server Started");

            counter = 0;
            while (true)
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine(" >> " + "Client No:" + Convert.ToString(counter) + " started!");
                handleClient client = new handleClient();
                client.startClient(clientSocket, Convert.ToString(counter));
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine(" >> " + "exit");
            Console.ReadLine();
        }
    }

    public class handleClient
    {
        TcpClient clientSocket;
        string clNo;
        public void startClient(TcpClient inClientSocket, string clientNo)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clientNo;
            Thread sendThread = new Thread(send);
            Thread receiveThread = new Thread(receive);
            sendThread.Start();
            receiveThread.Start();
        }

        private void send()
        {
            Byte[] sendBytes = null;

            while ((true))
            {
                try
                {
                    NetworkStream networkStream = clientSocket.GetStream();

                    //Console.WriteLine("Sending...");
                    Console.Write(">> ");
                    string serverResponse = Console.ReadLine();
                    sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                }
            }
        }

        private void receive()
        {
            byte[] bytesFrom = new byte[64];
            string dataFromClient = null;

            while ((true))
            {
                try
                {                    
                    //Console.WriteLine("Receiving...");
                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, 20);

                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    Console.WriteLine("From client-" + clNo + ": " + dataFromClient);
                    Array.Clear(bytesFrom, 0, bytesFrom.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                }
            }
        }

        private void doChat()
        {
            int requestCount = 0;
            byte[] bytesFrom = new byte[10025];
            string dataFromClient = null;
            Byte[] sendBytes = null;
            string serverResponse = null;
            string rCount = null;
            requestCount = 0;

            while ((true))
            {
                try
                {
                    Console.WriteLine("Receiving...");
                    NetworkStream networkStream = clientSocket.GetStream();
                    //networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    networkStream.Read(bytesFrom, 0, 20);

                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    Console.WriteLine(" >> " + "From client-" + clNo + ": " + dataFromClient);

                    Console.WriteLine("Sending...");
                    serverResponse = "You are client number: " + clNo;
                    sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                    Console.WriteLine(" >> " + serverResponse);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                }
            }
        }
    } 
}
