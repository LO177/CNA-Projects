using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace SimpleServer
{
    class SimpleServer
    {
        TcpListener tcpListener;
        List<ClientClass> clientsConnected = new List<ClientClass>();


        int currClientNumb = 0;


        public SimpleServer(string ipAddress, int port)
        {

            IPAddress IPClient = IPAddress.Parse(ipAddress);
            tcpListener = new TcpListener(IPClient, port);
            
        }

        /*public bool Connect(string ipAddress, int port)
        {
            try
            {
                tcpListener.Connect(ipAddress, port);

            }
            catch (Exception e)
            {
                Console.WriteLine("ExceptionS " + e.Message);
                return false;
            }

            return true;
        }*/

        public void Start()
        {
            Console.WriteLine("Initialising...");

            tcpListener.Start();

            while (true) {
                Socket currentSocket = tcpListener.AcceptSocket();
                //get the int value for the client index
                currClientNumb++;

                ClientClass currentClient = new ClientClass(currentSocket, currClientNumb);
                //add to clientsConnected list
                /*if (clientsConnected.Count == 0)
                {
                    clientsConnected.Add(currentSocket);
                }
                else
                {
                    for (int i = 0; i < clientsConnected.Count; i++)
                    {
                        if (currentSocket == clientsConnected[i])
                        {
                            //currClientNumb = i;
                            break;
                        }
                        else if (clientsConnected[i] == null)
                        {
                            clientsConnected[i] = currentSocket;
                        }
                    }
                }*/

                //currClientNumb++; // To make sure client number shown doesn't start at zero

                
                Thread thread = new Thread(new ParameterizedThreadStart(ClientMethod));

                thread.Start(currentClient);

                Console.WriteLine("Client Connected...");
            }
        }

        public void Stop()
        {
            tcpListener.Stop();
        }

        static void ClientMethod(object clientObject)
        {
            ClientClass client = (ClientClass)clientObject;

            string receivedMessage;

            //Console.WriteLine(reader.ReadLine());

            client.writer.WriteLine("message sent...");
            client.writer.Flush();


            string clientCountDisplay = client.thisClient.ToString();

            while ((receivedMessage = client.reader.ReadLine()) != null)
            {
                client.writer.WriteLine("user " + clientCountDisplay + ": " + GetReturnMessage(receivedMessage));
                client.writer.Flush();

                if (receivedMessage == "exit")
                {
                    break;
                }
            }

            client.Close();
        }

        /*static void SocketMethod(Socket socket, int currClientNumb)
        {


            string receivedMessage;

            NetworkStream stream = new NetworkStream(socket);
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            //Console.WriteLine(reader.ReadLine());

            writer.WriteLine("message sent...");
            writer.Flush();


            string clientCountDisplay = currClientNumb.ToString();

            while ((receivedMessage = reader.ReadLine()) != null)
            {
                writer.WriteLine("user " + clientCountDisplay + ": " + GetReturnMessage(receivedMessage));
                writer.Flush();

                if (receivedMessage == "exit")
                {
                    break;
                }
            }

            socket.Close();
        }*/

        static string GetReturnMessage(string code)
        {
            string str = code;
            return str;
        }
    }

    class ClientClass
    {
        Socket socket;
        NetworkStream stream;
        public StreamReader reader { get; private set; }
        public StreamWriter writer { get; private set; }
        //create an int for client number
        public int thisClient = 0;
        public ClientClass(Socket socket, int currClientNumb)
        {
            this.socket = socket;
            //set client number from currClientNumb
            stream = new NetworkStream(socket);
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);

            thisClient = currClientNumb;
        }

        public void Close()
        {
            socket.Close();
        }

    }
}
