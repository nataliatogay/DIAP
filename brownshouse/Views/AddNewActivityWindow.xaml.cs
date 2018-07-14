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
    public partial class AddNewActivityWindow : Window, IAddNewActivityView
    {
        IAddNewActivityViewModel viewModel;


        public AddNewActivityWindow()
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

        public void SetViewModel(IAddNewActivityViewModel viewModel)
        {
            this.DataContext = this.viewModel = viewModel;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.DisciplineSelectionChangedCommand.Execute(null);
        }
    }
}
