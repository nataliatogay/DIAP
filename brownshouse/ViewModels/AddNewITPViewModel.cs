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
    public class AddNewITPViewModel : NotifyPropertyChangedObject, IAddNewITPViewModel
    {
        private IAddNewITPView _view;
        private IBusinessLogic _businessLogic;
        public Discipline SelectedDiscipline { get; set; }
        public Work SelectedWork { get; set; }
        public Activity SelectedActivity { get; set; }

        public ICollection<Discipline> DisciplineList { get; set; }
        public ICollection<Form> FormsList { get; set; }
        public ICollection<InspectionRole> InspectionRolesList { get; set; }
        private ICollection<Work> worksList;
        public ICollection<Work> WorksList { get { return worksList; } set { worksList = value; OnPropertyChanged(); } }
        private ICollection<Activity> activitiesList;
        public ICollection<Activity> ActivitiesList { get { return activitiesList; } set { activitiesList = value; OnPropertyChanged(); } }
        private ICollection<Subactivity> subactivitiesList;
        public ICollection<Subactivity> SubactivitiesList { get { return subactivitiesList; } set { subactivitiesList = value; OnPropertyChanged(); } }

        public ObservableCollection<InspectionTestPlan> ITPsList { get; set; } = new ObservableCollection<InspectionTestPlan>();

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
                            if (SelectedDiscipline is null)
                            {
                                WorksList = null;
                            }
                            else
                            {
                                try
                                {
                                    WorksList = await _businessLogic.GetWorkByDisciplineAsync(SelectedDiscipline);
                                }
                                catch (Exception ex)
                                {
                                    _view.Alert(ex.Message, "Error");
                                }
                                //WorksList = WorksList.OrderBy(w => w.Title).ToList();
                            }
                        }
                    );
                }
                return this.disciplineSelectionChangedCommand;
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
                                    ActivitiesList = await _businessLogic.GetActivityByWorkAsync(SelectedWork);
                                }
                                catch (Exception ex)
                                {
                                    _view.Alert(ex.Message, "Error");
                                }
                                //ActivitiesList = ActivitiesList.OrderBy(a => a.Title).ToList();
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
                        (param) =>
                        {
                            if (SelectedActivity is null)
                            {
                                SubactivitiesList = null;
                                return;
                            }
                            else
                            {
                                try
                                {
                                    SubactivitiesList = _businessLogic.GetSubactivityByActivity(SelectedActivity).OrderBy(a => int.Parse(a.Number.Substring(a.Number.IndexOf('.') + 1))).ToList();
                                    IsActive = false;
                                    foreach (var subact in SubactivitiesList)
                                    {
                                        ITPsList.Add(new InspectionTestPlan() { Subactivity = subact });
                                    }
                                    ITPsList.OrderBy(i => i.Subactivity.Number).ToList();
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
                                foreach (var itp in ITPsList)
                                {
                                    if (itp.Form is null || itp.SubcontractorRole is null || itp.ContractorRole is null || itp.OwnerRole is null || itp.ThirdPartRole is null)
                                    {
                                        _view.Alert("There are some blank block in the list", "Error");
                                        return;
                                    }
                                }
                                await _businessLogic.AddNewITPsAsync(ITPsList);
                                this._view.Close(true);
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

        public AddNewITPViewModel(IAddNewITPView view, IBusinessLogic logic)
        {
            this._businessLogic = logic;
            this._view = view;
            this._view.SetViewModel(this);
            IsActive = true;
            try
            {
                DisciplineList = _businessLogic.GetAllDisciplines().OrderBy(d => d.Title).ToList();
                FormsList = _businessLogic.GetAllForms().OrderBy(f => f.Title).ToList();
                InspectionRolesList = _businessLogic.GetAllInspectionRoles().OrderBy(r => r.Code).ToList();
            }
            catch (Exception ex)
            {
                _view.Alert(ex.Message, "Error");
            }
        }


        private bool isActive;
        public bool IsActive { get { return isActive; } set { isActive = value; OnPropertyChanged(); } }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }
}
