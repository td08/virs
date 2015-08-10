using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Security.Cryptography;

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
        private object lockObject = new object();       // object used for the lock statement when writing data to the disk
        public bool closeHandler;                       // bool flag to signal thread shutdown

        // ClientHandler constructor
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
            byte[] encryptedData = null;
            byte[] decryptedData = null;
            byte[] encryptedWavFile = null;
            byte[] decryptedWavFile = null;
            byte[] wavBytes = null;
            string dataFromClient = null;
            bool exceptionOccurred = false;

            performKeyExchange(client);     // get sym key for encrypted communications with client

            try
            {
                encryptedData = receiveEncryptedData(client);    // receive encrypted data from client
                decryptedData = client.aes.decryptData(encryptedData);  // decrypt data using symmetric key
                processData(decryptedData);

                //wavBytes = receiveWavBytes(client);              // receive byte buffer containing length of encrypted wav file if present
                //if (BitConverter.ToInt32(wavBytes, 0) > 0)
                //{
                //    encryptedWavFile = receiveEncryptedWav(client, BitConverter.ToInt32(wavBytes, 0));
                //    decryptedWavFile = client.aes.decryptData(encryptedWavFile);   // decrypt wav file using symmetric key
                //    decryptedData = client.aes.decryptData(encryptedData);  // decrypt data using symmetric key
                //    processDataWithWav(decryptedData, decryptedWavFile);
                //}

                //else
                //{
                //    decryptedData = client.aes.decryptData(encryptedData);  // decrypt data using symmetric key
                //    processData(decryptedData);
                //}

                Array.Clear(decryptedData, 0, decryptedData.Length);
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

            catch (CryptographicException c)
            {
                client.stream.Dispose();
                client.Shutdown();
                exceptionOccurred = true;
            }

            catch (ArgumentNullException n)
            {
                client.stream.Dispose();
                client.Shutdown();
                exceptionOccurred = true;
            }

            if (!exceptionOccurred)     // if no exceptions occurred, exit successfully
            {
                parentForm.appendOutputDisplay("Client " + client.clientId + " disconnected successfully!");
                client.stream.Dispose();
                client.Shutdown();
            }
        }

        // method called to perform symmetric key exchange with client
        private void performKeyExchange(ClientObject c)
        {
            c.stream.Read(c.aes.remotePubKeyBlob, 0, c.aes.remotePubKeyBlob.Length);        // receive client pub key
            c.stream.Write(c.aes.localPubKeyBlob, 0, c.aes.localPubKeyBlob.Length);         // send server pub key
            c.stream.Read(c.aes.symmetricKeyBuffer, 0, c.aes.symmetricKeyBuffer.Length);    // receive encrypted sym key
            c.aes.addSymmetricKey();                                                        // decrypt sym key
        }

        // method called to receive and store encrypted data from client into specified byte array
        private byte[] receiveEncryptedData(ClientObject c)
        {
            Array.Clear(c.aes.cipherLength, 0, c.aes.cipherLength.Length);      // clear cipherLength
            c.stream.Read(c.aes.cipherLength, 0, c.aes.cipherLength.Length);    // receive encrypted data length
            byte[] data = new byte[BitConverter.ToInt32(c.aes.cipherLength, 0)];     // instantiate encrypted data buffer
            c.stream.Read(data, 0, data.Length);                            // receive encrypted data
            return data;
        }

        private byte[] receiveWavBytes(ClientObject c)
        {
            Array.Clear(c.aes.cipherLength, 0, c.aes.cipherLength.Length);      // clear cipherLength
            c.stream.Read(c.aes.cipherLength, 0, c.aes.cipherLength.Length);    // receive encrypted data length
            return c.aes.cipherLength;
        }

        private byte[] receiveEncryptedWav(ClientObject c, int length)
        {
            byte[] data = new byte[length];     // instantiate encrypted data buffer
            c.stream.Read(data, 0, data.Length);                            // receive encrypted data
            return data;
        }

        // method called to operate on received wav file data from client and stores to a local directory
        private void processDataWithWav(byte[] clientDataBytes, byte[] wavDataBytes)
        {
            string dataFromClient = System.Text.Encoding.UTF8.GetString(clientDataBytes);

            Vitals clientData = Jlib.fromJson(dataFromClient);
            string clientPath = Path.Combine(clientData.lastName + "-" + clientData.firstName, clientData.logTime);
            string writePath = Path.Combine(parentForm.settings.path, clientPath);

            try
            {
                lock (lockObject)   // locking while writing data to disk
                {
                    // serialize imported clientData to JSON and write to file in clientSettings path
                    parentForm.appendOutputDisplay("writing data");
                    Jlib.serializeVitalsToJson(writePath, clientData);
                    // build filtered wav audio file using data from clientData.steth
                    File.WriteAllBytes(Path.Combine(writePath, "steth.wav"), wavDataBytes);
                }
            }

            catch (Exception ex)
            {
                parentForm.appendOutputDisplay("Error saving VIRS files received from patient: " + clientData.lastName + "," + clientData.firstName + "\nPlease check path directory in settings and try again!");
            }
        }

        // method called to operate on received data from client and stores to a local directory
        private void processData(byte[] data)
        {
            string dataFromClient = System.Text.Encoding.UTF8.GetString(data);

            Vitals clientData = Jlib.fromJson(dataFromClient);
            string clientPath = Path.Combine(clientData.lastName + "-" + clientData.firstName, clientData.logTime);
            string writePath = Path.Combine(parentForm.settings.path, clientPath);

            try
            {
                lock (lockObject)   // locking while writing data to disk
                {
                    // serialize imported clientData to JSON and write to file in clientSettings path
                    parentForm.appendOutputDisplay("writing data");
                    Jlib.serializeVitalsToJson(writePath, clientData);
                }
            }

            catch (Exception ex)
            {
                parentForm.appendOutputDisplay("Error saving VIRS files received from patient: " + clientData.lastName + "," + clientData.firstName + "\nPlease check path directory in settings and try again!");
            }
        }
    }

    class ClientObject
    {
        public TcpClient socket { get; private set; }       // client socket
        public NetworkStream stream { get; private set; }   // client data stream
        public int clientId { get; private set; }           // id number used for tracking clients
        public bool isOpen { get; private set; }            // bool used to know if client is active
        public AesServiceProvider aes { get; private set; } // AesServiceProvider object for cryptographic operations

        // clientObject constructor
        public ClientObject(TcpClient c, int id)
        {
            this.socket = c;
            this.clientId = id;
            this.stream = c.GetStream();
            this.isOpen = true;
            this.aes = new AesServiceProvider(false);   // create new ASP object with server context
        }

        // method used to manually close client connection
        public void Shutdown()
        {
            this.aes.releaseResources();
            this.socket.Close();
            this.isOpen = false;
        }
    }

}
