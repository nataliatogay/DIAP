using brownshouse.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.Interfaces
{
    public interface IAddNewEmployeeViewModel
    {
        string NewLastName { get; set; }
        string NewFirstName { get; set; }
        string NewMail { get; set; }
        string NewLogin { get; set; }
        User SelectedUser { get; set; }
        IEnumerable<User> UsersList { get; set; }
        ICommand OkCommand { get; }
        bool? ShowDialog();
    }
}
