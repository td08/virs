using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;

namespace ServerTest
{
    class Server
    {
        static string output = "";

        public Server()
        {
        }

        public static void createListener()
        {
            //Socket serverSocket = new Socket()

            
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            foreach (IPAddress ip in ipHostInfo.AddressList)
            {
                Console.WriteLine(ip.ToString());
            }

            Console.Read();


            //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            //IPAddress ipAddress = ipHostInfo.AddressList[0];
            //IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            //String localIp = null;
            //int entry = 0;
            //foreach (IPAddress ip in ipHostInfo.AddressList)
            //{
            //    if (ip.AddressFamily == AddressFamily.InterNetwork)
            //    {
            //        localIp = ip.ToString();
            //    }
            //    entry++;
            //}

            //Console.WriteLine(localIp + "\n" + entry);

            //Console.Read();

            //// Create an instance of the TcpListener class.
            //TcpListener tcpListener = null;
            //IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
            //try
            //{
            //    // Set the listener on the local IP address 
            //    // and specify the port.
            //    tcpListener = new TcpListener(ipAddress, 13);
            //    tcpListener.Start();
            //    output = "Waiting for a connection...";
            //}
            //catch (Exception e)
            //{
            //    output = "Error: " + e.ToString();
            //    System.Console.WriteLine(output);
            //    //MessageBox.Show(output);
            //}
            //while (true)
            //{
            //    // Always use a Sleep call in a while(true) loop 
            //    // to avoid locking up your CPU.
            //    Thread.Sleep(10);
            //    // Create a TCP socket. 
            //    // If you ran this server on the desktop, you could use 
            //    // Socket socket = tcpListener.AcceptSocket() 
            //    // for greater flexibility.
            //    TcpClient tcpClient = tcpListener.AcceptTcpClient();
            //    // Read the data stream from the client. 
            //    byte[] bytes = new byte[256];
            //    NetworkStream stream = tcpClient.GetStream();
            //    stream.Read(bytes, 0, bytes.Length);
            //    SocketHelper helper = new SocketHelper();
            //    helper.processMsg(tcpClient, stream, bytes);                
            //}

        }

        static void Main()
        {
            System.Console.WriteLine("Server Started!");
            createListener();
        }

        
    }
}


            //static void Main(string[] args)
                //{


                //}
