using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    /* the Commands channel to send orders as client to flight simulator */
    class CommandConnect
    {
        TcpClient client;
        private Mutex mutex;
        /// singelton
        private static CommandConnect m_Instance = null;
        public static CommandConnect Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new CommandConnect();
                }
                return m_Instance;
            }
        }

        //private constructor will be called from singelton
        private CommandConnect()
        {
            isConnected = false;
            mutex = new Mutex();
        }

        private bool isConnected;
        public bool IsConnected
        {
            get;
            set;
        }

        /* connect to flight simulater as client on the setted IP and port */
        public void ConnetAsClient()
        {
            //extract the saved IP and port from the ApllicationSettingModel 
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),
                ApplicationSettingsModel.Instance.FlightCommandPort);
            client = new TcpClient();
            client.Connect(iPEndPoint);
            isConnected = true;
            //Console.WriteLine("Command channel :You are connected");

        }

        
        public void DisConnect()
        {
            isConnected = false;
            client.Close();
        }

        /* send orders to the flight simulator */
        public void Send(string message)
        {
            //break the string to an array of orders
            string[] commands = ParseMessage(message);
            mutex.WaitOne();
            //send the messages in different threads
            Thread thread = new Thread(() => RunSend(commands, client));
            thread.Start();
            mutex.ReleaseMutex();

        }

        /* the function to run in thread, send the orders to simulator */
        private void RunSend(string[] commands, TcpClient client)
        {
            if (!isConnected) return;

            NetworkStream stream = client.GetStream();

            //send orders one by one
            foreach (string command in commands)
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(command+"\r\n");
                stream.Write(data, 0, data.Length);
                //wait 2 seconds between messages
                System.Threading.Thread.Sleep(2000);
            }
        }

        /* get all commands concanated in one string, break the string and return array of the commands*/
        private string[] ParseMessage(string message)
        {
            string[] commands;
            return commands = message.Split(new[] { Environment.NewLine },StringSplitOptions.None);
        }

    }
}
