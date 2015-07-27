﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

/**********     Client Handler     **********
 * 
 * Class used to manage individual client TCP connections. When a connection is pending
 * within the acceptClients thread, the connection is accepted and a clientObject is created
 * containing the client socket, client ID, network stream, and an active flag. The created
 * clientObject is then passed to a processing thread in a threadpool which handles operations
 * for that client.
 * 
 */

namespace Json_Server_Form
{
    class ClientHandler
    {
        private TcpListener serverSocket;               // main TcpListener used for server socket
        private Json_Server_Form.ServerForm parentForm; // inherit parent serverForm controls
        private List<ClientObject> clientList;          // array list used to hold active clients
        public bool closeHandler;                       // bool flag to signal thread shutdown

        // ClientHandler cnstructor
        public ClientHandler(TcpListener server, object obj)
        {
            this.serverSocket = server;
            this.parentForm = (Json_Server_Form.ServerForm)obj;      // inherit serverForm
            clientList = new List<ClientObject>();
            closeHandler = false;
        }

        public void acceptClients()
        {
            int counter = 1;    // counter variable used for client ID            
            while (true)
            {
                try
                {
                    if (serverSocket.Pending())
                    {
                        ClientObject client = new ClientObject(serverSocket.AcceptTcpClient(), counter);      // create new clientObject with client socket and ID number
                        clientList.Add(client);
                        ThreadPool.QueueUserWorkItem(receiveFromClient, client);
                        parentForm.appendOutputDisplay("Client " + counter++ + " connected!");
                    }

                    if (closeHandler)   // if closeHandler flag is set, break acceptClients loop
                        break;
                }

                catch (Exception e)
                {
                    parentForm.appendOutputDisplay("Error: " + e.Message);
                    break;
                }
                               
            }

            foreach (var c in clientList)
            {
                if (c.isOpen)
                {
                    c.Shutdown();       // close each client socket currently connected                    
                    parentForm.appendOutputDisplay("Client " + c.clientId + " disconnected upon server exit");
                }                    
            }
        }

        private void receiveFromClient(Object threadContext)
        {
            ClientObject client = (ClientObject)threadContext;

            byte[] bytesFrom = new byte[1024];
            string dataFromClient = null;
            bool exceptionOccurred = false;

            while (client.socket.Connected)
            {
                try
                {
                    client.stream.Read(bytesFrom, 0, bytesFrom.Length);
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    parentForm.appendOutputDisplay("Client " + client.clientId + " : " + dataFromClient);
                    Array.Clear(bytesFrom, 0, bytesFrom.Length);
                }
                catch (SocketException s)
                {
                    if (closeHandler)
                        parentForm.appendOutputDisplay(s.Message);
                    client.stream.Dispose();
                    client.Shutdown();
                    exceptionOccurred = true;
                }

                catch (IOException i)
                {
                    if (!closeHandler)      // if client terminated the connection
                    {
                        parentForm.appendOutputDisplay("Error! Client " + client.clientId + " terminated connection!");
                        client.Shutdown();
                    }                        
                    client.stream.Dispose();
                    exceptionOccurred = true;
                }
            }

            if (!exceptionOccurred)     // if no exceptions occurred, exit successfully
            {
                parentForm.appendOutputDisplay("Client " + client.clientId + " disconnected successfully!");
                client.stream.Dispose();
                client.Shutdown();
            }
        }
    }

    class ClientObject
    {
        public TcpClient socket { get; private set; }       // client socket
        public NetworkStream stream { get; private set; }   // client data stream
        public int clientId { get; private set; }           // id number used for tracking clients
        public bool isOpen { get; private set; }            // bool used to know if client is active

        // clientObject constructor
        public ClientObject(TcpClient c, int id)
        {
            this.socket = c;
            this.clientId = id;
            this.stream = c.GetStream();
            this.isOpen = true;
        }

        // method used to manually close client connection
        public void Shutdown()
        {
            this.socket.Close();
            this.isOpen = false;
        }
    }

}