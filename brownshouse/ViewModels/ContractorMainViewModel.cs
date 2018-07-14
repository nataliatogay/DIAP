using Autofac;
using brownshouse.Domain.Models;
using brownshouse.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace brownshouse.ViewModels
{
    public class ContractorMainViewModel : NotifyPropertyChangedObject, IContractorMainViewModel
    {
        private IBusinessLogic _businessLogic;
        private IContractorMainView _view;
        public Visibility ButtonVisibility { get; set; }
        public string TodayContent { get; set; }
        private ObservableCollection<Request> openedRequests { get; set; }
        public ObservableCollection<Request> OpenedRequests { get { return openedRequests; } set { openedRequests = value; OnPropertyChanged(); } }
        public Request SelectedRequest { get; set; }

        public Employee CurrentUser { get; private set; }

        private ICommand showRequestCommand;
        public ICommand ShowRequestCommand
        {
            get
            {
                if (this.showRequestCommand is null)
                {
                    this.showRequestCommand = new RelayCommand(
                        (param) =>
                        {
                            var reqClosingViewModel = App.Container.Resolve<IRequestClosingViewModel>(new NamedParameter("selectedRequest", SelectedRequest), new NamedParameter("currentUser", CurrentUser));
                            reqClosingViewModel.ShowDialog();
                            try
                            {
                                OpenedRequests = new ObservableCollection<Request>(_businessLogic.GetOpenedRequestForDay(DateTime.Now, CurrentUser));
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        }
                    );
                }
                return this.showRequestCommand;
            }
        }

        private ICommand addRFICommand;
        public ICommand AddRFICommand
        {
            get
            {
                if (this.addRFICommand is null)
                {
                    this.addRFICommand = new RelayCommand(
                       (param) =>
                       {
                           var addRFIViewModel = App.Container.Resolve<IAddNewRequestViewModel>(new NamedParameter("currentUser", CurrentUser));
                           addRFIViewModel.ShowDialog();

                       }
                    );
                }
                return this.addRFICommand;
            }
        }

        private ICommand requestsCommand;
        public ICommand RequestsCommand
        {
            get
            {
                if (this.requestsCommand is null)
                {
                    this.requestsCommand = new RelayCommand(
                       (param) =>
                       {
                           var requestsViewModel = App.Container.Resolve<IRequestViewModel>(new NamedParameter("currentUser", CurrentUser));
                           requestsViewModel.ShowDialog();
                           try
                           {
                               OpenedRequests = new ObservableCollection<Request>(_businessLogic.GetOpenedRequestForDay(DateTime.Now, CurrentUser));
                           }
                           catch (Exception ex)
                           {
                               _view.Alert(ex.Message, "Error");
                           }
                       }
                    );
                }
                return this.requestsCommand;
            }
        }

        private ICommand addPunchCommand;
        public ICommand AddPunchCommand
        {
            get
            {
                if (this.addPunchCommand is null)
                {
                    this.addPunchCommand = new RelayCommand(
                       (param) =>
                       {
                           var addPunchViewModel = App.Container.Resolve<IAddNewPunchViewModel>(new NamedParameter("currentUser", CurrentUser));
                           addPunchViewModel.ShowDialog();

                       }
                    );
                }
                return this.addPunchCommand;
            }
        }

        private ICommand punchClosingCommand;
        public ICommand PunchClosingCommand
        {
            get
            {
                if (this.punchClosingCommand is null)
                {
                    this.punchClosingCommand = new RelayCommand(
                       (param) =>
                       {
                           var punchClosingViewModel = App.Container.Resolve<IPunchClosingViewModel>(new NamedParameter("currentUser", CurrentUser));
                           punchClosingViewModel.ShowDialog();

                       }
                    );
                }
                return this.punchClosingCommand;
            }
        }

        private ICommand changePasswordCommand;
        public ICommand ChangePasswordCommand
        {
            get
            {
                if (this.changePasswordCommand is null)
                {
                    this.changePasswordCommand = new RelayCommand(
                       (param) =>
                       {
                           var changePasswordViewModel = App.Container.Resolve<IChangePasswordViewModel>(new NamedParameter("currentUser", CurrentUser));
                           var res = changePasswordViewModel.ShowDialog();
                       }
                    );
                }
                return this.changePasswordCommand;
            }
        }

        private ICommand logOutCommand;
        public ICommand LogOutCommand
        {
            get
            {
                if (this.logOutCommand is null)
                {
                    this.logOutCommand = new RelayCommand(
                       (param) =>
                       {
                           _view.Close(true);
                       }
                    );
                }
                return this.logOutCommand;
            }
        }


        public IView View { get { return this._view; } }

        public ContractorMainViewModel(IContractorMainView view, IBusinessLogic logic, Employee currentUser)
        {
            this._view = view;
            this._view.SetBindingContext(this);
            this._businessLogic = logic;
            this.CurrentUser = currentUser;
            if (CurrentUser.User.Organization.OrganizationRole.Role.ToLower() == "subcontractor")
            {
                ButtonVisibility = Visibility.Visible;
            }
            else
            {
                ButtonVisibility = Visibility.Collapsed;
            }
            this.TodayContent = $"Today  ({DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year})";
            this.OpenedRequests = new ObservableCollection<Request>(_businessLogic.GetOpenedRequestForDay(DateTime.Now, CurrentUser));
        }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }
}
