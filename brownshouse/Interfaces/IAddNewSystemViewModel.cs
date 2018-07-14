using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.Interfaces
{
    public interface IAddNewSystemViewModel
    {
        string SystemCode { get; set; }
        string NewSystem { get; set; }
        string SystemDescription { get; set; }
        ICommand OkCommand { get; }
        bool? ShowDialog();
    }
}
