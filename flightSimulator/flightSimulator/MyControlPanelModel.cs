using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using OxyPlot;
using OxyPlot.Series;

namespace flightSimulator
{
    public class MyControlPanelModel : IControlPanelModel
    {
        private TimeSeries timeSeries;
        private MyClient client;
        private string speed;
        private PlotModel mplot;
        private PlotModel pearsonPlot;
        private PlotModel linearRegPlot;
        private string name;
        private volatile string length;
        private volatile int time;
        private double elevator_atm;
        private double aileronValue;
        private string xmlPath = @"C:\Program Files\FlightGear 2020.3\data\Protocol\playback_small.xml";
        private double throttleVal;
        private double rudd;
        private double altitude;
        private double airspeed;
        private double yaw;
        private double direction;
        private double roll;
        private double pitch;

        public Double Pitch
        {
            get
            {
                return this.pitch;
            }
            set
            {
                this.pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }
        public Double Roll
        {
            get
            {
                return this.roll;
            }
            set
            {
                this.roll = value;
                NotifyPropertyChanged("Roll");
            }
        }
        public Double Direction
        {
            get
            {
                return this.direction;
            }
            set
            {
                this.direction = value;
                NotifyPropertyChanged("Direction");
            }
        }
        public Double Yaw
        {
            get
            {
                return this.yaw;
            }
            set
            {
                this.yaw = value;
                NotifyPropertyChanged("Yaw");
            }
        }
        public Double AirSpeed
        {
            get
            {
                return this.airspeed;
            }
            set
            {
                this.airspeed = value;
                NotifyPropertyChanged("AirSpeed");
            }
        }
        public Double Altitude
        {
            get
            {
                return this.altitude;
            }
            set
            {
                this.altitude = value;
                NotifyPropertyChanged("Altitude");
            }
        }
        public Double Rudder
        {
            get
            {
                return this.rudd;
            }
            set
            {
                this.rudd = value;
                NotifyPropertyChanged("Rudder");
            }
        }
        public Double Throttle
        {
            get
            {
                return this.throttleVal;
            }
            set
            {
                this.throttleVal = value;
                NotifyPropertyChanged("Throttle");
            }
        }
        public Double Elevator
        {
            get
            {
                return this.elevator_atm;
            }
            set
            {
                this.elevator_atm = value;
                NotifyPropertyChanged("Elevator");
            }
        }
        public Double Aileron
        {
            get
            {
                return this.aileronValue;
            }
            set
            {
                this.aileronValue = value;
                NotifyPropertyChanged("Aileron");
            }
        }
        public String Speed
        {
            get
            {
                return this.speed;
            }
            set
            {
                this.speed = value;
                NotifyPropertyChanged("Speed");
            }
        }
        public int Time
        {
            get
            {
                return this.time;
            }
            set
            {
                this.time = value;
                NotifyPropertyChanged("Time");
            }
        }
        public string FlightLength
        {
            get
            {
                return this.length;
            }
            set
            {
                this.length = value;
                NotifyPropertyChanged("FlightLength");
            }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; NotifyPropertyChanged("Name"); }
        }
        public PlotModel Mplot
        {
            get
            {
                return this.mplot;
            }
            set
            {
                this.mplot = value;
                NotifyPropertyChanged("Mplot");
            }
        }
        public PlotModel PearsonPlot
        {
            get
            {
                return this.pearsonPlot;
            }
            set
            {
                this.pearsonPlot = value;
                NotifyPropertyChanged("pearsonPlot");
            }
        }
        public PlotModel LinearRegPlot
        {
            get
            {
                return this.linearRegPlot;
            }
            set
            {
                this.linearRegPlot = value;
                NotifyPropertyChanged("linearRegPlot");
            }
        }
        public List<string> Keys
        {
            get
            {
                return timeSeries.GetFeatures();
            }
        }
        public MyControlPanelModel(MyClient client)
        {
            this.client = client;
            this.timeSeries = new TimeSeries(this.client.parseXML(xmlPath));
            Time = 0;
            Elevator = 125;
            Aileron = 125;
            Throttle = 0;
            Rudder = -1;
            Altitude = 0;
            AirSpeed = 0;
            Yaw = 0;
            Direction = 0;
            Roll = 0;
            Pitch = 0;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void speedChanged(string newSpeed)
        {
            setClientSpeed(Convert.ToDouble(newSpeed));
        }
        public void sendCSV(string path)
        {
            if (!this.client.Connect("localhost", 5400))
            {
                return;
            }
            StreamReader sr = new StreamReader(path);
            Stream stm = this.client.TcpClient.GetStream();
            string line;
            new Thread(delegate ()
            {
                string[] allLines = File.ReadAllLines(path);
                this.FlightLength = (allLines.Length).ToString();
                foreach (string lineThis in allLines)
                {
                    timeSeries.AddSingleRow(lineThis);
                }
                for (; Time < allLines.Length && this.client.connected; Time++)
                {
                    line = allLines[Time];
                    Elevator = (((timeSeries.GetColByName("elevator"))[Time]) * 60) + 125;
                    Aileron = (((timeSeries.GetColByName("aileron"))[Time]) * 60) + 125;
                    Throttle = (timeSeries.GetColByName("throttle"))[Time];
                    Rudder = (timeSeries.GetColByName("rudder"))[Time];

                    string altitudeString = String.Format("{0:000.0000}", (timeSeries.GetColByName("altimeter_indicated-altitude-ft"))[Time]);
                    Altitude = Convert.ToDouble(altitudeString);
                    string airspeedString = String.Format("{0:00.00000}", (timeSeries.GetColByName("airspeed-kt"))[Time]);
                    AirSpeed = Convert.ToDouble(airspeedString);
                    string yawString = String.Format("{0:0.000000}", (timeSeries.GetColByName("side-slip-deg"))[Time]);
                    Yaw = Convert.ToDouble(yawString);
                    string directionString = String.Format("{0:000.0000}", (timeSeries.GetColByName("indicated-heading-deg"))[Time]);
                    Direction = Convert.ToDouble(directionString);
                    string rollString = String.Format("{0:00.00000}", (timeSeries.GetColByName("roll-deg"))[Time]);
                    Roll = Convert.ToDouble(rollString);
                    string pitchString = String.Format("{0:0.000000}", (timeSeries.GetColByName("pitch-deg"))[Time]);
                    Pitch = Convert.ToDouble(pitchString);

                    line += "\r\n";
                    Byte[] lineInBytes = System.Text.Encoding.ASCII.GetBytes(line);
                    stm.Write(lineInBytes, 0, lineInBytes.Length);
                    if (this.name != null) { DrawGraphs(Time); }
                    this.client.NetworkStream.Flush();

                    while (this.client.Speed == 0)
                    {
                        Thread.Sleep(1000);
                    }
                    int sleepTime = (int)(100 / this.client.Speed);
                    Thread.Sleep(sleepTime);
                    //Thread.Sleep(100);
                    //Console.Write(line);
                }
                sr.Close();
                stm.Close();
                this.client.TcpClient.Close();
            }).Start();
        }

        public void stopClicked()
        {
            this.client.closeConnection();
        }
        public void setClientSpeed(double newSpeed)
        {
            this.client.setSpeed(newSpeed);
        }
        public void DrawGraphs(int time)
        {
            //feature graph
            var tmp = new PlotModel { };
            var series1 = new LineSeries
            {
                Title = this.name,
                MarkerType = MarkerType.Circle,
                MarkerSize = 0.5
            };
            // j=time;
            for (int j = 0; j < time; j++)
            {
                series1.Points.Add(GetValue(j, this.name));
            }
            tmp.Series.Add(series1);
            this.mplot = tmp;
            NotifyPropertyChanged("mplot");

            //pearson graph
            string pf = timeSeries.learnNormal(this.name);
            var tmpPearson = new PlotModel { };
            var series2 = new LineSeries
            {
                Title = pf,
                MarkerType = MarkerType.Circle,
                LineStyle = LineStyle.Automatic,
                MarkerSize = 0.5

            };
            // j=time;
            for (int j = 0; j < time; j++)
            {
                series2.Points.Add(GetValue(j, pf));
            }
            tmpPearson.Series.Add(series2);
            this.pearsonPlot = tmpPearson;
            NotifyPropertyChanged("PearsonPlot");

            //linearReg graph
            var tmpLR = new PlotModel { };
            var series3 = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                LineStyle = LineStyle.Automatic,
                MarkerSize = 0.5
            };
            List<DataPoint> LRE = new List<DataPoint>();
            LRE = this.timeSeries.linearReg(timeSeries.GetColByName(this.name), timeSeries.GetColByName(pf));
            for (int i = 0; i < 2; i++)
            {
                series3.Points.Add(LRE[i]);
            }
            tmpLR.Series.Add(series3);


            //30 last
            var series30 = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                LineStyle = LineStyle.None,
                MarkerFill = OxyColors.Red,
                MarkerStroke = OxyColors.Red,
                MarkerSize = 0.8
            };
            if (time < 30)
                for (int i = 0; i < time; i++)
                {
                    series30.Points.Add(new DataPoint(timeSeries.GetColByName(pf)[i], timeSeries.GetColByName(this.name)[i]));
                }
            else
                for (int i = time - 30 + 1; i <= time; i++)
                {
                    series30.Points.Add(new DataPoint(timeSeries.GetColByName(pf)[i], timeSeries.GetColByName(this.name)[i]));
                }
            tmpLR.Series.Add(series30);
            this.linearRegPlot = tmpLR;
            NotifyPropertyChanged("LinearRegPlot");
        }
        public DataPoint GetValue(int i, string n)
        {
            return new DataPoint(i, this.timeSeries.GetColByName(n)[i]);
        }
    }
}
