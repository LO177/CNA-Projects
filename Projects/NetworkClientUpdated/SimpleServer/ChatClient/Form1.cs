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
using System.Runtime.Serialization.Formatters.Binary;
using Packets;

namespace ChatClient
{
    public partial class ChatClient : Form
    {
        TcpClient tcpClient;
        NetworkStream stream;
        BinaryReader reader;
        BinaryWriter writer;

        static BinaryFormatter formatter = new BinaryFormatter();

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
                reader = new BinaryReader(stream);
                writer = new BinaryWriter(stream);

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
            //string response;
            //Console.Write("Server says: " + reader.ReadLine());
            //Console.Write();



            int noOfIncomingBytes = 0;

            try
            {

                while ((noOfIncomingBytes = reader.ReadInt32()) != 0)
                {
                    byte[] bites = reader.ReadBytes(noOfIncomingBytes);

                    MemoryStream byteStream = new MemoryStream(bites);

                    Packet packet = formatter.Deserialize(byteStream) as Packet;

                    ChatMessagePacket messageRead = HandlePacket(packet) as ChatMessagePacket;

                    var this_read = Task.Run(() => UpdateChatWindow(messageRead._message));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static Packet HandlePacket(Packet packet)
        {
            switch (packet.type)
            {
                case PacketType.CHATMESSAGE:

                    string message = ((ChatMessagePacket)packet)._message;
                    ChatMessagePacket chatmessagepacket = new ChatMessagePacket(message);

                    return chatmessagepacket;

                default:

                    return null;
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
                Console.WriteLine("{0} Thread ID: {1}" + Thread.CurrentThread.ManagedThreadId);

                richTextBox1.Text += message += "\n";
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
            }
        }







        ////////////////////////////////////////////////////////////////////

        public void button2_Click(object sender, EventArgs e)
        {
            string userName;

            userName = textBox2.Text;

            NicknamePacket nicknamepacket = new NicknamePacket(userName);

            Send(nicknamepacket);
        }




        public void button1_Click(object sender, EventArgs e)
        {
            string userInput;
            //string userName;

            //userName = textBox2.Text;
            userInput = textBox1.Text;

            //writer.Write(userInput);
            //writer.Flush();

            if (textBox1.Text == "/exit")
            {
                Application.Exit();
            }

            ChatMessagePacket chatmessagepacket = new ChatMessagePacket(userInput);

            Send(chatmessagepacket);

            textBox1.Text = "";
        }

        private void KeyboardInputRegister(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string userInput;

                userInput = textBox1.Text;

                //writer.Write(userInput);
                //writer.Flush();

                if (textBox1.Text == "/exit")
                {
                    Application.Exit();
                }

                ChatMessagePacket chatmessagepacket = new ChatMessagePacket(userInput);

                Send(chatmessagepacket);

                textBox1.Text = "";
            }
        }

        void Send(Packet packet)
        {
            MemoryStream memstream = new MemoryStream();
            formatter.Serialize(memstream, packet);

            byte[] buffer = memstream.GetBuffer();

            writer.Write(buffer.Length);
            writer.Write(buffer);
            writer.Flush();
        }

        private void ChatClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            tcpClient.Close();
        }
    }
}