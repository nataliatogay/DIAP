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
    public class RequestClosingViewModel : NotifyPropertyChangedObject, IRequestClosingViewModel
    {
        private IBusinessLogic _businessLogic;
        private IRequestClosingView _view;
        public Request SelectedRequest { get; set; }
        public Employee CurrentUser { get; private set; }
        public ICollection<TagPunch> TagsList { get; set; }
        public string RequestNo { get; set; }
        private ICommand acceptedCommand;
        public ICommand AcceptedCommand
        {
            get
            {
                if (this.acceptedCommand is null)
                {
                    this.acceptedCommand = new RelayCommand(
                        async (param) =>
                        {
                            try
                            {
                                int count = TagsList.Where(t => t.IsRequiredPunch).Count();
                                if (count == 0)
                                {
                                    SelectedRequest = await _businessLogic.ClosingRequestAsync(SelectedRequest.Id, true, null, CurrentUser);
                                    _view.Alert("RFI closed with result \'accepted\'", "Closing");
                                    //_view.Close(true);
                                }
                                else
                                {
                                    ObservableCollection<Punch> TagsPunch = new ObservableCollection<Punch>();
                                    foreach (var item in TagsList)
                                    {
                                        if (item.IsRequiredPunch)
                                        {
                                            TagsPunch.Add(new Punch() { Tag = item.tag });
                                        }
                                    }
                                    var reqAddNewPunchViewModel = App.Container.Resolve<IAddNewPunchFromRFIViewModel>(new NamedParameter("selectedRequest", SelectedRequest), new NamedParameter("tagsList", TagsPunch), new NamedParameter("currentUser", CurrentUser));
                                    bool? res = reqAddNewPunchViewModel.ShowDialog();
                                    if (res.HasValue && res.Value)
                                    {
                                        SelectedRequest = await _businessLogic.ClosingRequestAsync(SelectedRequest.Id, true, TagsPunch, CurrentUser);
                                        _view.Alert($"RFI closed with result \'accepted with punch(es)\'", "Closing");
                                    }
                                }
                               
                                string userRole = CurrentUser.User.Organization.OrganizationRole.Role.ToLower();
                                string address = String.Empty;
                                if (userRole == "subcontractor")
                                {
                                    address = SelectedRequest.ResponsibleContractor.Email;
                                }
                                else if (userRole == "contractor")
                                {
                                    if (SelectedRequest.ResponsibleOwner != null)
                                    {
                                        address = SelectedRequest.ResponsibleOwner.Email;
                                    }
                                    else
                                    {
                                        address = SelectedRequest.RaisedByEmployee.Email;
                                    }
                                }
                                else if (userRole == "owner")
                                {
                                    if (SelectedRequest.ResponsibleThirdParty != null)
                                    {
                                        address = SelectedRequest.ResponsibleThirdParty.Email;
                                    }
                                    else
                                    {
                                        address = SelectedRequest.RaisedByEmployee.Email;
                                    }
                                }
                                if (String.IsNullOrEmpty(address))
                                {
                                    _view.Close(true);
                                    return;
                                }
                                await _businessLogic.SendNotificationResultRFI(address, SelectedRequest, CurrentUser);
                                _view.Close(true);

                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        }
                    );
                }
                return this.acceptedCommand;
            }
        }

        private ICommand rejectedCommand;
        public ICommand RejectedCommand
        {
            get
            {
                if (this.rejectedCommand is null)
                {
                    this.rejectedCommand = new RelayCommand(
                        async (param) =>
                        {
                            try
                            {
                                SelectedRequest = await _businessLogic.ClosingRequestAsync(SelectedRequest.Id, false, null, CurrentUser);
                                _view.Alert("RFI closed with result \'rejected\'", "Closing");
                                string address = SelectedRequest.RaisedByEmployee.Email;
                                await _businessLogic.SendNotificationResultRFI(address, SelectedRequest, CurrentUser);

                                _view.Close(true);
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        },
                        (param) =>
                        {
                            return (TagsList.Where(t => t.IsRequiredPunch).Count() == 0);
                        }
                    );
                }
                return this.rejectedCommand;
            }
        }
        private Visibility closedVisibility;
        public Visibility ClosedVisibility { get { return closedVisibility; } set { closedVisibility = value; OnPropertyChanged(); } }
        private Visibility resultVisibility;
        public Visibility ResultVisibility { get { return resultVisibility; } set { resultVisibility = value; OnPropertyChanged(); } }



        public RequestClosingViewModel(IRequestClosingView view, IBusinessLogic logic, Request selectedRequest, Employee currentUser)
        {
            this._businessLogic = logic;
            this._view = view;
            this._view.SetViewModel(this);
            TagsList = new List<TagPunch>();
            this.SelectedRequest = selectedRequest;
            this.CurrentUser = currentUser;
            foreach (var item in SelectedRequest.TagsRequests)
            {
                TagsList.Add(new TagPunch() { tag = item.Tag, IsRequiredPunch = false });
            }
            if (SelectedRequest.Status is null)
            {
                ClosedVisibility = Visibility.Visible;
                ResultVisibility = Visibility.Collapsed;
            }
            else
            {
                ClosedVisibility = Visibility.Collapsed;
                ResultVisibility = Visibility.Visible;
            }
            RequestNo = $"{CurrentUser.User.Discipline.Code} - {_businessLogic.GetNextRequestNo()}";
        }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }

        public class TagPunch
        {
            public Tag tag { get; set; }
            public bool IsRequiredPunch { get; set; }
        }
    }
}
