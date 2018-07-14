using brownshouse.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.ViewModels
{
    public class AddNewInspectionRoleViewModel : IAddNewInspectionRoleViewModel
    {
        private IBusinessLogic _businessLogic;
        private IAddNewInspectionRoleView _view;
        public string RoleDescription { get; set; }
        public string NewRole { get; set; }
        public bool RFIIsRequired { get; set; }
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
                                await _businessLogic.AddNewInspectionRoleAsync(NewRole, RoleDescription, RFIIsRequired);
                                _view.Close(true);
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        },
                        (param) =>
                        {
                            return (!String.IsNullOrWhiteSpace(NewRole) && !String.IsNullOrEmpty(NewRole) &&
                                    !String.IsNullOrWhiteSpace(RoleDescription) && !String.IsNullOrEmpty(RoleDescription));
                        }
                    );
                }
                return this.okCommand;
            }
        }


        public AddNewInspectionRoleViewModel(IAddNewInspectionRoleView view, IBusinessLogic logic)
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
