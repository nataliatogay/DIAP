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
    public interface IAddNewPunchFromRFIViewModel
    {
        Employee CurrentUser { get; }
        Request SelectedRequest { get; set; }
        Punch SelectedPunch { get; set; }
        ObservableCollection<Punch> TagsList { get; set; }
        ICollection<PunchPriority> PunchesPriorityList { get; set; }
        ICommand OkCommand { get; }
        ICommand AddPunchCommand { get; }
        ICommand RemovePunchCommand { get; }
        bool? ShowDialog();
    }
}
