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
    class AddNewPunchFromRFIViewModel : IAddNewPunchFromRFIViewModel
    {
        private IAddNewPunchFromRFIView _view;
        private IBusinessLogic _businessLogic;
        public Employee CurrentUser { get; private set; }
        public Request SelectedRequest { get; set; }
        public Punch SelectedPunch { get; set; }
        public ObservableCollection<Punch> TagsList { get; set; }
        public ICollection<PunchPriority> PunchesPriorityList { get; set; }

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
                            int emptyDescrCount = TagsList.Where(t => String.IsNullOrEmpty(t.Description) || String.IsNullOrWhiteSpace(t.Description)).Count();
                            int emptyPriorityCount = TagsList.Where(t => t.PunchPriority == null).Count();
                            if (emptyDescrCount > 0)
                            {
                                _view.Alert("There is a blank punch description block in the list", "Error");
                                return;
                            }
                            else if (emptyPriorityCount > 0)
                            {
                                _view.Alert("here is a blank punch priority block in the list", "Error");
                                return;
                            }
                            try
                            {
                                await _businessLogic.AddPunchesAsync(TagsList, SelectedRequest, CurrentUser);
                               
                                this._view.Close(true);
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        },
                        (param) =>
                        {
                            return (TagsList.Count > 0);
                        }
                    );
                }
                return this.okCommand;
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
                            TagsList.Add(new Punch()
                            {
                                Tag = new Tag()
                                {
                                    Id = SelectedPunch.Tag.Id,
                                    IdUnit = SelectedPunch.Tag.IdUnit,
                                    Code = SelectedPunch.Tag.Code,
                                    IdSubsyst = SelectedPunch.Tag.IdSubsyst,
                                    IdDiscipline = SelectedPunch.Tag.IdDiscipline,
                                    Title = SelectedPunch.Tag.Title
                                }
                            });

                        },
                        (param) =>
                        {
                            return (SelectedPunch != null);
                        }
                    );
                }
                return this.addPunchCommand;
            }
        }

        private ICommand removePunchCommand;
        public ICommand RemovePunchCommand
        {
            get
            {
                if (this.removePunchCommand is null)
                {
                    this.removePunchCommand = new RelayCommand(
                        (param) =>
                        {
                            if (SelectedPunch != null)
                            {
                                TagsList.Remove(SelectedPunch);
                                SelectedPunch = null;
                                if(TagsList.Count == 0)
                                {
                                    this._view.Close(false);
                                }
                            }
                        },
                        (param) =>
                        {
                            return SelectedPunch != null;
                        }
                    );
                }
                return this.removePunchCommand;
            }
        }

        public AddNewPunchFromRFIViewModel(IAddNewPunchFromRFIView view, IBusinessLogic logic, Request selectedRequest, ObservableCollection<Punch> tagsList, Employee currentUser)
        {
            this._businessLogic = logic;
            this._view = view;
            this._view.SetViewModel(this);
            this.SelectedRequest = selectedRequest;
            this.TagsList = tagsList;
            try
            {
                PunchesPriorityList = _businessLogic.GetPunchesPriorities();
            }
            catch(Exception ex)
            {
                _view.Alert(ex.Message, "Error");
            }
            
            this.CurrentUser = currentUser;
        }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }
}
