using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string IP = "127.0.0.1"; //local IP
            int port = 4444;

            SimpleClient clientInst = new SimpleClient();
            if (clientInst.Connect(IP, port)) {
                clientInst.Run();
            }
            else
            {
                Console.WriteLine("Failed to connect to the server");
            }
        }
    }
}
