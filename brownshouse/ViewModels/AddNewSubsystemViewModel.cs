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
    public class AddNewSubsystemViewModel : NotifyPropertyChangedObject, IAddNewSubsystemViewModel
    {
        private IBusinessLogic _businessLogic;
        private IAddNewSubsystemView _view;
        public Syst SelectedSystem { get; set; }
        public Subsyst SelectedSubsystem { get; set; }
        public ICollection<Syst> SystemsList { get; set; }
        public ObservableCollection<Subsyst> SubsystemsList { get; set; } = new ObservableCollection<Subsyst>();
        private bool isActive;
        public bool IsActive { get { return isActive; } set { isActive = value; OnPropertyChanged(); } }

        private ICommand addSubsystemCommand;
        public ICommand AddSubsystemCommand
        {
            get
            {
                if (this.addSubsystemCommand is null)
                {
                    this.addSubsystemCommand = new RelayCommand(
                        (param) =>
                        {
                            IsActive = false;

                            SubsystemsList.Add(new Subsyst() { IdSyst = SelectedSystem.Id });
                        },
                        (param) =>
                        {
                            return SelectedSystem != null;
                        }
                    );
                }
                return this.addSubsystemCommand;
            }
        }

        private ICommand removeSubsystemCommand;
        public ICommand RemoveSubsystemCommand
        {
            get
            {
                if (this.removeSubsystemCommand is null)
                {
                    this.removeSubsystemCommand = new RelayCommand(
                        (param) =>
                        {
                            if (SelectedSubsystem != null)
                            {
                                SubsystemsList.Remove(SelectedSubsystem);
                                SelectedSubsystem = null;
                                if (SubsystemsList.Count == 0)
                                {
                                    IsActive = true;
                                }
                            }
                        },
                        (param) =>
                        {
                            return SelectedSubsystem != null;
                        }
                    );
                }
                return this.removeSubsystemCommand;
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
                            int emptyTitleCount = SubsystemsList.Where(s => String.IsNullOrEmpty(s.Title) || String.IsNullOrWhiteSpace(s.Title)).Count();
                            int emptyCodeCount = SubsystemsList.Where(s => String.IsNullOrEmpty(s.Code) || String.IsNullOrWhiteSpace(s.Code)).Count();
                            int emptyDescrCount = SubsystemsList.Where(s => String.IsNullOrEmpty(s.Description) || String.IsNullOrWhiteSpace(s.Description)).Count();
                            if (emptyTitleCount > 0)
                            {
                                _view.Alert("There is a blank subsystem title block in the list", "Error");
                                return;
                            }
                            else if (emptyCodeCount > 0)
                            {
                                _view.Alert("There is a blank subsystem code block in the list", "Error");
                                return;
                            }
                            else if (emptyDescrCount > 0)
                            {
                                _view.Alert("There is a blank subsystem description block in the list", "Error");
                                return;
                            }
                            if (SubsystemsList.Count >= 1)
                            {
                                foreach (var subact in SubsystemsList)
                                {
                                    int codeCount = SubsystemsList.Where(s => s.Code == subact.Code).Count();
                                    int titleCount = SubsystemsList.Where(s => s.Title == subact.Title).Count();
                                    if (codeCount > 1)
                                    {
                                        _view.Alert("There are two or more subsystems with the same code in the list", "Error");
                                        return;
                                    }
                                    else if (titleCount > 1)
                                    {
                                        _view.Alert("There are two or more subsystems with the same title in the list", "Error");
                                        return;
                                    }
                                }
                            }

                            try
                            {
                                await _businessLogic.AddSubsystemsAsync(SubsystemsList);
                                this._view.Close(true);
                            }
                            catch (Exception ex)
                            {
                                _view.Alert(ex.Message, "Error");
                            }
                        },
                        (param) =>
                        {
                            return (SubsystemsList.Count > 0);
                        }
                    );
                }
                return this.okCommand;
            }
        }

        public AddNewSubsystemViewModel(IAddNewSubsystemView view, IBusinessLogic logic)
        {
            this._view = view;
            this._businessLogic = logic;
            this._view.SetViewModel(this);
            try
            {
                SystemsList = _businessLogic.GetAllSystems().OrderBy(s => s.Title).ToList();
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
