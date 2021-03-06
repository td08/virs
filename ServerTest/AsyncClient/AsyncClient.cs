﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace AsyncClient
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

    public class AsynchronousClient {
        // The port number for the remote device.
        private const int port = 11000;
        private const string welcome = "Client started. Enter IP Address or 'end' to exit:";
        static public int mycount = 0;
        
        // ManualResetEvent instances signal completion.
        private static ManualResetEvent connectDone = 
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone = 
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = 
            new ManualResetEvent(false);

        // The response from the remote device.
        private static String response = String.Empty;

        private static void StartClient(string myip) {
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
            connectDone.WaitOne();
            
            // Send test data to the remote device.
            switch (mycount)
            {
                case 0:
                    Send(client, "David is a bitch");
                    break;
                case 1:
                    Send(client, "Hello");
                    break;
                
            }
            //Send(client,"David is a bitch");
            sendDone.WaitOne();

            //// Receive the response from the remote device.
            //Receive(client);
            //receiveDone.WaitOne();

            //// Write the response to the console.
            //Console.WriteLine("Response received : {0}", response);

            //perform shutdown
            shutdownClient(client);            
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

            Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());

            // Signal main thread that the connection has been made.
            connectDone.Set();
        }         
        catch (Exception e) 
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void Receive(Socket client) {
    try {
        // Create the state object.
        StateObject state = new StateObject();
        state.workSocket = client;

        // Begin receiving the data from the remote device.
        Console.WriteLine("Begin receiving from server...");
        client.BeginReceive( state.buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReceiveCallback), state);
    } catch (Exception e) {
        Console.WriteLine(e.ToString());
    }
    }

    private static void ReceiveCallback( IAsyncResult ar ) {
    try {
        // Retrieve the state object and the client socket 
        // from the asynchronous state object.
        StateObject state = (StateObject) ar.AsyncState;
        Socket client = state.workSocket;

            // Read data from the remote device.
            int bytesRead = client.EndReceive(ar);

            if (bytesRead > 0) {

                Console.WriteLine("Received some data...");
                // There might be more data, so store the data received so far.
            state.sb.Append(Encoding.ASCII.GetString(state.buffer,0,bytesRead));

                // Get the rest of the data.
                //client.BeginReceive(state.buffer,0,StateObject.BufferSize,0, new AsyncCallback(ReceiveCallback), state);

                if (state.sb.Length > 1)
                {
                    response = state.sb.ToString();
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        response.Length, response);
                }
                // Signal that all bytes have been received.
                receiveDone.Set();
            } else {
                // All the data has arrived; put it in response.
                if (state.sb.Length > 1) {
                    response = state.sb.ToString();
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        response.Length, response);
                }
                // Signal that all bytes have been received.
                receiveDone.Set();
            }
        } catch (Exception e) {
            Console.WriteLine(e.ToString());
        }
    }

    private static void Send(Socket client, String data) {
        // Convert the string data to byte data using ASCII encoding.
        byte[] byteData = Encoding.ASCII.GetBytes(data);

        // Begin sending the data to the remote device.
        client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
        mycount++;
    }

    private static void SendCallback(IAsyncResult ar) {
        try {
            // Retrieve the socket from the state object.
            Socket client = (Socket) ar.AsyncState;

            // Complete sending the data to the remote device.
            int bytesSent = client.EndSend(ar);
            Console.WriteLine("Sent {0} bytes to server.", bytesSent);

            // Signal that all bytes have been sent.
            sendDone.Set();
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
    
    public static int Main(String[] args) {
        Console.WriteLine(welcome);
        string myip = Console.ReadLine();

        while (!myip.Equals("exit"))
        {
            StartClient(myip);
            Console.WriteLine("Enter new IP or 'exit' to exit:");
            myip = Console.ReadLine();
        }
        Console.WriteLine("Exiting! Press any key to continue...");
        Console.Read();
        return 0;
    }
  }


}
