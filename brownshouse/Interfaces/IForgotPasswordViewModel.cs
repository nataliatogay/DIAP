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
    public interface IForgotPasswordViewModel
    {
        bool? ShowDialog();
        string NewPassword { set; }
        string NewPasswordRepeat { set; }
        string VerificationCode { get; set; }
        Employee CurrentUser { get; set; }
        Visibility PasswordVisibility { get; set; }
        ICommand VerifyCommand { get; }
        ICommand OkCommand { get; }
    }
}
