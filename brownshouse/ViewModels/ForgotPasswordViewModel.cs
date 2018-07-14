using brownshouse.Domain.Models;
using brownshouse.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace brownshouse.ViewModels
{
    public class ForgotPasswordViewModel : NotifyPropertyChangedObject, IForgotPasswordViewModel
    {
        private IBusinessLogic _businessLogic;
        private IForgotPasswordView _view;
        public string NewPassword { private get; set; }
        public string NewPasswordRepeat { private get; set; }
        private string verificationCode;
        private int verificationCodeCorrect;
        public string VerificationCode { get { return verificationCode; } set { verificationCode = value; OnPropertyChanged(); } }
        public Employee CurrentUser { get; set; }
        private Visibility passwordVisibility;
        public Visibility PasswordVisibility
        {
            get { return passwordVisibility; }
            set { passwordVisibility = value; OnPropertyChanged(); }
        }
        private int attemptLeft;

        private ICommand verifyCommand;
        public ICommand VerifyCommand
        {
            get
            {
                if (this.verifyCommand is null)
                {
                    this.verifyCommand = new RelayCommand(
                        (param) =>
                        {
                            int code;
                            var res = Int32.TryParse(VerificationCode, out code);
                            if (!res || code != verificationCodeCorrect)
                            {
                                if (attemptLeft == 1)
                                {
                                    _view.Alert($"You've entered incorrect verification code for 3 times.\n Try later", "Error");
                                    _view.Close(false);
                                }
                                else
                                {
                                    _view.Alert($"Incorrect verification code. Attempt left:{--attemptLeft}", "Error");
                                    return;
                                }
                                
                            }
                            else
                            {
                                PasswordVisibility = Visibility.Visible;
                            }
                        },
                        (param) =>
                        {
                            return (!String.IsNullOrEmpty(VerificationCode) &&
                                    !String.IsNullOrWhiteSpace(VerificationCode) &&
                                    VerificationCode.Length > 5);
                        }
                    );
                }
                return this.verifyCommand;
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
                            try
                            {
                                if (NewPassword != NewPasswordRepeat)
                                {
                                    _view.Alert("Repeated password is not the same", "Error");
                                    return;
                                }

                                await _businessLogic.ForgotPassword(CurrentUser, NewPassword);
                                var tmp = await _businessLogic.GetEmployeeByLoginAsync(CurrentUser.Login);
                                CurrentUser.Password = tmp.Password;
                                _view.Close(true);
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        }
                    );
                }
                return this.okCommand;
            }
        }


        public ForgotPasswordViewModel(IForgotPasswordView view, IBusinessLogic logic, int verificationCodeCorrect, Employee currentUser)
        {
            this._businessLogic = logic;
            this._view = view;
            this.CurrentUser = currentUser;
            this._view.SetViewModel(this);
            PasswordVisibility = Visibility.Collapsed;
            this.verificationCodeCorrect = verificationCodeCorrect;
            this.attemptLeft = 3;
        }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }
}
