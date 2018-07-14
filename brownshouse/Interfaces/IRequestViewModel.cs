using brownshouse.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.Interfaces
{
    public interface IRequestViewModel
    {
        bool? ShowDialog();
        ObservableCollection<Request> OpenedRequests { get; set; }
        Request SelectedRequest { get; set; }
        Employee CurrentUser { get; }
        ICommand OpenedRequestsCommand { get; }
        ICommand AllRequestsCommand { get; }
        ICommand MyRequestsCommand { get; }
        ICommand MyRaisedRequestsCommand { get; }
        ICommand ShowRequestCommand { get; }
    }
}
