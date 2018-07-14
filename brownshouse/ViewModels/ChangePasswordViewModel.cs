using brownshouse.Domain.Models;
using brownshouse.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace brownshouse.ViewModels
{
    public class ChangePasswordViewModel : NotifyPropertyChangedObject, IChangePasswordViewModel
    {
        private IBusinessLogic _businessLogic;
        private IChangePasswordView _view;
        public string OldPassword { private get; set; }
        public string NewPassword { private get; set; }
        public string NewPasswordRepeat { private get; set; }
        public Employee CurrentUser { get; private set; }
        private bool isActive;
        public bool IsActive { get { return isActive; } set { isActive = value; OnPropertyChanged(); } }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (this.cancelCommand is null)
                {
                    this.cancelCommand = new RelayCommand(
                        (param) =>
                        {
                            _view.Close(false);
                        }
                    );
                }
                return this.cancelCommand;
            }
        }

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
                            IsActive = false;
                            try
                            {
                                if (NewPassword != NewPasswordRepeat)
                                {
                                    _view.Alert("Repeated password is not the same", "Error");
                                    IsActive = true;
                                    return;
                                }

                                bool res = await _businessLogic.ChangePassword(CurrentUser, OldPassword, NewPassword);
                                if (res)
                                {
                                    var tmp = await _businessLogic.GetEmployeeByLoginAsync(CurrentUser.Login);
                                    CurrentUser.Password = tmp.Password;
                                    _view.Close(true);
                                }
                                else
                                {
                                    _view.Alert("Incorrect login", "Error");
                                    IsActive = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                                IsActive = true;
                            }
                        },
                         (param) =>
                         {
                             return (!String.IsNullOrEmpty(OldPassword) && !String.IsNullOrWhiteSpace(OldPassword) &&
                             !String.IsNullOrEmpty(NewPassword) && !String.IsNullOrWhiteSpace(NewPassword) &&
                             !String.IsNullOrEmpty(NewPasswordRepeat) && !String.IsNullOrWhiteSpace(NewPasswordRepeat));
                         }
                    );
                }
                return this.okCommand;
            }
        }


        public ChangePasswordViewModel(IChangePasswordView view, IBusinessLogic logic, Employee currentUser)
        {
            this._businessLogic = logic;
            this._view = view;
            this._view.SetViewModel(this);
            this.CurrentUser = currentUser;
            this.IsActive = true;
        }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }


}
