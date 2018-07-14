using brownshouse.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace brownshouse.Interfaces
{
    public interface IContractorMainViewModel
    {
        Employee CurrentUser { get; }
        IView View { get; }
        ICommand ShowRequestCommand { get; }
        Visibility ButtonVisibility { get; set; }
        string TodayContent { get; set; }
        ObservableCollection<Request> OpenedRequests { get; set; }
        Request SelectedRequest { get; set; }
        ICommand AddRFICommand { get; }
        ICommand RequestsCommand { get; }
        ICommand AddPunchCommand { get; }
        ICommand PunchClosingCommand { get; }
        ICommand ChangePasswordCommand { get; }
        ICommand LogOutCommand { get; }
        bool? ShowDialog();
    }
}
