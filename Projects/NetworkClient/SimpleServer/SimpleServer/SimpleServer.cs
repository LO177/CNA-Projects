using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Packets;

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

                clientsConnected.Add(currentClient);
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

        void ClientMethod(object clientObject)
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

                for (int i = 0; i < clientsConnected.Count; i++)
                {
                    ClientClass selectedClient = clientsConnected[i];
                    if (selectedClient != client)
                    {
                        selectedClient.writer.WriteLine("user " + clientCountDisplay + ": " + GetReturnMessage(receivedMessage));
                        selectedClient.writer.Flush();
                    }
                }

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
       
        public BinaryReader reader { get; private set; }
        public BinaryWriter writer { get; private set; }

        BinaryFormatter formatter;

        //create an int for client number
        public int thisClient = 0;
        public ClientClass(Socket socket, int currClientNumb)
        {
            formatter = new BinaryFormatter();
            this.socket = socket;
            //set client number from currClientNumb
            stream = new NetworkStream(socket);
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);

            thisClient = currClientNumb;
        }

        public void Client()
        {

        }

        public void Close()
        {
            socket.Close();
        }

        public void Send(Packet packet)
        {
            MemoryStream memstream = new MemoryStream();
            formatter.Serialize(memstream, packet);

            byte[] buffer = memstream.GetBuffer();

            writer.Write(buffer.Length);
            writer.Write(buffer);
            writer.Flush();
        }
    }
}
