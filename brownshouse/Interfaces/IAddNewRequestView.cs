using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace brownshouse.Interfaces
{
    public interface IAddNewRequestView : IView
    {
        void SetViewModel(IAddNewRequestViewModel viewModel);
        MessageBoxResult AlertQuestion(string message, string caption);
    }
}
