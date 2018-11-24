using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace ChatClient
{
    public partial class ChatClient : Form
    {
        TcpClient tcpClient;
        NetworkStream stream;
        StreamWriter writer;
        StreamReader reader;

        delegate void UpdateChatWindowDelegate(string message);
        UpdateChatWindowDelegate _updateChatWindowDelegate;

        public ChatClient()
        {
            tcpClient = new TcpClient();
            _updateChatWindowDelegate = new UpdateChatWindowDelegate(UpdateChatWindow);
            InitializeComponent();
        }

        public bool Connect(string ipAddress, int port)
        {

            try
            {
                tcpClient.Connect(ipAddress, port);
                stream = tcpClient.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);

                Thread read = new Thread(ProcessServerResponse);
                read.Start();
                Application.Run(this);

            }
            catch (Exception e)
            {
                Console.WriteLine("ExceptionL " + e.Message);
                return false;
            }

            return true;
        }

        /*public void Run()
        {
            string userInput;

            ProcessServerResponse();

            while ((userInput = Console.ReadLine()) != null)
            {
                writer.WriteLine(userInput);
                writer.Flush();

                ProcessServerResponse();

                if (userInput == "exit")
                {
                    break;
                }
            }

            tcpClient.Close();
        }*/

        void ProcessServerResponse()
        {
            string response;
            //Console.WriteLine("Server says: " + reader.ReadLine());
            //Console.WriteLine();

            while (true)
            {
                response = reader.ReadLine();


                UpdateChatWindow(response);
            }
        }

        public void button1_Click(object sender, EventArgs e)
        {
            string userInput;

            userInput = textBox1.Text;
            
            writer.WriteLine(userInput);
            writer.Flush();

            textBox1.Text = "";
            //ProcessServerResponse();
        }

        private void KeyboardInputRegister(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string userInput;

                userInput = textBox1.Text;

                writer.WriteLine(userInput);
                writer.Flush();

                textBox1.Text = "";
            }
        }

        void UpdateChatWindow(string message)
        {
            if (InvokeRequired)
            {
                Invoke(_updateChatWindowDelegate, message);
            }
            else
            {
                richTextBox1.Text += message += "\n";
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
            }
        }
    }
}