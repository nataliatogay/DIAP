using brownshouse.Domain.Models;
using brownshouse.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace brownshouse.ViewModels
{
    class AddNewEmployeeViewModel : IAddNewEmployeeViewModel
    {
        private IAddNewEmployeeView _view;
        private IBusinessLogic _businessLogic;
        public string NewLastName { get; set; }
        public string NewFirstName { get; set; }
        public string NewMail { get; set; }
        public string NewLogin { get; set; }
        public IEnumerable<User> UsersList { get; set; }
        public User SelectedUser { get; set; }

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
                                var passwordBox = param as PasswordBox;
                                if (passwordBox is null)
                                {
                                    _view.Alert("Password is not entered", "Error");
                                    return;
                                }
                                var password = passwordBox.Password;
                                if (String.IsNullOrEmpty(password) || String.IsNullOrWhiteSpace(password))
                                {
                                    _view.Alert("Password is empty", "Error");
                                    return;
                                }
                                await _businessLogic.AddNewEmployeeAsync(NewLastName, NewFirstName, NewMail, NewLogin, password, SelectedUser);
                                _view.Close(true);
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        },
                        (param) =>
                        {
                            return (!String.IsNullOrWhiteSpace(NewFirstName) && !String.IsNullOrEmpty(NewFirstName) &&
                                    !String.IsNullOrWhiteSpace(NewLastName) && !String.IsNullOrEmpty(NewLastName) &&
                                    !String.IsNullOrWhiteSpace(NewMail) && !String.IsNullOrEmpty(NewMail) &&
                                    !String.IsNullOrWhiteSpace(NewLogin) && !String.IsNullOrEmpty(NewLogin) &&
                                    SelectedUser != null);
                        }
                    );
                }
                return this.okCommand;
            }
        }

        public AddNewEmployeeViewModel(IAddNewEmployeeView view, IBusinessLogic logic)
        {
            this._businessLogic = logic;
            this._view = view;
            this._view.SetViewModel(this);
            try
            {
                UsersList = _businessLogic.GetAllUsers().OrderBy(d => d.Abbreviation).ToList();
            }
            catch (Exception ex)
            {
                _view.Alert(ex.Message, "Error");
            }
        }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }
}
