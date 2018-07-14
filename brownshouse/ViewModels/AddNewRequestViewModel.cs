using brownshouse.Domain.Models;
using brownshouse.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.ViewModels
{
    public class AddNewRequestViewModel : NotifyPropertyChangedObject, IAddNewRequestViewModel
    {
        private IBusinessLogic _businessLogic;
        private IAddNewRequestView _view;
        public Employee CurrentUser { get; private set; }
        public Employee SelectedContractor { get; set; }
        public Employee SelectedSubcontractor { get; set; }
        public Employee SelectedOwner { get; set; }
        public Employee SelectedThirdParty { get; set; }
        public Unit SelectedUnit { get; set; }
        public Work SelectedWork { get; set; }
        public Activity SelectedActivity { get; set; }
        public Subactivity SelectedSubactivity { get; set; }
        public Syst SelectedSystem { get; set; }
        public Subsyst SelectedSubsystem { get; set; }
        public Tag SelectedTag { get; set; }
        public ICollection<Unit> UnitsList { get; set; }
        private ICollection<Work> worksList;
        public ICollection<Work> WorksList { get { return worksList; } set { worksList = value; OnPropertyChanged(); } }
        private ICollection<Activity> activitiesList;
        public ICollection<Activity> ActivitiesList { get { return activitiesList; } set { activitiesList = value; OnPropertyChanged(); } }
        private ICollection<Subactivity> subactivitiesList;
        public ICollection<Subactivity> SubactivitiesList { get { return subactivitiesList; } set { subactivitiesList = value; OnPropertyChanged(); } }
        public ICollection<Syst> SystemsList { get; set; }
        private ICollection<Subsyst> subsystemsList;
        public ICollection<Subsyst> SubsystemsList { get { return subsystemsList; } set { subsystemsList = value; OnPropertyChanged(); } }
        private ICollection<Employee> subcontractorsList { get; set; }
        public ICollection<Employee> SubcontractorsList { get { return subcontractorsList; } set { subcontractorsList = value; OnPropertyChanged(); } }
        private ICollection<Employee> contractorsList { get; set; }
        public ICollection<Employee> ContractorsList { get { return contractorsList; } set { contractorsList = value; OnPropertyChanged(); } }
        private ICollection<Employee> ownersList { get; set; }
        public ICollection<Employee> OwnersList { get { return ownersList; } set { ownersList = value; OnPropertyChanged(); } }
        private ICollection<Employee> thirdPartiesList { get; set; }
        public ICollection<Employee> ThirdPartiesList { get { return thirdPartiesList; } set { thirdPartiesList = value; OnPropertyChanged(); } }
        private ObservableCollection<Tag> tagsList;
        public ObservableCollection<Tag> TagsList { get { return tagsList; } set { tagsList = value; OnPropertyChanged(); } }
        private bool isActive;
        public bool IsActive { get { return isActive; } set { isActive = value; OnPropertyChanged(); } }
        public string RequestNo { get; set; }
        public DateTime? DateToDo { get; set; }

        private int count = 1;
        private string selectedHours;
        public string SelectedHours { get { return selectedHours; } set { selectedHours = value; OnPropertyChanged(); } }
        private string selectedMinutess;
        public string SelectedMinutess { get { return selectedMinutess; } set { selectedMinutess = value; OnPropertyChanged(); } }
        public ICollection<string> Hours { get; set; }
        public ICollection<string> Minutes { get; set; }

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
                            TagsList.Add(new Tag()
                            {
                                Id = count,
                                IdSubsyst = SelectedSubsystem.Id,
                                IdUnit = SelectedUnit.Id,
                                IdDiscipline = CurrentUser.User.Discipline.Id
                            });
                            ++count;
                        },
                        (param) =>
                        {
                            return (SelectedUnit != null &&
                                    SelectedSubactivity != null &&
                                    SelectedSubsystem != null &&
                                    DateToDo != null &&
                                    SelectedContractor != null &&
                                    SelectedSubcontractor != null &&
                                    ((SelectedSubactivity != null) ? (SelectedSubactivity.InspectionTestPlan.OwnerRole.RFIIsRequired ? SelectedOwner != null : true) : true) &&
                                    ((SelectedSubactivity != null) ? (SelectedSubactivity.InspectionTestPlan.ThirdPartRole.RFIIsRequired ? SelectedOwner != null : true) : true) &&
                                    !String.IsNullOrEmpty(SelectedHours) &&
                                    !String.IsNullOrEmpty(SelectedMinutess));
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
                            if (SelectedTag != null)
                            {
                                TagsList.Remove(SelectedTag);
                                SelectedTag = null;
                                if (TagsList.Count == 0)
                                {
                                    IsActive = true;
                                }
                                count = 1;
                                var tmp = new ObservableCollection<Tag>();
                                foreach (var t in TagsList)
                                {
                                    t.Id = count++;
                                    tmp.Add(t);
                                }
                                TagsList = tmp;
                            }
                        },
                        (param) =>
                        {
                            return SelectedTag != null;
                        }
                    );
                }
                return this.removeTagCommand;
            }
        }


        private ICommand workSelectionChangedCommand;
        public ICommand WorkSelectionChangedCommand
        {
            get
            {
                if (this.workSelectionChangedCommand is null)
                {
                    this.workSelectionChangedCommand = new RelayCommand(
                        async (param) =>
                        {
                            if (SelectedWork is null)
                            {
                                ActivitiesList = null;
                            }
                            else
                            {
                                try
                                {
                                    ActivitiesList = await _businessLogic.GetActivityByWorkAsync(SelectedWork);//.OrderBy(a => a.Title).ToList();
                                }
                                catch (Exception ex)
                                {
                                    _view.Alert(ex.Message, "Error");
                                }
                            }
                        }
                    );
                }
                return this.workSelectionChangedCommand;
            }
        }

        private ICommand activitySelectionChangedCommand;
        public ICommand ActivitySelectionChangedCommand
        {
            get
            {
                if (this.activitySelectionChangedCommand is null)
                {
                    this.activitySelectionChangedCommand = new RelayCommand(
                        async (param) =>
                        {
                            if (SelectedActivity is null)
                            {
                                SubactivitiesList = null;
                            }
                            else
                            {
                                try
                                {
                                    SubactivitiesList = await _businessLogic.GetSubactivityByActivityAsync(SelectedActivity);//.OrderBy(s => s.Title).ToList();
                                }
                                catch (Exception ex)
                                {
                                    _view.Alert(ex.Message, "Error");
                                }
                            }
                        }
                    );
                }
                return this.activitySelectionChangedCommand;
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
                                    SubsystemsList = await _businessLogic.GetSubsystemBySystemAsync(SelectedSystem);//.OrderBy(s => s.Title).ToList();
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
                            int emptyCodeCount = TagsList.Where(t => String.IsNullOrEmpty(t.Code) || String.IsNullOrWhiteSpace(t.Code)).Count();
                            if (emptyCodeCount > 0)
                            {
                                _view.Alert("There is a blank tag title block in the list", "Error");
                                return;
                            }
                            if (TagsList.Count > 1)
                            {
                                foreach (var tag in TagsList)
                                {
                                    int titleCount = TagsList.Where(t => t.Title == tag.Title).Count(); // нужно ли проверять?
                                    int codeCount = TagsList.Where(t => t.Code == tag.Code).Count();
                                    if (titleCount > 1)
                                    {
                                        _view.Alert("There are two or more tags with the same title in the list", "Error");
                                        return;
                                    }
                                    if (codeCount > 1)
                                    {
                                        _view.Alert("There are two or more tags with the same code in the list", "Error");
                                        return;
                                    }
                                }
                            }
                            try
                            {
                                DateTime requestTime = new DateTime(DateToDo.Value.Year, DateToDo.Value.Month, DateToDo.Value.Day, Int32.Parse(SelectedHours), Int32.Parse(SelectedMinutess), 0);

                                if (requestTime.Date == DateTime.Now.Date)
                                {
                                    var resp = _view.AlertQuestion("You've selected the current date for inspection time.\n Are you sure you want to continue?", "Inspection time");
                                    if (resp == System.Windows.MessageBoxResult.No)
                                    {
                                        return;
                                    }
                                }

                                Request newRequest = await _businessLogic.AddRequestAsync(TagsList, SelectedSubactivity, CurrentUser, SelectedSubcontractor, SelectedContractor, SelectedOwner, SelectedThirdParty, requestTime);
                                await _businessLogic.SendNotificationNewRFI(SelectedSubcontractor.Email, newRequest);
                                this._view.Close(true);
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        },
                        (param) =>
                        {
                            return (TagsList.Count() > 0);
                        }
                    );
                }
                return this.okCommand;
            }
        }


        public AddNewRequestViewModel(IAddNewRequestView view, IBusinessLogic logic, Employee currentUser)
        {
            this._view = view;
            this._businessLogic = logic;
            this._view.SetViewModel(this);
            this.CurrentUser = currentUser;
            this.TagsList = new ObservableCollection<Tag>();
            try
            {
                UnitsList = _businessLogic.GetAllUnits().OrderBy(u => u.Title).ToList();
                SystemsList = _businessLogic.GetAllSystems().OrderBy(s => s.Title).ToList();

                WorksList = _businessLogic.GetWorkByDiscipline(CurrentUser.User.Discipline);

                SubcontractorsList = _businessLogic.GetSubcontractorsByDiscipline(CurrentUser);
                ContractorsList = _businessLogic.GetContractorsByDiscipline(CurrentUser);
                OwnersList = _businessLogic.GetOwnersByDiscipline(CurrentUser);
                ThirdPartiesList = null;

                RequestNo = $"{CurrentUser.User.Discipline.Code} - {_businessLogic.GetNextRequestNo()}";
            }
            catch (Exception ex)
            {
                _view.Alert(ex.Message, "Error");
            }
            this.Hours = new List<string>() { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23" };
            this.Minutes = new List<string>() { "00", "05", "10", "15", "20", "25", "30", "35", "40", "45", "50", "55" };
            DateToDo = DateTime.Now.AddDays(1);
            IsActive = true;
        }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }
}
