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
    public class ProgramMailViewModel : NotifyPropertyChangedObject, IProgramMailViewModel
    {
        private IBusinessLogic _businessLogic;
        private IProgramMailView _view;
        public string NewPassword { private get; set; }
        public string NewPasswordRepeat { private get; set; }
        public ProgramMail MailAddress { get; set; }
        public string Address { get; set; }
        private Visibility createButtonVisibility;
        public Visibility CreateButtonVisibility { get { return createButtonVisibility; } set { createButtonVisibility = value; OnPropertyChanged(); } }
        private Visibility editButtonVisibility;
        public Visibility EditButtonVisibility { get { return editButtonVisibility; } set { editButtonVisibility = value; OnPropertyChanged(); } }
        private Visibility passwordVisibility;
        public Visibility PasswordVisibility { get { return passwordVisibility; } set { passwordVisibility = value; OnPropertyChanged(); } }
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
                                MailAddress.Address = this.Address;
                                await _businessLogic.CreateProgramMail(MailAddress, NewPassword);
                                _view.Close(true);
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                                IsActive = true;
                            }
                        },
                         (param) =>
                         {
                             return (!String.IsNullOrEmpty(NewPassword) && !String.IsNullOrWhiteSpace(NewPassword) &&
                                     !String.IsNullOrEmpty(NewPasswordRepeat) && !String.IsNullOrWhiteSpace(NewPasswordRepeat));
                         }
                    );
                }
                return this.okCommand;
            }
        }

        private ICommand createEmailCommand;
        public ICommand CreateEmailCommand
        {
            get
            {
                if (this.createEmailCommand is null)
                {
                    this.createEmailCommand = new RelayCommand(
                        (param) =>
                        {
                            MailAddress = new ProgramMail();
                            IsActive = true;
                            PasswordVisibility = Visibility.Visible;
                        }
                    );
                }
                return this.createEmailCommand;
            }
        }

        private ICommand editEmailCommand;
        public ICommand EditEmailCommand
        {
            get
            {
                if (this.editEmailCommand is null)
                {
                    this.editEmailCommand = new RelayCommand(
                        (param) =>
                        {
                            _view.Close(false);
                        }
                    );
                }
                return this.editEmailCommand;
            }
        }

        public ProgramMailViewModel(IProgramMailView view, IBusinessLogic logic)
        {
            this._businessLogic = logic;
            this._view = view;
            this._view.SetViewModel(this);
            this.IsActive = false;
            try
            {
                MailAddress = _businessLogic.GetProgramMail();
            }
            catch (Exception ex)
            {
                _view.Alert(ex.Message, "Error");
            }
            if (MailAddress is null)
            {
                CreateButtonVisibility = Visibility.Visible;
                EditButtonVisibility = Visibility.Collapsed;
            }
            else
            {
                this.Address = MailAddress.Address;
                CreateButtonVisibility = Visibility.Collapsed;
                EditButtonVisibility = Visibility.Visible;
            }
            PasswordVisibility = Visibility.Collapsed;
        }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }
}
