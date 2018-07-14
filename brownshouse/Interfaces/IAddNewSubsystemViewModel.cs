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
    public interface IAddNewSubsystemViewModel
    {
        Syst SelectedSystem { get; set; }
        ICollection<Syst> SystemsList { get; set; }
        ObservableCollection<Subsyst> SubsystemsList { get; set; }
        ICommand OkCommand { get; }
        ICommand AddSubsystemCommand { get; }
        bool IsActive { get; set; }
        bool? ShowDialog();
    }
}
