﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;

namespace ClientTest
{
    class Client
    {
        public Client()
        {
            //this.MinimizeBox = false;
        }

	    static void Connect(string serverIP, string message)
        {
                string output = "";

                try
                {
                    // Create a TcpClient. 
                    // The client requires a TcpServer that is connected 
                    // to the same address specified by the server and port 
                    // combination.
                    Int32 port = 13;
                    TcpClient client = new TcpClient(serverIP, port);

                    // Translate the passed message into ASCII and store it as a byte array.
                    Byte[] data = new Byte[256];
                    data = System.Text.Encoding.ASCII.GetBytes(message);

                    // Get a client stream for reading and writing. 
                    // Stream stream = client.GetStream();
                    NetworkStream stream = client.GetStream();

                    // Send the message to the connected TcpServer. 
                    stream.Write(data, 0, data.Length);

                    output = "Sent: " + message;
                    Console.WriteLine(output);

                    // Buffer to store the response bytes.
                    data = new Byte[256];

                    // String to store the response ASCII representation.
                    String responseData = String.Empty;

                    // Read the first batch of the TcpServer response bytes.
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    output = "Received: " + responseData;
                    Console.WriteLine(output);

                    // Close everything.
                    stream.Close();
                    client.Close();

                    Console.Read();
                }
                catch (ArgumentNullException e)
                {
                    output = "ArgumentNullException: " + e;
                    Console.WriteLine(output);
                    Console.Read();
                }
                catch (SocketException e)
                {
                    output = "SocketException: " + e.ToString();
                    Console.WriteLine(output);
                    Console.Read();
                }
            }

	    static void Main()
        {
            // In this code example, use a hard-coded 
            // IP address and message. 
            string serverIP = "localhost";
            //string message = "Hello";
            string message = "que?";
            Connect(serverIP, message);
        }
    }
}
