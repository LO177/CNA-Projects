using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string IP = "127.0.0.1"; //local IP
            int port = 4444;

            SimpleServer serverInst = new SimpleServer(IP, port);
            serverInst.Start();
            serverInst.Stop();
        }
    }
}
