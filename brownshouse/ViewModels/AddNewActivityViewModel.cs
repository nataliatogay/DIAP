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
    class AddNewActivityViewModel : NotifyPropertyChangedObject, IAddNewActivityViewModel
    {
        private IAddNewActivityView _view;
        private IBusinessLogic businessLogic;
        public Work SelectedWork { get; set; }
        public Discipline SelectedDiscipline { get; set; }
        private ICollection<Work> worksList;
        public ICollection<Work> WorksList { get { return worksList; } set { worksList = value; OnPropertyChanged(); } }

        public ICollection<Discipline> DisciplineList { get; set; }
        public Activity SelectedActivity { get; set; }
        public ObservableCollection<Activity> ActivitiesList { get; set; } = new ObservableCollection<Activity>();
        private bool isActive;
        public bool IsActive { get { return isActive; } set { isActive = value; OnPropertyChanged(); } }

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
                            int emptyTitleCount = ActivitiesList.Where(a => String.IsNullOrEmpty(a.Title) || String.IsNullOrWhiteSpace(a.Title)).Count();
                            int incorrectNumberCount = ActivitiesList.Where(a => a.Number < 1).Count();
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
                            if (ActivitiesList.Count > 1)
                            {
                                foreach (var item in ActivitiesList)
                                {
                                    int numberCount = ActivitiesList.Where(a => a.Number == item.Number).Count();
                                    int titleCount = ActivitiesList.Where(a => a.Title == item.Title).Count();
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
                            try
                            {
                                await businessLogic.AddNewActivitiesAsync(ActivitiesList);
                                this._view.Close(true);
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        },
                        (param) =>
                        {
                            return (ActivitiesList.Count > 0);
                        }
                    );
                }
                return this.okCommand;
            }
        }

        private ICommand addActivityCommand;
        public ICommand AddActivityCommand
        {
            get
            {
                if (this.addActivityCommand is null)
                {
                    this.addActivityCommand = new RelayCommand(
                        (param) =>
                        {
                            IsActive = false;

                            ActivitiesList.Add(new Activity() { IdWork = SelectedWork.Id });
                        },
                        (param) =>
                        {
                            return SelectedWork != null;
                        }
                    );
                }
                return this.addActivityCommand;
            }
        }

        private ICommand removeActivityCommand;
        public ICommand RemoveActivityCommand
        {
            get
            {
                if (this.removeActivityCommand is null)
                {
                    this.removeActivityCommand = new RelayCommand(
                        (param) =>
                        {
                            if(SelectedActivity != null)
                            {
                                ActivitiesList.Remove(SelectedActivity);
                                SelectedActivity = null;
                                if (ActivitiesList.Count == 0)
                                {
                                    IsActive = true;
                                }
                            }
                        },
                        (param) =>
                        {
                            return SelectedActivity != null;
                        }
                    );
                }
                return this.removeActivityCommand;
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
                                WorksList = await businessLogic.GetWorkByDisciplineAsync(SelectedDiscipline);
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

        public AddNewActivityViewModel(IAddNewActivityView view, IBusinessLogic logic)
        {
            
            this.businessLogic = logic;
            this._view = view;
            this._view.SetViewModel(this);
            IsActive = true;
            try
            {
                DisciplineList = businessLogic.GetAllDisciplines().OrderBy(d => d.Title).ToList();
            }
            catch (Exception ex)
            {
                _view.Alert(ex.Message, "Error");
            }
        }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }
}
