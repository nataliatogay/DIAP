using Autofac;
using brownshouse.Domain.Models;
using brownshouse.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.ViewModels
{
    public class RequestsViewModel : NotifyPropertyChangedObject, IRequestViewModel
    {
        private IRequestView _view;
        private IBusinessLogic _businessLogic;
        private ObservableCollection<Request> openedRequests { get; set; }
        public ObservableCollection<Request> OpenedRequests { get { return openedRequests; } set { openedRequests = value; OnPropertyChanged(); } }

        public Request SelectedRequest { get; set; }
        public Employee CurrentUser { get; private set; }

        private ICommand openedRequestsCommand;
        public ICommand OpenedRequestsCommand
        {
            get
            {
                if (this.openedRequestsCommand is null)
                {
                    this.openedRequestsCommand = new RelayCommand(
                        async (param) =>
                        {
                            try
                            {
                                OpenedRequests = new ObservableCollection<Request>(await _businessLogic.GetOpenedRequestsAsync());
                                allRequests = false;
                                myRequests = false;
                                openRequests = true;
                                raisedRequests = false;
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        }
                    );
                }
                return this.openedRequestsCommand;
            }
        }

        private ICommand allRequestsCommand;
        public ICommand AllRequestsCommand
        {
            get
            {
                if (this.allRequestsCommand is null)
                {
                    this.allRequestsCommand = new RelayCommand(
                        async (param) =>
                        {
                            try
                            {
                                OpenedRequests = new ObservableCollection<Request>(await _businessLogic.GetAllRequestsAsync());
                                allRequests = true;
                                myRequests = false;
                                openRequests = false;
                                raisedRequests = false;
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        }
                    );
                }
                return this.allRequestsCommand;
            }
        }

        private ICommand myRequestsCommand;
        public ICommand MyRequestsCommand
        {
            get
            {
                if (this.myRequestsCommand is null)
                {
                    this.myRequestsCommand = new RelayCommand(
                        async (param) =>
                        {
                            try
                            {
                                OpenedRequests = new ObservableCollection<Request>(await _businessLogic.GetOpenedRequestByUser(CurrentUser));
                                allRequests = false;
                                myRequests = true;
                                openRequests = false;
                                raisedRequests = false;
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        }
                    );
                }
                return this.myRequestsCommand;
            }
        }

        private ICommand myRaisedRequestsCommand;
        public ICommand MyRaisedRequestsCommand
        {
            get
            {
                if (this.myRaisedRequestsCommand is null)
                {
                    this.myRaisedRequestsCommand = new RelayCommand(
                        async (param) =>
                        {
                            try
                            {
                                OpenedRequests = new ObservableCollection<Request>(await _businessLogic.GetRaisedRequestByUserAsync(CurrentUser));
                                allRequests = false;
                                myRequests = false;
                                openRequests = false;
                                raisedRequests = true;
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        }
                    );
                }
                return this.myRaisedRequestsCommand;
            }
        }

        private ICommand showRequestCommand;
        public ICommand ShowRequestCommand
        {
            get
            {
                if (this.showRequestCommand is null)
                {
                    this.showRequestCommand = new RelayCommand(
                        async (param) =>
                        {
                            var reqClosingViewModel = App.Container.Resolve<IRequestClosingViewModel>(new NamedParameter("selectedRequest", SelectedRequest), new NamedParameter("currentUser", CurrentUser));
                            reqClosingViewModel.ShowDialog();
                           
                            if (allRequests)
                            {
                                OpenedRequests = new ObservableCollection<Request>(await _businessLogic.GetAllRequestsAsync());
                            }
                            else if (myRequests)
                            {
                                OpenedRequests = new ObservableCollection<Request>(await _businessLogic.GetOpenedRequestByUser(CurrentUser));
                            }
                            else if (openRequests)
                            {
                                OpenedRequests = new ObservableCollection<Request>(await _businessLogic.GetOpenedRequestsAsync());
                            }
                            else
                            {
                                OpenedRequests = new ObservableCollection<Request>(await _businessLogic.GetRaisedRequestByUserAsync(CurrentUser));
                            }
                        }
                    );
                }
                return this.showRequestCommand;
            }
        }

        private bool allRequests;
        private bool myRequests;
        private bool openRequests;
        private bool raisedRequests;

        public RequestsViewModel(IRequestView view, IBusinessLogic logic, Employee currentUser)
        {
            this._businessLogic = logic;
            this._view = view;
            this._view.SetViewModel(this);
            this.CurrentUser = currentUser;
            try
            {
                Config();
            }
            catch (Exception ex)
            {
                _view.Alert(ex.Message, "Error");
            }
        }

        private void Config()
        {
            try
            {
                OpenedRequests = new ObservableCollection<Request>(_businessLogic.GetOpenedRequests());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            allRequests = false;
            myRequests = false;
            openRequests = true;
            raisedRequests = false;
        }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }
}
