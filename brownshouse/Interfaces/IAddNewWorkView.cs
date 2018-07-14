using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Interfaces
{
    public interface IAddNewWorkView : IView
    {
        void SetViewModel(IAddNewWorkViewModel viewModel);
    }
}
