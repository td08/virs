using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerFormTest
{
    class ServerHelper
    {
        private ServerFormTest.ServerForm parentForm;
        private TcpListener serverSocket;
        private IPHostEntry ipHostInfo;
        private IPAddress ipAddress;
        public ManualResetEvent stopServer;
        public Thread socketHandler;

        public ServerHelper(object obj)
        {
            this.parentForm = (ServerFormTest.ServerForm) obj;      // inherit serverForm
            stopServer = new ManualResetEvent(false);

            ipAddress = null;

            string host = Dns.GetHostName();

            //host = "localhost";

            ipHostInfo = Dns.GetHostEntry(host);
            foreach (IPAddress ip in ipHostInfo.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip;
                }
            }

            socketHandler = new Thread(startListening);
            socketHandler.Start();
        }

        public void startListening()
        {
            stopServer.Reset();                                 // reset stopServer event to block socketHandler thread

            serverSocket = new TcpListener(ipAddress, 8888);

            serverSocket.Start();

            parentForm.appendStatusBox("Server started with ip: " + ipAddress);

            ClientHandler clienthandler = new ClientHandler(serverSocket, parentForm);
            Thread clientHandlerThread = new Thread(clienthandler.acceptClients);
            clientHandlerThread.Start();
            stopServer.WaitOne();        // waits for resetEvent to close server socket operations  

            clienthandler.closeHandler = true;
            parentForm.appendStatusBox("Stopping server...");
            Thread.Sleep(3000);         // wait for 3 seconds to allow client sockets to close
            clientHandlerThread.Join();
            serverSocket.Stop();

            parentForm.appendStatusBox("Server stopped!");      // finished with server operations. Server must now be manually restarted
        }

        
    }
}
