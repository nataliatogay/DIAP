using brownshouse.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.ViewModels
{
    public class AddNewUnitViewModel : IAddNewUnitViewModel
    {
        private IBusinessLogic _businessLogic;
        private IAddNewUnitView _view;
        public string UnitCode { get; set; }
        public string NewUnit { get; set; }

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
                                await _businessLogic.NewUnitAsync(NewUnit, UnitCode);
                                _view.Close(true);
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        },
                        (param) =>
                        {
                            return (!String.IsNullOrWhiteSpace(NewUnit) && !String.IsNullOrEmpty(NewUnit) &&
                                    !String.IsNullOrWhiteSpace(UnitCode) && !String.IsNullOrEmpty(UnitCode));
                        }
                    );
                }
                return this.okCommand;
            }
        }


        public AddNewUnitViewModel(IAddNewUnitView view, IBusinessLogic logic)
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
