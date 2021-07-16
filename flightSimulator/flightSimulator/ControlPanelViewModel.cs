using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;


namespace flightSimulator
{
    public class ControlPanelViewModel : INotifyPropertyChanged
    {
        private MyControlPanelModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        private PlotModel mplot;
        private PlotModel pearsonPlot;
        private string name;
        private PlotModel linearRegPlot;

        public Double VM_Pitch
        {
            get
            {
                return this.model.Pitch;
            }
            set
            {
                this.model.Pitch = value;
            }
        }
        public Double VM_Roll
        {
            get
            {
                return this.model.Roll;
            }
            set
            {
                this.model.Roll = value;
            }
        }
        public Double VM_Direction
        {
            get
            {
                return this.model.Direction;
            }
            set
            {
                this.model.Direction = value;
            }
        }
        public Double VM_Yaw
        {
            get
            {
                return this.model.Yaw;
            }
            set
            {
                this.model.Yaw = value;
            }
        }
        public Double VM_AirSpeed
        {
            get
            {
                return this.model.AirSpeed;
            }
            set
            {
                this.model.AirSpeed = value;
            }
        }
        public Double VM_Altitude
        {
            get
            {
                return this.model.Altitude;
            }
            set
            {
                this.model.Altitude = value;
            }
        }
        public Double VM_Rudder
        {
            get
            {
                return this.model.Rudder;
            }
            set
            {
                this.model.Rudder = value;
            }
        }
        public Double VM_Throttle
        {
            get
            {
                return this.model.Throttle;
            }
            set
            {
                this.model.Throttle = value;
            }
        }
        public Double VM_Aileron
        {
            get
            {
                return this.model.Aileron;
            }
            set
            {
                this.model.Aileron = value;
            }
        }
        public Double VM_Elevator
        {
            get
            {
                return this.model.Elevator;
            }
            set
            {
                this.model.Elevator = value;
            }
        }
        public int VM_Time
        {
            get
            {
                return this.model.Time;
            }
            set
            {
                this.model.Time = value;
            }
        }
        public string VM_FlightLength
        {
            get
            {
                return this.model.FlightLength;
            }
            set
            {
                this.model.FlightLength = value;
            }
        }
        public String VM_Speed
        {
            get
            {
                return this.model.Speed;
            }
            set
            {
                this.model.Speed = value;
                this.model.setClientSpeed(Convert.ToDouble(value));
                //NotifyPropertyChanged("Speed");
            }
        }
        public string VM_Name 
        { 
            get 
            { 
                return model.Name; 
            } 
            set 
            { 
                this.name = value; 
                model.Name = value; 
            } 
        }
        public PlotModel VM_Mplot 
        { 
            get 
            { 
                return model.Mplot; 
            } 
            set 
            { 
                this.mplot = value;
                 
            } 
        }
        public PlotModel VM_PearsonPlot
        { 
            get 
            { 
                return model.PearsonPlot; 
            } 
            set 
            { 
                this.pearsonPlot = value;
            } 
        }
        public List<string> VM_Keys
        { 
            get 
            { 
                return this.model.Keys; 
            }
        }
        public PlotModel VM_LinearRegPlot
        {
            get
            {
                return this.model.LinearRegPlot;
            }
            set
            {
                this.linearRegPlot = value;
            }
        }
        public ControlPanelViewModel(MyControlPanelModel model)
        {
            this.model = model;
            VM_Speed = "0";
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        public void speedChanged(string newSpeed)
        {
            model.speedChanged(newSpeed);
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void setClientSpeed(double newSpeed)
        {
            this.model.setClientSpeed(newSpeed);
        }

        public void stopClicked()
        {
            this.model.stopClicked();
        }

        public void sendCSV(string path)
        {
            this.model.sendCSV(path);
        }
    }
}
