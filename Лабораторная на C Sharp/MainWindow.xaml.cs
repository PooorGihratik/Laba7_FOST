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

namespace Laba8_FOST
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isChecked = true;
        ShitViewModel viewModel = new ShitViewModel(); 
        public MainWindow()
        {
            InitializeComponent();
            viewModel.Show += ShowMessage; 
        }

        private void But1_Click(object sender, RoutedEventArgs e)
        {
            results.Text = viewModel.GetResults(speed.Text,angle.Text,isChecked);
        }

        private void InRadians_Checked(object sender, RoutedEventArgs e)
        {
            isChecked = true;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            isChecked = false;
        }
        private void ShowMessage(string message,string title)
        {
            MessageBox.Show(message,title);
        }
    }
}
