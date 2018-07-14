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
    public partial class AddNewSubsystemWindow : Window, IAddNewSubsystemView
    {
        IAddNewSubsystemViewModel viewModel;
        public AddNewSubsystemWindow()
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

        public void SetViewModel(IAddNewSubsystemViewModel viewModel)
        {
            this.DataContext = this.viewModel = viewModel;
        }
    }
}
