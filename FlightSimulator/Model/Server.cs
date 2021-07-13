using FlightSimulator.ViewModels;
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
    /* the Info channel from the simulator */
    public class Server : BaseNotify
    {
        TcpClient _client;
        TcpListener _listener;
        double lon, lat;

        //singelton
        private static Server m_Instance = null;
        public static Server Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Server();
                }
                return m_Instance;
            }
        }

        //private constructor will be called from singelton
        private Server()
        {

            ShouldStop = false;
        }

        public bool ShouldStop
        {
            set;
            get;
        }


        public double Lon
        {
            set
            {
                lon = value;
                //when recieved new data, notify the view by viewModel
                NotifyPropertyChanged("Lon");

            }
            get { return lon; }
        }

        public double Lat
        {
            set
            {
                lat = value;
                //when recieved new data, notify the view by viewModel
                NotifyPropertyChanged("Lat");
            }
            get { return lat; }
        }
        

        /*make a connection to the simulator as a server to recieve data*/
        public void connectServer()
        {
            //extract IP and port of the simulator as saved in ApplicationSettingsModel
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),
                ApplicationSettingsModel.Instance.FlightInfoPort);

            _listener = new TcpListener(ep);
            _listener.Start();
            _client = _listener.AcceptTcpClient();
            //after connection receive data in differnt thread
            Thread thread = new Thread(() => listen(_client, _listener));
            thread.Start();

        }

        // recieves the data from the plane and split the message to lon and lat
        public void listen(TcpClient _client, TcpListener _listener)
        {
            Byte[] bytes;
            NetworkStream ns = _client.GetStream();
            //recieve message from the simulator until shouldStop.
            while (!ShouldStop)
            {
                if (_client.ReceiveBufferSize > 0)
                {
                    bytes = new byte[_client.ReceiveBufferSize];
                    ns.Read(bytes, 0, _client.ReceiveBufferSize);
                    string msg = Encoding.ASCII.GetString(bytes); //the message incoming
                    splitMessage(msg);
                }
            }
            ns.Close();
            _client.Close();
            _listener.Stop();
        }

        // split the server message and parse to lon and lat
        public void splitMessage(string msg)
        {
            string[] splitMs = msg.Split(',');
            //if the message recieved new data
            if (msg.Contains(","))
            {
                Lon = double.Parse(splitMs[0]);
                Lat = double.Parse(splitMs[1]);
            }
        }

        public void DisConnect()
        {
            ShouldStop = true;
        }

        /* returns true if the server still listening, false otherwise */
        public bool isConnected()
        {
            return (_listener != null) ;
        }

    }
}