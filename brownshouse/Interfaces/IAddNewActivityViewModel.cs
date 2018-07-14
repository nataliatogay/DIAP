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
    public interface IAddNewActivityViewModel
    {
        Work SelectedWork { get; set; }
        Discipline SelectedDiscipline { get; set; }
        ICollection<Work> WorksList { get; set; }
        ObservableCollection<Activity> ActivitiesList { get; set; }
        ICollection<Discipline> DisciplineList { get; set; }
        ICommand OkCommand { get; }
        ICommand AddActivityCommand { get; }
        ICommand RemoveActivityCommand { get; }
        ICommand DisciplineSelectionChangedCommand { get; }
        bool IsActive { get; set; }
        bool? ShowDialog();
    }
}
