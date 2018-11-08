using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace SimpleServer
{
    class SimpleServer
    {
        TcpListener tcpListener;
        //string IP = "127.0.0.1"; //local IP
        //port = ;
        SimpleServer(string ipAddress, int port)
        {
            IPAddress IPClient = IPAddress.Parse(ipAddress);
            tcpListener = new TcpListener(IPClient, port);
            
        }

        void Start()
        {
            tcpListener.Start();
            tcpListener.AcceptSocket();


        }

        void Stop()
        {

        }

        static void SocketMethod(Socket socket)
        {
            return;
        }

        static string GetReturnMessage(string code)
        {
            string str = "Hello!";
            return str;
        }
    }
}
