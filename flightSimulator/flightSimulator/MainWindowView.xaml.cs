using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace flightSimulator
{
        
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        MyClient reader = new MyClient();
        public MainWindowView()
        {
            InitializeComponent();
            //XML always stored in this path (FlightGear)
            //string pathToXML = @"C:\Program Files\FlightGear 2020.3\data\Protocol\playback_small.xml";
            //MyClient mr = new MyClient();
            //List<string> l = mr.parseXML(@"C:\Program Files\FlightGear 2020.3\data\Protocol\playback_small.xml");
            //l.ForEach(Console.WriteLine);
            //Console.WriteLine(l.Count);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            controlPanelView control_panel = new controlPanelView();
            control_panel.Show();
            this.Hide();

            //controlPanelView win3 = new controlPanelView();
            //win3.Show();
        }
    }
}
