using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /////////////////////////change this^

            string IP = "127.0.0.1"; //local IP
            int port = 4444;

            ChatClient clientInst = new ChatClient();
            if (clientInst.Connect(IP, port))
            {
                //clientInst.button1.PerformClick();
            }
            else
            {
                Console.WriteLine("Failed to connect to the server");
            }
        }
    }
}
