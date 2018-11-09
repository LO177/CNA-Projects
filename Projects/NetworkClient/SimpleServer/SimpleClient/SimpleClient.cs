using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;


namespace SimpleClient
{
    class SimpleClient
    {
        TcpClient tcpClient;
        NetworkStream stream;
        StreamWriter writer;
        StreamReader reader;

        public SimpleClient()
        {
            tcpClient = new TcpClient();

        }

        public bool Connect(string ipAddress, int port)
        {
            bool connection = false;

            try
            {
                tcpClient.Connect(ipAddress, port);
                stream = tcpClient.GetStream();

                string receivedMessage;
                
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);

                connection = true;
            }
            catch(Exception e)
            {
                connection = false;
                Console.WriteLine("ExceptionL " + e.Message);
                return connection;
            }
            
            return connection;
        }

        public void Run()
        {
            string userInput;

            ProcessServerResponse();
        }

        void ProcessServerResponse()
        {

        }
    }
}
