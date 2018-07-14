using brownshouse.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.ViewModels
{
    class NewAcceptanceResultViewModel : INewAcceptanceResultViewModel
    {
        private IBusinessLogic _businessLogic;
        private INewAcceptanceResultView _view;
        public string NewResult { get; set; }

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
                                await _businessLogic.NewAcceptanceResultAsync(NewResult);
                                _view.Close(true);
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        },
                        (param) =>
                        {
                            return (!String.IsNullOrWhiteSpace(NewResult) && !String.IsNullOrEmpty(NewResult));
                        }
                    );
                }
                return this.okCommand;
            }
        }

        public NewAcceptanceResultViewModel(INewAcceptanceResultView view, IBusinessLogic logic)
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
