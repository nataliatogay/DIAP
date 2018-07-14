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
    public interface IAddNewITPViewModel
    {
        Work SelectedWork { get; set; }
        Discipline SelectedDiscipline { get; set; }
        Activity SelectedActivity { get; set; }
        ICollection<Work> WorksList { get; set; }
        ICollection<Discipline> DisciplineList { get; set; }
        ICollection<Activity> ActivitiesList { get; set; }
        ICollection<Subactivity> SubactivitiesList { get; set; }
        ICollection<Form> FormsList { get; set; }
        ICollection<InspectionRole> InspectionRolesList { get; set; }
        ObservableCollection<InspectionTestPlan> ITPsList { get; set; }
        ICommand OkCommand { get; }
        ICommand DisciplineSelectionChangedCommand { get; }
        ICommand WorkSelectionChangedCommand { get; }
        ICommand ActivitySelectionChangedCommand { get; }
        bool IsActive { get; set; }
        bool? ShowDialog();
    }
}
