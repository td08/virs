using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Json_Client_Form
{
    // State object for receiving data from remote device.
    public class StateObject {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 256;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }

    public class AsyncClient {
        private static Json_Client_Form.ApplicationInterface parent;
        private static string sendData;

        // The port number for the remote device.
        private const int port = 11000;
        
        // ManualResetEvent instances signal completion.
        private static ManualResetEvent connectDone = 
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone = 
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = 
            new ManualResetEvent(false);

        // The response from the remote device.

        public AsyncClient(object obj, string data)
        {
            parent = (Json_Client_Form.ApplicationInterface)obj;
            sendData = data;
        }

        public void StartClient(string myip) {
        // Connect to a remote device.
        try {
            // Establish the remote endpoint for the socket.
            //IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
            IPHostEntry ipHostInfo = Dns.GetHostEntry(myip);
            
            IPAddress ipAddress = null;

            foreach (IPAddress ip in ipHostInfo.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    ipAddress = ip;
            }

            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

            // Create a TCP/IP socket.
            Socket client = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect to the remote endpoint.
            client.BeginConnect( remoteEP, new AsyncCallback(ConnectCallback), client);
            //connectDone.WaitOne();
            
            //send data to server
            //Send(client, "hello");
            //sendDone.WaitOne();

            //perform shutdown
            //shutdownClient(client);            
        } 
        catch (Exception e) 
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void ConnectCallback(IAsyncResult ar) {
        try {
            // Retrieve the socket from the state object.
            Socket client = (Socket) ar.AsyncState;

            // Complete the connection.
            client.EndConnect(ar);
            parent.appendOutputDisplay("Socket connected to: " + client.RemoteEndPoint.ToString());

            Send(client, sendData);

            // Signal main thread that the connection has been made.
            //connectDone.Set();
        }         
        catch (Exception e) 
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void Send(Socket client, String data) {
        // Convert the string data to byte data using ASCII encoding.
        byte[] byteData = Encoding.ASCII.GetBytes(data);

        // Begin sending the data to the remote device.
        client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
    }

    private static void SendCallback(IAsyncResult ar) {
        try {
            // Retrieve the socket from the state object.
            Socket client = (Socket) ar.AsyncState;

            // Complete sending the data to the remote device.
            int bytesSent = client.EndSend(ar);
            parent.appendOutputDisplay("Sent " + bytesSent + " bytes to server");

            // Signal that all bytes have been sent.
            //sendDone.Set();
            shutdownClient(client);
        } catch (Exception e) {
            Console.WriteLine(e.ToString());
        }
    }

    private static void shutdownClient(Socket client)
    {
        //reset event states
        connectDone.Reset();
        sendDone.Reset();
        receiveDone.Reset();

        // Release the socket.
        client.Shutdown(SocketShutdown.Both);
        client.Close();
    }
    
    //public static int Main(String[] args) 
    //{
    //    Console.WriteLine(welcome);
    //    string myip = Console.ReadLine();

    //    while (!myip.Equals("exit"))
    //    {
    //        StartClient(myip);
    //        Console.WriteLine("Enter new IP or 'exit' to exit:");
    //        myip = Console.ReadLine();
    //    }
    //    Console.WriteLine("Exiting! Press any key to continue...");
    //    Console.Read();
    //    return 0;
    //}
  }


}
