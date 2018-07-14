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
    public interface IProgramMailViewModel
    {
        string NewPassword { set; }
        string NewPasswordRepeat { set; }
        ProgramMail MailAddress { get; }
        ICommand CreateEmailCommand { get; }
        ICommand EditEmailCommand { get; }
        ICommand OkCommand { get; }
        ICommand CancelCommand { get; }
        Visibility CreateButtonVisibility { get; set; }
        Visibility EditButtonVisibility { get; set; }
        Visibility PasswordVisibility { get; set; }
        bool IsActive { get; set; }
        bool? ShowDialog();
    }
}
