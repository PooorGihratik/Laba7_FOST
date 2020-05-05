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
        bool isChecked = true;
        NewViewModel viewModel = new NewViewModel();
        public MainWindow()
        {
            InitializeComponent();
            viewModel.Show += ShowMessage;
        }

        private void ShowMessage(string message, string title)
        {
            MessageBox.Show(message, title);
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Results.Text != "") Clipboard.SetText(Results.Text);
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            Expander.Header = "Добавить барьер";
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            Expander.Header = "Параметры барьера";
        }

        private void Radians_Checked(object sender, RoutedEventArgs e)
        {
            isChecked = true;
        }

        private void Degrees_Checked(object sender, RoutedEventArgs e)
        {
            isChecked = false;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShowPicture(Speed.Text, Angle.Text, isChecked);
            picture.ItemsSource = viewModel.DataPoints;
            Results.Text = viewModel.ListOfData;
            if (Results.Text != "") Save.IsEnabled = true;
        }

        private void Speed_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Angle.Text != "" && Speed.Text != "") Start.IsEnabled = true;
            else Start.IsEnabled = false;
        }

        private void Angle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Angle.Text != "" && Speed.Text != "") Start.IsEnabled = true;
            else Start.IsEnabled = false;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveInFile();
        }

        private void BurgerMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            Properties.Visibility = Visibility.Collapsed;
        }

        private void HideAnimation_Completed(object sender, EventArgs e)
        {
            Properties.Visibility = Visibility.Visible;
        }

        private void ShowAnimation_Completed(object sender, EventArgs e)
        {
            Panel.Visibility = Visibility.Visible;
        }

        private void BurgerMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            Panel.Visibility = Visibility.Collapsed;
        }
    }
}
