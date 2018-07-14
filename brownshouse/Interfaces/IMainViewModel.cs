using brownshouse.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.Interfaces
{
    public interface IMainViewModel
    {
        ICommand AddAcceptanceCommand { get; }
        ICommand AddDisciplineCommand { get; }
        ICommand AddWorkCommand { get; }
        ICommand AddActivityCommand { get; }
        ICommand AddSubactivityCommand { get; }
        ICommand AddInspectionRoleCommand { get; }
        ICommand AddFormCommand { get; }
        ICommand AddITPCommand { get; }
        Employee CurrentUser { get; }
        ICommand AddRFICommand { get; }
        ICommand AddSystemCommand { get; }
        ICommand AddSubsystemCommand { get; }
        ICommand RequestsCommand { get; }
        ICommand AddPunchCommand { get; }
        ICommand PunchClosingCommand { get; }
        ICommand AddEmployeeCommand { get; }
        ICommand ChangePasswordCommand { get; }
        ICommand ProgramMailCommand { get; }
        ICommand LogOutCommand { get; }
        IView View { get; }
        bool? ShowDialog();
    }
}
