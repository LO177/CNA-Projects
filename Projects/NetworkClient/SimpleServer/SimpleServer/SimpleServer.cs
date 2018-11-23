using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace SimpleServer
{
    class SimpleServer
    {
        TcpListener tcpListener;
        List<Socket> clientsConnected = new List<Socket>();
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
            Socket currentSocket = tcpListener.AcceptSocket();
            Console.WriteLine("Client Connected...");


            if (clientsConnected.Count == 0) {
                clientsConnected.Add(currentSocket);
            }
            else {
                for (int i = 0; i < clientsConnected.Count; i++) {
                    if (currentSocket == clientsConnected[i]) {
                        currClientNumb = i;
                        break;
                    }
                    else if (clientsConnected[i] == null) {
                        clientsConnected[i] = currentSocket;
                    }
                }
            }

            currClientNumb++; // To make sure client number shown doesn't start at zero

            SocketMethod(currentSocket, currClientNumb);
        }

        public void Stop()
        {
            tcpListener.Stop();
        }

        static void SocketMethod(Socket socket, int currClientNumb)
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
        }

        static string GetReturnMessage(string code)
        {
            string str = code;
            return str;
        }
    }
}
