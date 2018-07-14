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
    public class PunchClosingViewModel : NotifyPropertyChangedObject, IPunchClosingViewModel
    {
        private IBusinessLogic _businessLogic;
        private IPunchClosingView _view;
        public Employee CurrentUser { get; private set; }
        private Punch selectedOpenedPunch;
        public Punch SelectedOpenedPunch
        {
            get
            {
                return selectedOpenedPunch;
            }
            set
            {
                selectedOpenedPunch = value;
                OnPropertyChanged();
            }
        }

        public Punch SelectedToClosePunch { get; set; }
        public Discipline SelectedDiscipline { get; set; }
        public Unit SelectedUnit { get; set; }
        public Syst SelectedSystem { get; set; }
        public Subsyst SelectedSubsystem { get; set; }

        public ICollection<Discipline> DisciplineList { get; set; }
        public ICollection<Unit> UnitsList { get; set; }
        public ICollection<Syst> SystemsList { get; set; }
        private ICollection<Subsyst> subsystemsList;
        public ICollection<Subsyst> SubsystemsList { get { return subsystemsList; } set { subsystemsList = value; OnPropertyChanged(); } }

        private ObservableCollection<Punch> allPunches;
        public ObservableCollection<Punch> AllPunches
        {
            get
            {
                return allPunches;
            }
            set
            {
                allPunches = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Punch> OpenedPunches { get; set; }
        public ObservableCollection<Punch> ToClosePunches { get; set; } = new ObservableCollection<Punch>();
        public bool IsAll { get; set; } = false;
        private bool isActive;
        public bool IsActive { get { return isActive; } set { isActive = value; OnPropertyChanged(); } }

        private PunchReport openedPunchesReport;
        public PunchReport OpenedPunchesReport { get { return openedPunchesReport; } set { openedPunchesReport = value; OnPropertyChanged(); } }
        private PunchReport closedPunchesReport;
        public PunchReport ClosedPunchesReport { get { return closedPunchesReport; } set { closedPunchesReport = value; OnPropertyChanged(); } }
        private Visibility deleteVisibility;
        public Visibility DeleteVisibility
        {
            get { return deleteVisibility; }
            set { deleteVisibility = value; OnPropertyChanged(); }
        }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        private ICommand isAllCheckedCommand;
        public ICommand IsAllCheckedCommand
        {
            get
            {
                if (this.isAllCheckedCommand is null)
                {
                    this.isAllCheckedCommand = new RelayCommand(
                        async (param) =>
                        {
                            try
                            {
                                AllPunches = new ObservableCollection<Punch>(await _businessLogic.GetAllPunchesByParamsAsync(SelectedDiscipline, SelectedUnit, SelectedSystem, SelectedSubsystem, IsAll));
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        }
                    );
                }
                return this.isAllCheckedCommand;
            }
        }

        private ICommand unitSelectionChangedCommand;
        public ICommand UnitSelectionChangedCommand
        {
            get
            {
                if (this.unitSelectionChangedCommand is null)
                {
                    this.unitSelectionChangedCommand = new RelayCommand(
                        async (param) =>
                        {
                            try
                            {
                                AllPunches = new ObservableCollection<Punch>(await _businessLogic.GetAllPunchesByParamsAsync(SelectedDiscipline, SelectedUnit, SelectedSystem, SelectedSubsystem, IsAll));
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        }
                    );
                }
                return this.unitSelectionChangedCommand;
            }
        }

        private ICommand disciplineSelectionChangedCommand;
        public ICommand DisciplineSelectionChangedCommand
        {
            get
            {
                if (this.disciplineSelectionChangedCommand is null)
                {
                    this.disciplineSelectionChangedCommand = new RelayCommand(
                        async (param) =>
                        {
                            try
                            {
                                AllPunches = new ObservableCollection<Punch>(await _businessLogic.GetAllPunchesByParamsAsync(SelectedDiscipline, SelectedUnit, SelectedSystem, SelectedSubsystem, IsAll));
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        }
                    );
                }
                return this.disciplineSelectionChangedCommand;
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
                            try
                            {
                                AllPunches = new ObservableCollection<Punch>(await _businessLogic.GetAllPunchesByParamsAsync(SelectedDiscipline, SelectedUnit, SelectedSystem, SelectedSubsystem, IsAll));
                                if (SelectedSystem is null || SelectedSystem.Id == 0)
                                {
                                    SubsystemsList = null;
                                }
                                else
                                {
                                    SubsystemsList = await _businessLogic.GetSubsystemBySystemAsync(SelectedSystem);
                                }
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        }
                    );
                }
                return this.systemSelectionChangedCommand;
            }
        }

        private ICommand subsystemSelectionChangedCommand;
        public ICommand SubsystemSelectionChangedCommand
        {
            get
            {
                if (this.subsystemSelectionChangedCommand is null)
                {
                    this.subsystemSelectionChangedCommand = new RelayCommand(
                        async (param) =>
                        {
                            try
                            {
                                AllPunches = new ObservableCollection<Punch>(await _businessLogic.GetAllPunchesByParamsAsync(SelectedDiscipline, SelectedUnit, SelectedSystem, SelectedSubsystem, IsAll));
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        }
                    );
                }
                return this.subsystemSelectionChangedCommand;
            }
        }

        private ICommand selectPunchToCloseCommand;
        public ICommand SelectPunchToCloseCommand
        {
            get
            {
                if (this.selectPunchToCloseCommand is null)
                {
                    this.selectPunchToCloseCommand = new RelayCommand(
                        (param) =>
                        {
                            try
                            {
                                AllPunches.Add(SelectedToClosePunch);
                                ToClosePunches.Remove(SelectedToClosePunch);
                                if (ToClosePunches.Count == 0)
                                {
                                    IsActive = true;
                                }
                                else
                                {
                                    IsActive = false;
                                }
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        }
                    );
                }
                return this.selectPunchToCloseCommand;
            }
        }

        private ICommand selectPunchOpenedCommand;
        public ICommand SelectPunchOpenedCommand
        {
            get
            {
                if (this.selectPunchOpenedCommand is null)
                {
                    this.selectPunchOpenedCommand = new RelayCommand(
                        (param) =>
                        {
                            try
                            {
                                if (SelectedOpenedPunch.DateClosed != null)
                                {
                                    _view.Alert("Selected punch is already closed", "Error");
                                    return;
                                }
                                ToClosePunches.Add(SelectedOpenedPunch);
                                AllPunches.Remove(SelectedOpenedPunch);
                                if (ToClosePunches.Count == 0)
                                {
                                    IsActive = true;
                                }
                                else
                                {
                                    IsActive = false;
                                }
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }

                        }
                    );
                }
                return this.selectPunchOpenedCommand;
            }
        }

        private ICommand closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (this.closeCommand is null)
                {
                    this.closeCommand = new RelayCommand(
                        async (param) =>
                        {
                            try
                            {
                                await _businessLogic.ClosingPunchesAsync(ToClosePunches);
                                _view.Close(true);
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }

                        },
                        (param) =>
                        {
                            return (ToClosePunches.Count > 0);
                        }

                    );
                }
                return this.closeCommand;
            }
        }

        private ICommand deletePunchCommand;
        public ICommand DeletePunchCommand
        {
            get
            {
                if (this.deletePunchCommand is null)
                {
                    this.deletePunchCommand = new RelayCommand(
                        async (param) =>
                        {
                            try
                            {
                                await _businessLogic.DeletePunchAsync(SelectedOpenedPunch);
                                AllPunches.Remove(SelectedOpenedPunch);
                                SelectedOpenedPunch = null;
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        },
                        (param) =>
                        {
                            return (SelectedOpenedPunch != null);
                        }

                    );
                }
                return this.deletePunchCommand;
            }
        }

        private ICommand showReportCommand;
        public ICommand ShowReportCommand
        {
            get
            {
                if (this.showReportCommand is null)
                {
                    this.showReportCommand = new RelayCommand(
                        async (param) =>
                        {
                            try
                            {
                                OpenedPunchesReport = await _businessLogic.GetOpenedPunchesReportAsync(new DateTime(FromDate.Value.Year, FromDate.Value.Month, FromDate.Value.Day, 0, 0, 0),
                                                                                            new DateTime(ToDate.Value.Year, ToDate.Value.Month, ToDate.Value.Day, 23, 59, 59));
                                ClosedPunchesReport = await _businessLogic.GetClosedPunchesReportAsync(new DateTime(FromDate.Value.Year, FromDate.Value.Month, FromDate.Value.Day, 0, 0, 0),
                                                                                            new DateTime(ToDate.Value.Year, ToDate.Value.Month, ToDate.Value.Day, 23, 59, 59));
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        },
                        (param) =>
                        {
                            return (FromDate != null && ToDate != null);
                        }
                    );
                }
                return this.showReportCommand;
            }
        }


        public PunchClosingViewModel(IPunchClosingView view, IBusinessLogic logic, Employee currentUser)
        {
            this._businessLogic = logic;
            this._view = view;
            this._view.SetViewModel(this);
            this.CurrentUser = currentUser;
            IsActive = true;
            try
            {
                DisciplineList = _businessLogic.GetAllDisciplines().OrderBy(d => d.Title).ToList();
                (DisciplineList as IList<Discipline>).Insert(0, new Discipline() { Title = "All", Id = 0 });
                UnitsList = _businessLogic.GetAllUnits().OrderBy(u => u.Title).ToList();
                (UnitsList as IList<Unit>).Insert(0, new Unit() { Title = "All", Id = 0 });
                SystemsList = _businessLogic.GetAllSystems().OrderBy(s => s.Title).ToList();
                (SystemsList as IList<Syst>).Insert(0, new Syst() { Title = "All", Id = 0 });
                AllPunches = new ObservableCollection<Punch>();
                OpenedPunchesReport = _businessLogic.GetOpenedPunchesReport(null, null);
                ClosedPunchesReport = _businessLogic.GetClosedPunchesReport(null, null);
            }
            catch (Exception ex)
            {
                _view.Alert(ex.Message, "Error");
            }
            if (CurrentUser.User.Abbreviation.ToLower() == "a")
            {
                DeleteVisibility = Visibility.Visible;
            }
            else
            {
                DeleteVisibility = Visibility.Collapsed;
            }
        }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }
}
