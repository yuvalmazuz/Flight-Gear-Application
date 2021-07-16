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
using System.Windows.Shapes;
using Microsoft.Win32;

namespace flightSimulator
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class controlPanelView : Window
    {
        ControlPanelViewModel vm;
        private string csv_path;
        private Boolean isPlayPressed;
        private Boolean isPausePressed;
        private Boolean firstPlay;
        private statsWindowView stats;
        public string CsvPath
        {
            get
            {
                return this.csv_path;
            }
            set
            {
                this.csv_path = String.Copy(value);
            }
        }
        public controlPanelView()
        {
            InitializeComponent();
            vm = new ControlPanelViewModel(new MyControlPanelModel(new MyClient()));
            DataContext = vm;
            CsvPath = "";
            isPlayPressed = false;
            isPausePressed = false;
            firstPlay = true;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            String filePath = "";
            OpenFileDialog ofd = new OpenFileDialog();
            //Filter out only CSV files
            ofd.Filter = "CSV files(*.csv)| *.csv";
            if (ofd.ShowDialog() == true)
            {
                filePath = ofd.FileName;
            }
            pathTextBox.Text = filePath;
            this.CsvPath = filePath;
        }


        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            //if no file was uploaded, don't load.
            if (this.CsvPath.Length == 0)
            {
                return;
            }
            this.LoadButton.Visibility = Visibility.Hidden;
            this.BrowseButton.Visibility = Visibility.Hidden;
            this.pathTextBox.Visibility = Visibility.Hidden;
            this.PlayButton.Visibility = Visibility.Visible;
            this.PauseButton.Visibility = Visibility.Visible;
            this.StopButton.Visibility = Visibility.Visible;
            this.speedComboBox.Visibility = Visibility.Visible;
            //this.FeatureComboBox.Visibility = Visibility.Visible;
            //this.FeatureGraph.Visibility = Visibility.Visible;
            this.speedComboBox.Items.Add("0.25");
            this.speedComboBox.Items.Add("0.5");
            this.speedComboBox.Items.Add("1");
            this.speedComboBox.Items.Add("1.5");
            this.speedComboBox.Items.Add("2");
            this.Slider.Visibility = Visibility.Visible;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (isPlayPressed)
            {
                return;
            }
            isPausePressed = false;
            isPlayPressed = true;
            if (firstPlay)
            {
                stats = new statsWindowView(this.vm);
                this.vm.setClientSpeed(1);
                firstPlay = false;
                stats.Show();
                this.vm.sendCSV(this.CsvPath);
            }
            else
            {
                this.vm.setClientSpeed(1);
                this.speedComboBox.SelectedItem = "1";
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            this.vm.stopClicked();
            this.Close();
            MessageBox.Show("Flight stopped, thanks for playing!");
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (isPausePressed)
            {
                return;
            }
            isPausePressed = true;
            isPlayPressed = false;
            this.vm.setClientSpeed(0);
            this.speedComboBox.SelectedValue = default;
        }

        private void speedComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.speedChanged((string)speedComboBox.SelectedItem);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Slider.Value == Convert.ToInt64(vm.VM_FlightLength))
            {
                StopButton_Click(sender, e);
            }
            vm.VM_Time = (int)Slider.Value;
        }

    }
}
