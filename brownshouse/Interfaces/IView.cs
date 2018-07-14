using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Interfaces
{
    public interface IView
    {
        bool? ShowDialog();
        void Alert(string message, string caption);
        void Close(bool? dialogResult);
    }

    public interface IMainView : IView
    {
        void SetBindingContext(IMainViewModel viewModel);
    }

    public interface IContractorMainView : IView
    {
        void SetBindingContext(IContractorMainViewModel viewModel);
    }
}
