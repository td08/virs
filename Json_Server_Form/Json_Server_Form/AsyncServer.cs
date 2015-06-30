using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Json_Server_Form
{

    // State object for reading client data asynchronously
    public class StateObject
    {
        // Client  socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }

    public class AsyncServer
    {
        //static class properties
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public static Json_Server_Form.ServerForm parent;

        public AsyncServer(object obj)
        {
            parent = (Json_Server_Form.ServerForm)obj;
        }

        public void StartListening(Boolean localHost)
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];
            IPHostEntry ipHostInfo;
            IPAddress ipAddress = null;
            string host = Dns.GetHostName();

            if (localHost)
            {
                host = "localHost";
                parent.appendOutputDisplay("Server started using localhost");
            }

            //get IP host info
            ipHostInfo = Dns.GetHostEntry(host);

            //enumerate and obtain IPv4 IP address
            foreach (IPAddress ip in ipHostInfo.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip;
                    parent.setIpLabel(ipAddress.ToString());
                    parent.appendOutputDisplay("Server started with IP: " + ipAddress);
                }
            }

            //create new IP endpoint on specified port number
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                parent.appendOutputDisplay("Waiting for a new connection...");
                listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);


                ////loop used to accept incoming connections
                //while (true)
                //{
                //    // Set the event to nonsignaled state.
                //    allDone.Reset();

                //    // Start an asynchronous socket to listen for connections.
                //    parent.appendOutputDisplay("Waiting for a new connection...");
                //    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                //    // Wait until a connection is made before continuing.
                //    allDone.WaitOne();
                //}

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            //Console.WriteLine("\nPress ENTER to continue...");
            //Console.Read();

        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);
            parent.appendOutputDisplay("Client connected.");

            // Signal the main thread to continue.
            //allDone.Set();

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
          
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);

        }

        public static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);
            parent.appendOutputDisplay("Bytes received: " + bytesRead);

            if (bytesRead > 0)
            {
                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                parent.appendOutputDisplay("Finished receiving");

                //write received data to file
                writeToFile(state);

            }



            //if (bytesRead > 0)
            //{
            //    // There  might be more data, so store the data received so far.
            //    state.sb.Append(Encoding.ASCII.GetString(
            //        state.buffer, 0, bytesRead));

            //    // Check for end-of-file tag. If it is not there, read 
            //    // more data.
            //    content = state.sb.ToString();
            //    if (content.IndexOf("<EOF>") > -1)
            //    {
            //        // All the data has been read from the 
            //        // client. Display it on the console.
            //        Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
            //            content.Length, content);
            //        // Echo the data back to the client.
            //        //Send(handler, content);
            //    }
            //    else
            //    {
            //        parent.appendOutputDisplay("Still receiving...");
            //        // Not all data received. Get more.
            //        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            //        new AsyncCallback(ReadCallback), state);
            //    }
            //}
            //else
            //{
            //    //Console.WriteLine("Finished receiving.\n" + state.sb.ToString());
            //    parent.appendOutputDisplay("Finished receiving.\n" + state.sb.ToString());

            //    //handler.Shutdown(SocketShutdown.Both);
            //    //handler.Close();
            //}
        }

        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                //wait to close
                Console.Read();

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void writeToFile(StateObject state)
        {
            File.WriteAllText(@"C:\Users\Trevor\Documents\GitHub\virs\Json_Server_Form\Json_Server_Form\test.json", state.sb.ToString());
        }

    }
}
