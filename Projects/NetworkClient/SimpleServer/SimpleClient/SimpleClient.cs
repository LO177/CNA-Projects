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

            try
            {
                tcpClient.Connect(ipAddress, port);
                stream = tcpClient.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);

            }
            catch(Exception e)
            {
                Console.WriteLine("ExceptionL " + e.Message);
                return false;
            }
            
            return true;
        }

        public void Run()
        {
            string userInput;

            ProcessServerResponse();

            while((userInput = Console.ReadLine()) != null){
                writer.WriteLine(userInput);
                writer.Flush();

                if (userInput == "exit")
                {
                    break;
                }
            }

            tcpClient.Close();
        }

        void ProcessServerResponse()
        {
            Console.Write("Server says " + reader.ReadLine());
            Console.WriteLine();
        }
    }
}
