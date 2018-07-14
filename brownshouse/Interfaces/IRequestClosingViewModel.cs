using brownshouse.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace brownshouse.Interfaces
{
    public interface IRequestClosingViewModel
    {
        Request SelectedRequest { get; set; }
        Employee CurrentUser { get; }
        string RequestNo { get; set; }
        ICommand AcceptedCommand { get; }
        ICommand RejectedCommand { get; }
        Visibility ClosedVisibility { get; set; }
        Visibility ResultVisibility { get; set; }
        bool? ShowDialog();
    }
}
