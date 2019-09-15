using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.ComponentModel;

namespace Middleware1
{
    class Program
    {
        /// <summary>
        const int myPort = 8082;
        int mySentMsg = 0;
        int numRecievedMsgs = 0;

        List<string> mySentMsgs;
        List<string> myOffMsgID;

        List<string> recMsgs;
        List<string> readyMsgs;

        int[] officialMsgIDArray = new int[9];
        int[] officialMsgIDCount = new int[9];


        string[] receivedMsgs = new string[100];
        int[] msgOrder = new int[100];
        string[][] recMsgByPort = new string[5][];

        int clientIP = 0;
        int hostIP = 0;

        int[] client2 = new int[20];
        int[] client3 = new int[20];
        int[] client4 = new int[20];
        int[] client5 = new int[20];
        int[] client6 = new int[20];



        // This method sets up a socket for receiving messages from the Network
        private async void ReceiveMulticast()
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            // Determine the IP address of localhost
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = null;
            foreach (IPAddress ip in ipHostInfo.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip;
                    break;
                }
            }

            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, myPort);

            // Create a TCP/IP socket for receiving message from the Network.
            TcpListener listener = new TcpListener(localEndPoint);
            listener.Start(10);

            try
            {
                string data = null;

                // Start listening for connections.
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");

                    // Program is suspended while waiting for an incoming connection.
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();

                    Console.WriteLine("connectted");
                    data = null;

                    // Receive one message from the network
                    while (true)
                    {
                        bytes = new byte[1024];
                        NetworkStream readStream = tcpClient.GetStream();
                        int bytesRec = await readStream.ReadAsync(bytes, 0, 1024);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        // All messages ends with "<EOM>"
                        // Check whether a complete message has been received
                        if (data.IndexOf("<EOM>") > -1)
                        {
                            break;
                        }
                    }
                    Console.WriteLine("msg received:    {0}", data);

                    if (data.Contains("Response"))
                    {
                        int myMsgNum = Convert.ToInt32(data.Substring(22, 2));
                        int clientRecNum = Convert.ToInt32(data.Substring(36, 2));
                        myOffMsgID[myMsgNum] = Math.Max(myOffMsgID[myMsgNum], clientRecNum);
                        officialMsgIDCount[myMsgNum] += 1;

                        if (officialMsgIDCount[myMsgNum] == 5)
                        {
                            SendOfficialID(myMsgNum, officialMsgIDArray[myMsgNum]);
                        }
                    }
                    if (data.Contains("Official"))
                    {
                        int offMsg = Convert.ToInt32(data.Substring(30, 2));
                        int portNum = Convert.ToInt32(data.Substring(39, 4));
                        int offID = Convert.ToInt32(data.Substring(47, 2));

                        if (portNum == 8082)
                        {

                        }

                    }

                    else
                    {
                        receivedMsgs.Append(data);
                        int msgNum = Convert.ToInt32(data.Substring(10, 2));
                        int hostNum = Convert.ToInt32(data.Substring(18, 4));
                        SendRecNum(msgNum, hostNum, numRecievedMsgs);

                    }
                    
                }

            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString());
            }
        }

        // This method first sets up a task for receiving messages from the Network.
        // Then, it sends a multicast message to the Netwrok.
        public void SendOrigMsg()
        {
            try
            {
                // Find the IP address of localhost
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = null;
                foreach (IPAddress ip in ipHostInfo.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = ip;
                        break;
                    }
                }
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8081);
                Socket sendSocket;
                try
                {
                    // Create a TCP/IP  socket.
                    sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    // Connect to the Network 
                    sendSocket.Connect(remoteEP);

                    // Generate and encode the multicast message into a byte array.
                    mySentMsg += 1;
                    string asciiMsg = "Message #:" + mySentMsg + " From " + myPort + ": This is my message.<EOM>\n";
                    byte[] msg = Encoding.ASCII.GetBytes(asciiMsg);

                    // Send the data to the network.
                    int bytesSent = sendSocket.Send(msg);
                    // Add msg to sent list
                    mySentMsgs.Append(asciiMsg);

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void SendRecNum(int msgNum, int hostNum, int recNum)
        {
            try
            {
                // Find the IP address of localhost
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = null;
                foreach (IPAddress ip in ipHostInfo.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = ip;
                        break;
                    }
                }
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, hostNum);
                Socket sendSocket;
                try
                {
                    // Create a TCP/IP  socket.
                    sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    // Connect to the Network 
                    sendSocket.Connect(remoteEP);


                    // Generate and encode the multicast message into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes("Response to message #:" + msgNum + " Reciving #:" + recNum + "<EOM>\n");

                    // Send the data to the network.
                    int bytesSent = sendSocket.Send(msg);

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void SendOfficialID(int msgNum, int offNum)
        {
            try
            {
                // Find the IP address of localhost
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = null;
                foreach (IPAddress ip in ipHostInfo.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = ip;
                        break;
                    }
                }
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8081);
                Socket sendSocket;
                try
                {
                    // Create a TCP/IP  socket.
                    sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    // Connect to the Network 
                    sendSocket.Connect(remoteEP);


                    // Generate and encode the multicast message into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes("Official number for message #:" + msgNum + " From: " + myPort + " is:" + offNum + "<EOM>\n");

                    // Send the data to the network.
                    int bytesSent = sendSocket.Send(msg);

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
