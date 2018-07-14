using Autofac;
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
    public partial class LoginWindow : Window, ILoginView
    {
        ILoginViewModel viewModel;
        public LoginWindow()
        {
            InitializeComponent();
        }

        public void Alert(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK);
        }

        public void Close(bool? dialogResult)
        {
            this.Close();
        }

        public void SetBindingContext(ILoginViewModel viewModel)
        {
            this.DataContext = this.viewModel = viewModel;
        }

        public MessageBoxResult AlertQuestion(string message, string caption)
        {
            return MessageBox.Show(message, caption, MessageBoxButton.YesNo);
        }

        public new void Show()
        {
            txtPassword.Clear();
            base.Show();
        }
    }
}
