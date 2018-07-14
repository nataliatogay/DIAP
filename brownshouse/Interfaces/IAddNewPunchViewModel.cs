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
    public interface IAddNewPunchViewModel
    {
        Unit SelectedUnit { get; set; }
        Discipline SelectedDiscipline { get; set; }
        Employee CurrentUser { get; }
        //Work SelectedWork { get; set; }
        //Activity SelectedActivity { get; set; }
        //Subactivity SelectedSubactivity { get; set; }
        Syst SelectedSystem { get; set; }
        Subsyst SelectedSubsystem { get; set; }
        ICollection<Discipline> DisciplineList { get; set; }
        ICollection<Unit> UnitsList { get; set; }
        //ICollection<Work> WorksList { get; set; }
        //ICollection<Activity> ActivitiesList { get; set; }
        //ICollection<Subactivity> SubactivitiesList { get; set; }
        ICollection<Syst> SystemsList { get; set; }
        ICollection<Subsyst> SubsystemsList { get; set; }
        ICollection<PunchPriority> PunchesPriorityList { get; set; }
        ObservableCollection<Punch> TagsList { get; set; }
        ICommand AddTagCommand { get; }
        //ICommand DisciplineSelectionChangedCommand { get; }
        //ICommand WorkSelectionChangedCommand { get; }
        //ICommand ActivitySelectionChangedCommand { get; }
        ICommand SystemSelectionChangedCommand { get; }
        ICommand OkCommand { get; }
        bool IsActive { get; set; }
        bool? ShowDialog();
    }
}
