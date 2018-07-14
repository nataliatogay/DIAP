using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.Interfaces
{
    public interface IAddNewInspectionRoleViewModel
    {
        string RoleDescription { get; set; }
        string NewRole { get; set; }
        bool RFIIsRequired { get; set; }
        ICommand OkCommand { get; }
        bool? ShowDialog();
    }
}
