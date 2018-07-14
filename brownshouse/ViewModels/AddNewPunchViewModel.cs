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
    public class AddNewPunchViewModel : NotifyPropertyChangedObject, IAddNewPunchViewModel
    {
        private IAddNewPunchView _view;
        private IBusinessLogic _businessLogic;
        public Employee CurrentUser { get; private set; }
        public Unit SelectedUnit { get; set; }
        public Discipline SelectedDiscipline { get; set; }
        public Punch SelectedPunch { get; set; }
        public Syst SelectedSystem { get; set; }
        public Subsyst SelectedSubsystem { get; set; }
        public ICollection<Discipline> DisciplineList { get; set; }
        public ICollection<Unit> UnitsList { get; set; }
        public ICollection<Syst> SystemsList { get; set; }
        private ICollection<Subsyst> subsystemsList;
        public ICollection<Subsyst> SubsystemsList { get { return subsystemsList; } set { subsystemsList = value; OnPropertyChanged(); } }
        public ObservableCollection<Punch> TagsList { get; set; } = new ObservableCollection<Punch>();
        public ICollection<PunchPriority> PunchesPriorityList { get; set; }
        private bool isActive;
        public bool IsActive { get { return isActive; } set { isActive = value; OnPropertyChanged(); } }

        private ICommand addTagCommand;
        public ICommand AddTagCommand
        {
            get
            {
                if (this.addTagCommand is null)
                {
                    this.addTagCommand = new RelayCommand(
                        (param) =>
                        {
                            IsActive = false;
                            TagsList.Add(new Punch()
                            {
                                Tag = new Tag()
                                {
                                    IdSubsyst = SelectedSubsystem.Id,
                                    IdUnit = SelectedUnit.Id,
                                    IdDiscipline = SelectedDiscipline.Id
                                }
                            });
                        },
                        (param) =>
                        {
                            return (SelectedUnit != null && SelectedSubsystem != null);
                        }
                    );
                }
                return this.addTagCommand;
            }
        }

        private ICommand removeTagCommand;
        public ICommand RemoveTagCommand
        {
            get
            {
                if (this.removeTagCommand is null)
                {
                    this.removeTagCommand = new RelayCommand(
                        (param) =>
                        {
                            if (SelectedPunch != null)
                            {
                                TagsList.Remove(SelectedPunch);
                                SelectedPunch = null;
                                if (TagsList.Count == 0)
                                {
                                    IsActive = true;
                                }
                            }
                        },
                        (param) =>
                        {
                            return SelectedPunch != null;
                        }
                    );
                }
                return this.removeTagCommand;
            }
        }

        private ICommand systemSelectionChangedCommand;
        public ICommand SystemSelectionChangedCommand
        {
            get
            {
                if (this.systemSelectionChangedCommand is null)
                {
                    this.systemSelectionChangedCommand = new RelayCommand(
                        async (param) =>
                        {
                            if (SelectedSystem is null)
                            {
                                SubsystemsList = null;
                            }
                            else
                            {
                                try
                                {
                                    SubsystemsList = await _businessLogic.GetSubsystemBySystemAsync(SelectedSystem);
                                }
                                catch (Exception ex)
                                {
                                    _view.Alert(ex.Message, "Error");
                                }
                            }
                        }
                    );
                }
                return this.systemSelectionChangedCommand;
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
                                await _businessLogic.AddPunchesAsync(TagsList, null, CurrentUser);
                                this._view.Close(true);
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        },
                        (param) =>
                        {
                            return (TagsList.Count() > 0)
                            && (TagsList.Where(t => String.IsNullOrEmpty(t.Description) || String.IsNullOrWhiteSpace(t.Description)).Count() == 0)
                            && (TagsList.Where(t => t.PunchPriority == null).Count() == 0);
                        }
                    );
                }
                return this.okCommand;
            }
        }

        public AddNewPunchViewModel(IAddNewPunchView view, IBusinessLogic logic, Employee currentUser)
        {
            this._view = view;
            this._businessLogic = logic;
            this._view.SetViewModel(this);
            this.CurrentUser = currentUser;
            try
            {
                DisciplineList = _businessLogic.GetAllDisciplines().OrderBy(d => d.Title).ToList();
                UnitsList = _businessLogic.GetAllUnits().OrderBy(u => u.Title).ToList();
                SystemsList = _businessLogic.GetAllSystems().OrderBy(s => s.Title).ToList();
                PunchesPriorityList = _businessLogic.GetPunchesPriorities();
            }
            catch (Exception ex)
            {
                _view.Alert(ex.Message, "Error");
            }
            IsActive = true;
        }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }
}
