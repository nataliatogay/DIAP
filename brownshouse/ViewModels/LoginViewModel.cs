using Autofac;
using brownshouse.Domain.Models;
using brownshouse.Interfaces;
using DocumentFormat.OpenXml.Office.CustomXsn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace brownshouse.ViewModels
{
    public class LoginViewModel : NotifyPropertyChangedObject, ILoginViewModel
    {
        private IBusinessLogic _businessLogic;
        private ILoginView _view;
        private string login;
        public string Login { get { return login; } set { login = value; OnPropertyChanged(); } }
        public Employee CurrentUser { get; private set; }

        public IView View
        {
            get
            {
                return this._view;
            }
        }

        private bool isActive;
        public bool IsActive { get { return isActive; } set { isActive = value; OnPropertyChanged(); } }

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
                                var passwordBox = param as PasswordBox;
                                if (passwordBox is null)
                                {
                                    _view.Alert("Password is not entered", "Error");
                                    IsActive = true;
                                    return;
                                }
                                var password = passwordBox.Password;
                                if (String.IsNullOrEmpty(password) || String.IsNullOrWhiteSpace(password))
                                {
                                    _view.Alert("Password is empty", "Error");
                                    IsActive = true;
                                    return;
                                }
                                CurrentUser = await _businessLogic.GetEmployeeAsync(Login, password);
                                if (CurrentUser is null)
                                {
                                    _view.Alert("Incorrect login or password", "Error");
                                    IsActive = true;
                                    return;
                                }
                                _view.Hide();
                                bool? dialRes;
                                if (CurrentUser.User.Organization.OrganizationRole.Role.ToLower() == "administrator")
                                {
                                    var mainViewModel = App.Container.Resolve<IMainViewModel>(new NamedParameter("currentUser", CurrentUser));
                                    dialRes = mainViewModel.ShowDialog();
                                }
                                else
                                {
                                    var mainViewModel = App.Container.Resolve<IContractorMainViewModel>(new NamedParameter("currentUser", CurrentUser));
                                    dialRes = mainViewModel.ShowDialog();
                                }
                                if (dialRes.HasValue && dialRes.Value)
                                {
                                    Login = String.Empty;
                                    _view.Show();
                                    IsActive = true;
                                }
                                else
                                {
                                    _view.Close(true);
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
                            return (!String.IsNullOrEmpty(Login) && !String.IsNullOrWhiteSpace(Login));
                        }
                    );
                }
                return this.okCommand;
            }
        }

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

        private ICommand forgotPasswordCommand;
        public ICommand ForgotPasswordCommand
        {
            get
            {
                if (this.forgotPasswordCommand is null)
                {
                    this.forgotPasswordCommand = new RelayCommand(
                        async (param) =>
                        {
                            if (String.IsNullOrEmpty(Login) || String.IsNullOrWhiteSpace(Login))
                            {
                                _view.Alert("Enter your login then press button \'Forgot password\'", "Forgot password");
                                return;
                            }
                            else
                            {
                                var resp = _view.AlertQuestion($"Is entered login \'{Login}\' correct?", "Forgot password");
                                if (resp == System.Windows.MessageBoxResult.No)
                                {
                                    return;
                                }
                            }

                            try
                            {
                                CurrentUser = await _businessLogic.GetEmployeeByLoginAsync(Login);
                                if (CurrentUser is null)
                                {
                                    _view.Alert("Incorrect login", "Error");
                                    return;
                                }
                                int code = _businessLogic.SendVerificationCode(CurrentUser.Email);
                                _view.Alert("Verification code was sent on your email address", "Forgot password");
                                var forgotPasswordViewModel = App.Container.Resolve<IForgotPasswordViewModel>(new NamedParameter("verificationCodeCorrect", code), new NamedParameter("currentUser", CurrentUser));
                                bool? dialRes = forgotPasswordViewModel.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }

                            //_view.Close(false);
                        }
                    );
                }
                return this.forgotPasswordCommand;
            }
        }

        public LoginViewModel(ILoginView view, IBusinessLogic logic)
        {
            this._businessLogic = logic;
            this._view = view;
            this._view.SetBindingContext(this);
            IsActive = true;
        }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }
}
