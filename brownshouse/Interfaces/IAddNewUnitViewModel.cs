using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.Interfaces
{
    public interface IAddNewUnitViewModel
    {
        string UnitCode { get; set; }
        string NewUnit { get; set; }
        ICommand OkCommand { get; }
        bool? ShowDialog();
    }
}
