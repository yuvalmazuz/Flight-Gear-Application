using System.Collections.Generic;
using System.ComponentModel;
using OxyPlot;

namespace flightSimulator
{
    public interface IControlPanelModel:INotifyPropertyChanged
    {
        double Aileron { get; set; }
        double AirSpeed { get; set; }
        double Altitude { get; set; }
        double Direction { get; set; }
        double Elevator { get; set; }
        string FlightLength { get; set; }
        List<string> Keys { get; }
        PlotModel LinearRegPlot { get; set; }
        PlotModel Mplot { get; set; }
        string Name { get; set; }
        PlotModel PearsonPlot { get; set; }
        double Pitch { get; set; }
        double Roll { get; set; }
        double Rudder { get; set; }
        string Speed { get; set; }
        double Throttle { get; set; }
        int Time { get; set; }
        double Yaw { get; set; }

        //event PropertyChangedEventHandler PropertyChanged;

        void DrawGraphs(int time);
        DataPoint GetValue(int i, string n);
        void NotifyPropertyChanged(string propName);
        void sendCSV(string path);
        void setClientSpeed(double newSpeed);
        void speedChanged(string newSpeed);
        void stopClicked();
    }
}