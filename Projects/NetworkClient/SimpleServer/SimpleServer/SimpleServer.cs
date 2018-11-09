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
        static int clientsConnected = 0;
        
        public SimpleServer(string ipAddress, int port)
        {
            IPAddress IPClient = IPAddress.Parse(ipAddress);
            tcpListener = new TcpListener(IPClient, port);
            
        }

        public void Start()
        {
            tcpListener.Start();
            Socket currentSocket = tcpListener.AcceptSocket();

            clientsConnected++;

            SocketMethod(currentSocket);
        }

        public void Stop()
        {
            tcpListener.Stop();

            clientsConnected--;
        }

        static void SocketMethod(Socket socket)
        {
            string receivedMessage;
            NetworkStream stream;

            stream = new NetworkStream(socket);

            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            writer.WriteLine("Packet received...");
            writer.Flush();


            string clientCountDisplay = clientsConnected.ToString();
            string returnedMessage = "";

            while ((receivedMessage = reader.ReadLine()) != null)
            {
                returnedMessage = GetReturnMessage(receivedMessage);
                writer.WriteLine("user ", clientCountDisplay, ": ", returnedMessage);
                writer.Flush();
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
