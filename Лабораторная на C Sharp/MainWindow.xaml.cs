using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Laba7_FOST
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            Expander.Header = "Добавить барьер";
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            Expander.Header = "Параметры барьера";
        }

        private void BurgerMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            Properties.Visibility = Visibility.Collapsed;
        }
        private void BurgerMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            Panel.Visibility = Visibility.Collapsed;
        }

        private void HideAnimation_Completed(object sender, EventArgs e)
        {
            Properties.Visibility = Visibility.Visible;
        }

        private void ShowAnimation_Completed(object sender, EventArgs e)
        {
            Panel.Visibility = Visibility.Visible;
        }
    }
}
