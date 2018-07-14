using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.Interfaces
{
    public interface IAddNewDisciplineViewModel
    {
        string DisciplineCode { get; set; }
        string NewDiscipline { get; set; }
        ICommand OkCommand { get; }
        bool? ShowDialog();
    }
}
