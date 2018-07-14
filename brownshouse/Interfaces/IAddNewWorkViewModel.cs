using brownshouse.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.Interfaces
{
    public interface IAddNewWorkViewModel
    {
        string NewWork { get; set; }
        Discipline SelectedDiscipline { get; set; }
        IEnumerable<Discipline> DisciplinesList { get; set; }
        ICommand OkCommand { get; }
        bool? ShowDialog();
    }
}
