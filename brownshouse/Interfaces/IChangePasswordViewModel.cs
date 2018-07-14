using brownshouse.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.Interfaces
{
    public interface IChangePasswordViewModel
    {
        string OldPassword { set; }
        string NewPassword { set; }
        string NewPasswordRepeat { set; }
        Employee CurrentUser { get; }
        ICommand OkCommand { get; }
        ICommand CancelCommand { get; }
        bool? ShowDialog();
    }
}
