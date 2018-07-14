using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.Interfaces
{
    public interface IAddNewFormViewModel
    {
        string FormTitle { get; set; }
        string FilePath { get; set; }
        ICommand OkCommand { get; }
        ICommand OpenCommand { get; }
        bool? ShowDialog();
    }
}
