using brownshouse.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.ViewModels
{
    public class AddNewSystemViewModel : IAddNewSystemViewModel
    {
        private IBusinessLogic _businessLogic;
        private IAddNewSystemView _view;
        public string SystemCode { get; set; }
        public string NewSystem { get; set; }
        public string SystemDescription { get; set; }

        private ICommand okCommand;
        public ICommand OkCommand
        {
            get
            {
                if (this.okCommand is null)
                {
                    this.okCommand = new RelayCommand(
                        async (param) =>
                        {
                            try
                            {
                               await _businessLogic.AddSystemAsync(NewSystem, SystemCode, SystemDescription);
                                _view.Close(true);
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        },
                        (param) =>
                        {
                            return (!String.IsNullOrWhiteSpace(NewSystem) && !String.IsNullOrEmpty(NewSystem) &&
                                    !String.IsNullOrWhiteSpace(SystemCode) && !String.IsNullOrEmpty(SystemCode) &&
                                    !String.IsNullOrWhiteSpace(SystemDescription) && !String.IsNullOrEmpty(SystemDescription));
                        }
                    );
                }
                return this.okCommand;
            }
        }


        public AddNewSystemViewModel(IAddNewSystemView view, IBusinessLogic logic)
        {
            this._businessLogic = logic;
            this._view = view;
            this._view.SetViewModel(this);
        }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }
}
