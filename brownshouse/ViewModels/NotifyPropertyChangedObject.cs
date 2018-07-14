using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.ViewModels
{
    public class NotifyPropertyChangedObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string name = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
