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
    public interface IAddNewRequestViewModel
    {
        DateTime? DateToDo { get; set; }
        Employee CurrentUser { get; }
        ICollection<Unit> UnitsList { get; set; }
        ICollection<Work> WorksList { get; set; }
        ICollection<Activity> ActivitiesList { get; set; }
        ICollection<Subactivity> SubactivitiesList { get; set; }
        ICollection<Syst> SystemsList { get; set; }
        ICollection<Subsyst> SubsystemsList { get; set; }
        Unit SelectedUnit { get; set; }
        Work SelectedWork { get; set; }
        Activity SelectedActivity { get; set; }
        Subactivity SelectedSubactivity { get; set; }
        Syst SelectedSystem { get; set; }
        Subsyst SelectedSubsystem { get; set; }
        bool IsActive { get; set; }
        ObservableCollection<Tag> TagsList { get; set; }
        ICommand WorkSelectionChangedCommand { get; }
        ICommand ActivitySelectionChangedCommand { get; }
        ICommand SystemSelectionChangedCommand { get; }
        ICommand OkCommand { get; }
        ICommand AddTagCommand { get; }
        bool? ShowDialog();
        string RequestNo { get; set; }
    }
}
