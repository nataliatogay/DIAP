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
    public interface IAddNewSubactivityViewModel
    {
        Work SelectedWork { get; set; }
        Discipline SelectedDiscipline { get; set; }
        Activity SelectedActivity { get; set; }
        ICollection<Work> WorksList { get; set; }
        ICollection<Discipline> DisciplineList { get; set; }
        ICollection<Activity> ActivitiesList { get; set; }
        ObservableCollection<Subactivity> SubactivitiesList { get; set; }
        ICommand OkCommand { get; }
        ICommand AddSubactivityCommand { get; }
        ICommand DisciplineSelectionChangedCommand { get; }
        ICommand WorkSelectionChangedCommand { get; }
        bool IsActive { get; set; }
        bool? ShowDialog();
    }
}
