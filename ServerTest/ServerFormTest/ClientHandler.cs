using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace ServerFormTest
{
    class ClientHandler
    {
        private TcpListener serverSocket;
        private ServerFormTest.ServerForm parentForm;
        private List<ClientObject> clientList;
        public bool closeHandler;       // bool flag to signal thread shutdown

        public ClientHandler(TcpListener server, object obj)
        {
            this.serverSocket = server;
            this.parentForm = (ServerFormTest.ServerForm)obj;      // inherit serverForm
            clientList = new List<ClientObject>();
            closeHandler = false;
        }

        public void acceptClients()
        {
            int counter = 1;
            
            while (true)
            {
                try
                {
                    if (serverSocket.Pending())
                    {
                        ClientObject client = new ClientObject(serverSocket.AcceptTcpClient(), counter);      // create new clientObject with client socket and ID number
                        clientList.Add(client);
                        ThreadPool.QueueUserWorkItem(receiveFromClient, client);
                        parentForm.appendStatusBox("Client " + counter++ + " connected!");
                    }

                    if (closeHandler)   // if closeHandler flag is set, break acceptClients loop
                        break;
                }

                catch (Exception e)
                {
                    parentForm.appendStatusBox("Error: " + e.Message);
                    break;
                }
                               
            }

            foreach (var c in clientList)
            {
                if (c.isOpen)
                {
                    c.Shutdown();       // close each client socket currently connected                    
                    parentForm.appendStatusBox("Client " + c.clientId + " disconnected upon server exit");
                }                    
            }
        }

        private void receiveFromClient(Object threadContext)
        {
            ClientObject client = (ClientObject)threadContext;

            byte[] bytesFrom = new byte[1024];
            string dataFromClient = null;
            NetworkStream clientStream = client.socket.GetStream();
            bool exceptionOccurred = false;

            while (client.socket.Connected)
            {
                try
                {
                    clientStream.Read(bytesFrom, 0, bytesFrom.Length);
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    parentForm.appendStatusBox("Client " + client.clientId + " : " + dataFromClient);
                    Array.Clear(bytesFrom, 0, bytesFrom.Length);
                }
                catch (SocketException s)
                {
                    if (closeHandler)
                        parentForm.appendStatusBox(s.Message);
                    clientStream.Dispose();
                    client.Shutdown();
                    exceptionOccurred = true;
                }

                catch (IOException i)
                {
                    if (!closeHandler)      // if client terminated the connection
                    {
                        parentForm.appendStatusBox("Error! Client " + client.clientId + " terminated connection!");
                        client.Shutdown();
                    }                        
                    clientStream.Dispose();
                    exceptionOccurred = true;
                }
            }

            if (!exceptionOccurred)     // if no exceptions occurred, exit successfully
            {
                parentForm.appendStatusBox("Client " + client.clientId + " disconnected successfully!");
                clientStream.Dispose();
                client.Shutdown();
            }
        }
    }

    class ClientObject
    {
        public TcpClient socket { get; private set; }
        public int clientId { get; private set; }
        public bool isOpen { get; private set; }

        public ClientObject(TcpClient c, int id)
        {
            this.socket = c;
            this.clientId = id;
            this.isOpen = true;
        }

        public void Shutdown()
        {
            this.socket.Close();
            this.isOpen = false;
        }
    }

}
