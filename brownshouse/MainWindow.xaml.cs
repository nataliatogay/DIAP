using brownshouse.Interfaces;
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

namespace brownshouse
{
    public partial class MainWindow : Window, IMainView
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Alert(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK);
        }

        public void Close(bool? dialogResult)
        {
            DialogResult = dialogResult;
        }

        public void SetBindingContext(IMainViewModel viewModel)
        {
            this.DataContext = viewModel;
        }

        private void Button_Click_Down(object sender, RoutedEventArgs e)
        {
            mainWindow.Height = 700;
            mainWindow.MaxHeight = 700;
            mainWindow.MinHeight = 700;
            btnDown.Visibility = Visibility.Collapsed;
            btnUp.Visibility = Visibility.Visible;
            stackAdditional.Visibility = Visibility.Visible;
        }

        private void Button_Click_Up(object sender, RoutedEventArgs e)
        {
            mainWindow.Height = 300;
            mainWindow.MaxHeight = 300;
            mainWindow.MinHeight = 300;
            btnDown.Visibility = Visibility.Visible;
            btnUp.Visibility = Visibility.Collapsed;
            stackAdditional.Visibility = Visibility.Collapsed;
        }
    }
}
