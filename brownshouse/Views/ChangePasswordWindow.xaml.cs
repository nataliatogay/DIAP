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
using System.Windows.Shapes;

namespace brownshouse.Views
{
    public partial class ChangePasswordWindow : Window, IChangePasswordView
    {
        IChangePasswordViewModel viewModel;
        public ChangePasswordWindow()
        {
            InitializeComponent();
        }

        private void newPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                viewModel.NewPassword = ((PasswordBox)sender).Password;
            }
        }

        private void oldPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                viewModel.OldPassword = ((PasswordBox)sender).Password;
            }
        }

        private void repeatPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                viewModel.NewPasswordRepeat = ((PasswordBox)sender).Password;
            }
        }

        public void Alert(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK);
        }

        public void Close(bool? dialogResult)
        {
            DialogResult = dialogResult;
        }

        public void SetViewModel(IChangePasswordViewModel viewModel)
        {
            this.DataContext = this.viewModel = viewModel;
        }
    }
}
