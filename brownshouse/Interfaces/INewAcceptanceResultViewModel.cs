using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.Interfaces
{
    public interface INewAcceptanceResultViewModel
    {
        string NewResult { get; set; }
        ICommand OkCommand { get; }
        bool? ShowDialog();
    }
}
