using brownshouse.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.Interfaces
{
    public interface ILoginViewModel
    {
        string Login { get; set; }
        Employee CurrentUser { get; }
        ICommand OkCommand { get; }
        ICommand CancelCommand { get; }
        IView View { get; }
        bool? ShowDialog();
    }
}
