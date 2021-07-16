using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net.Sockets;
using System.Threading;

namespace flightSimulator
{
    
    public class MyClient : IClient
    {
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        public volatile bool connected;
        private string length;
        private string time;
        private double speed;
        public TcpClient TcpClient
        {
            get
            {
                return tcpClient;
            }
        }
        public string flightLength
        {
            get
            {
                return this.length;
            }
            set
            {
                this.length = value;
            }
        }
        public double Speed
        {
            get
            {
                return this.speed;
            }
            set
            {
                this.speed = value;
            }
        }
        public string Time
        {
            get
            {
                return this.time;
            }
            set
            {
                this.time = value;
            }
        }
        public NetworkStream NetworkStream
        {
            get
            {
                return this.networkStream;
            }
        }

        public MyClient()
        {
            this.connected = false;
            speed = 1;
            Time = "0";
          
        }
        public void setSpeed(double speed)
        {
            this.speed = speed;
        }
        public List<String> parseXML(String path)
        {
            List<String> attNames = new List<String>(); // Create a list that will hold all attributes names.
            XmlDocument xmlDoc = new XmlDocument(); 
            xmlDoc.Load(path); // Load the XML document from the specified file using it's path.
            var outputNodes = xmlDoc.SelectNodes("//output");
            foreach (XmlNode thisNode in outputNodes)
            {
                var nameNodes = thisNode.SelectNodes(".//name");
                foreach(XmlNode name in nameNodes)
                {
                    attNames.Add(name.InnerText);
                }
            }
            return attNames;
        }

        public bool Connect(String IP, int port)
        {
            try
            {
                this.tcpClient = new TcpClient(IP, port);
                this.networkStream = this.tcpClient.GetStream();
                this.connected = true;
                Console.Write("I am in connect");
                //this.tcpClient.Connect(IP, port);
                return true;

            }
            catch
            {
                this.connected = false;
                return false;
            }
        }

        public void closeConnection()
        {
            this.tcpClient.Close();
            this.networkStream.Close();
            this.connected = false;
        }
    }
}

