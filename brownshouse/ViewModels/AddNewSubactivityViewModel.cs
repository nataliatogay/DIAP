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
    public class AddNewSubactivityViewModel : NotifyPropertyChangedObject, IAddNewSubactivityViewModel
    {
        private IBusinessLogic _businessLogic;
        private IAddNewSubactivityView _view;
        public Discipline SelectedDiscipline { get; set; }
        public Work SelectedWork { get; set; }
        public Activity SelectedActivity { get; set; }
        public Subactivity SelectedSubactivity { get; set; }
        public ICollection<Discipline> DisciplineList { get; set; }
        private ICollection<Work> worksList;
        public ICollection<Work> WorksList { get { return worksList; } set { worksList = value; OnPropertyChanged(); } }
        private ICollection<Activity> activitiesList;
        public ICollection<Activity> ActivitiesList { get { return activitiesList; } set { activitiesList = value; OnPropertyChanged(); } }

        public ObservableCollection<Subactivity> SubactivitiesList { get; set; } = new ObservableCollection<Subactivity>();

        private ICommand addSubactivityCommand;
        public ICommand AddSubactivityCommand
        {
            get
            {
                if (this.addSubactivityCommand is null)
                {
                    this.addSubactivityCommand = new RelayCommand(
                        (param) =>
                        {
                            IsActive = false;
                            SubactivitiesList.Add(new Subactivity() { IdActivity = SelectedActivity.Id });
                        },
                        (param) =>
                        {
                            return SelectedActivity != null;
                        }
                    );
                }
                return this.addSubactivityCommand;
            }
        }

        private ICommand removeSubactivityCommand;
        public ICommand RemoveSubactivityCommand
        {
            get
            {
                if (this.removeSubactivityCommand is null)
                {
                    this.removeSubactivityCommand = new RelayCommand(
                        (param) =>
                        {
                            if (SelectedSubactivity != null)
                            {
                                SubactivitiesList.Remove(SelectedSubactivity);
                                SelectedSubactivity = null;
                                if (SubactivitiesList.Count == 0)
                                {
                                    IsActive = true;
                                }
                            }
                        },
                        (param) =>
                        {
                            return SelectedSubactivity != null;
                        }
                    );
                }
                return this.removeSubactivityCommand;
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
                            }
                        }
                    );
                }
                return this.workSelectionChangedCommand;
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
                            int emptyTitleCount = SubactivitiesList.Where(s => String.IsNullOrEmpty(s.Title) || String.IsNullOrWhiteSpace(s.Title)).Count();
                            double res;
                            int incorrectNumberCount = SubactivitiesList.Where(s => !double.TryParse(s.Number.Replace(',', '.'), out res) || res <= 1).Count();
                            if (emptyTitleCount > 0)
                            {
                                _view.Alert("There is a blank activity title block in the list", "Error");
                                return;
                            }
                            else if (incorrectNumberCount > 0)
                            {
                                _view.Alert("There is an incorrect activity code in the list", "Error");
                                return;
                            }
                            if (SubactivitiesList.Count > 1)
                            {
                                foreach (var subact in SubactivitiesList)
                                {
                                    subact.Number = subact.Number.Replace(',', '.');
                                    int numberCount = SubactivitiesList.Where(s => s.Number.Replace(',', '.') == subact.Number).Count();
                                    int titleCount = SubactivitiesList.Where(s => s.Title == subact.Title).Count();
                                    if (numberCount > 1)
                                    {
                                        _view.Alert("There are two or more subactivities with the same code in the list", "Error");
                                        return;
                                    }
                                    else if (titleCount > 1)
                                    {
                                        _view.Alert("There are two or more subactivities with the same title in the list", "Error");
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                SubactivitiesList.First().Number = SubactivitiesList.First().Number.Replace(',', '.');
                            }

                            try
                            {
                                await _businessLogic.AddNewSubactivitiesAsync(SubactivitiesList);
                                this._view.Close(true);
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        },
                        (param) =>
                        {
                            return (SubactivitiesList.Count > 0);
                        }
                    );
                }
                return this.okCommand;
            }
        }

        public AddNewSubactivityViewModel(IAddNewSubactivityView view, IBusinessLogic logic)
        {
            this._view = view;
            this._businessLogic = logic;
            this._view.SetViewModel(this);
            try
            {
                DisciplineList = _businessLogic.GetAllDisciplines().OrderBy(d => d.Title).ToList();
            }
            catch (Exception ex)
            {
                _view.Alert(ex.Message, "Error");
            }
            IsActive = true;
        }


        private bool isActive;
        public bool IsActive { get { return isActive; } set { isActive = value; OnPropertyChanged(); } }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }
}
