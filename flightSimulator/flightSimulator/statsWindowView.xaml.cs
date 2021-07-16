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

namespace flightSimulator
{
    
    public partial class statsWindowView : Window
    {
        ControlPanelViewModel vm;
        public statsWindowView(ControlPanelViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = vm;
        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.VM_Name = FeatureComboBox.SelectedItem.ToString();
        }

    }
}
