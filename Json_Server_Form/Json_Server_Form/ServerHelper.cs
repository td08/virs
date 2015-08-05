using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

/**********     Server Helper     **********
 * 
 * Class that starts general server operations and creates a clientHandler thread which 
 * manages all connected clients. ServerHelper is called from ServerForm when start button
 * is clicked.
 * 
 */

namespace Json_Server_Form
{
    class ServerHelper
    {
        private Json_Server_Form.ServerForm parentForm;     // class variable for inheriting parent form controls
        private TcpListener serverSocket;       // main TcpListener variable used for the server socket
        private IPHostEntry ipHostInfo;         // contains IP host information used for DNS lookup
        private IPAddress ipAddress;            // contains the IP address for socket use

        public ManualResetEvent stopServer;     // manual reset event used to block socketHandler thread
        public Thread socketHandler;            // thread used to handle starting and stopping serverSocket
        public bool isLocal;                    // bool used for starting server using localhost

        // serverHelper constructor
        public ServerHelper(object obj, bool local)
        {
            this.parentForm = (Json_Server_Form.ServerForm)obj;      // inherit serverForm
            this.isLocal = local;
            stopServer = new ManualResetEvent(false);               // create new reset event
            getIpAddress();                                         // get IP address from DNS resolution
            socketHandler = new Thread(startListening);
            socketHandler.Start();                                  // start listening thread
        }

        // method used to start TcpListener and manages the clientHandler thread
        public void startListening()
        {
            stopServer.Reset();                                 // reset stopServer event to block socketHandler thread

            serverSocket = new TcpListener(ipAddress, 8888);    // create new TcpListener with specified address and port number
            serverSocket.Start();

            parentForm.appendOutputDisplay("Server started with ip: " + ipAddress);

            ClientHandler clienthandler = new ClientHandler(serverSocket, parentForm);  // create new clientHandler instance
            Thread clientHandlerThread = new Thread(clienthandler.acceptClients);
            clientHandlerThread.Start();                                                // start new clientHandler thread
            
            stopServer.WaitOne();        // block socketHandler thread until reset event is set  

            clienthandler.closeHandler = true;  // set closeHandler flag to signal socket shutdown
            parentForm.appendOutputDisplay("Stopping server...");
            Thread.Sleep(3000);         // wait for 3 seconds to allow client sockets to close
            clientHandlerThread.Join(); // join clientHandler thread with socketHandler thread
            serverSocket.Stop();        // stop TcpListener

            parentForm.enableStartButtons();

            parentForm.appendOutputDisplay("Server stopped!");      // finished with server operations. Server must now be manually restarted
        }

        // method sets server IP address using DNS resolution
        private void getIpAddress()
        {
            ipAddress = null;
            string host = Dns.GetHostName();
            if (isLocal)
                host = "localhost";

            ipHostInfo = Dns.GetHostEntry(host);
            foreach (IPAddress ip in ipHostInfo.AddressList)       // loops through addressList to get IPv4 address
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork) // if IPv4 address found, set ipAddress
                {
                    ipAddress = ip;
                    parentForm.setIpLabel(ipAddress.ToString());       
                }
            }
        }        
    }
}
