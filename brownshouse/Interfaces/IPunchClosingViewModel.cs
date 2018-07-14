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
    public interface IPunchClosingViewModel
    {
        Punch SelectedOpenedPunch { get; set; }
        Punch SelectedToClosePunch { get; set; }
        ObservableCollection<Punch> OpenedPunches { get; set; }
        ObservableCollection<Punch> ToClosePunches { get; set; }
        ICommand SelectPunchToCloseCommand { get; }
        ICommand SelectPunchOpenedCommand { get; }
        ICommand SystemSelectionChangedCommand { get; }
        ICommand SubsystemSelectionChangedCommand { get; }
        ICommand DisciplineSelectionChangedCommand { get; }
        ICommand UnitSelectionChangedCommand { get; }
        ICommand IsAllCheckedCommand { get; }
        bool? ShowDialog();
    }
}
