using Autofac;
using brownshouse.Domain.Models;
using brownshouse.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace brownshouse.ViewModels
{
    public class MainViewModel : NotifyPropertyChangedObject, IMainViewModel
    {
        private IBusinessLogic _businessLogic;
        private IMainView _view;
        public Employee CurrentUser { get; private set; }
        private ICommand addAcceptanceCommand;
        public ICommand AddAcceptanceCommand
        {
            get
            {
                if (this.addAcceptanceCommand is null)
                {
                    this.addAcceptanceCommand = new RelayCommand(
                       (param) =>
                       {
                           var addViewModel = App.Container.Resolve<INewAcceptanceResultViewModel>();
                           addViewModel.ShowDialog();
                       }
                    );
                }
                return this.addAcceptanceCommand;
            }
        }

        private ICommand addDisciplineCommand;
        public ICommand AddDisciplineCommand
        {
            get
            {
                if (this.addDisciplineCommand is null)
                {
                    this.addDisciplineCommand = new RelayCommand(
                       (param) =>
                       {
                           var addDisciplineViewModel = App.Container.Resolve<IAddNewDisciplineViewModel>();
                           addDisciplineViewModel.ShowDialog();
                       }
                    );
                }
                return this.addDisciplineCommand;
            }
        }

        private ICommand addUnitCommand;
        public ICommand AddUnitCommand
        {
            get
            {
                if (this.addUnitCommand is null)
                {
                    this.addUnitCommand = new RelayCommand(
                       (param) =>
                       {
                           var addUnitViewModel = App.Container.Resolve<IAddNewUnitViewModel>();
                           addUnitViewModel.ShowDialog();
                       }
                    );
                }
                return this.addUnitCommand;
            }
        }

        private ICommand addWorkCommand;
        public ICommand AddWorkCommand
        {
            get
            {
                if (this.addWorkCommand is null)
                {
                    this.addWorkCommand = new RelayCommand(
                       (param) =>
                       {
                           var addWorkViewModel = App.Container.Resolve<IAddNewWorkViewModel>();
                           addWorkViewModel.ShowDialog();
                       }
                    );
                }
                return this.addWorkCommand;
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
                           var addActivityViewModel = App.Container.Resolve<IAddNewActivityViewModel>();
                           addActivityViewModel.ShowDialog();

                       }
                    );
                }
                return this.addActivityCommand;
            }
        }

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
                           var addSubactivityViewModel = App.Container.Resolve<IAddNewSubactivityViewModel>();
                           addSubactivityViewModel.ShowDialog();

                       }
                    );
                }
                return this.addSubactivityCommand;
            }
        }

        private ICommand addInspectionRoleCommand;
        public ICommand AddInspectionRoleCommand
        {
            get
            {
                if (this.addInspectionRoleCommand is null)
                {
                    this.addInspectionRoleCommand = new RelayCommand(
                       (param) =>
                       {
                           var addInspectionRoleViewModel = App.Container.Resolve<IAddNewInspectionRoleViewModel>();
                           addInspectionRoleViewModel.ShowDialog();

                       }
                    );
                }
                return this.addInspectionRoleCommand;
            }
        }

        private ICommand addFormCommand;
        public ICommand AddFormCommand
        {
            get
            {
                if (this.addFormCommand is null)
                {
                    this.addFormCommand = new RelayCommand(
                       (param) =>
                       {
                           var addFormViewModel = App.Container.Resolve<IAddNewFormViewModel>();
                           addFormViewModel.ShowDialog();

                       }
                    );
                }
                return this.addFormCommand;
            }
        }

        private ICommand addITPCommand;
        public ICommand AddITPCommand
        {
            get
            {
                if (this.addITPCommand is null)
                {
                    this.addITPCommand = new RelayCommand(
                       (param) =>
                       {
                           var addITPViewModel = App.Container.Resolve<IAddNewITPViewModel>();
                           addITPViewModel.ShowDialog();

                       }
                    );
                }
                return this.addITPCommand;
            }
        }

        private ICommand addRFICommand;
        public ICommand AddRFICommand
        {
            get
            {
                if (this.addRFICommand is null)
                {
                    this.addRFICommand = new RelayCommand(
                       (param) =>
                       {
                           var addRFIViewModel = App.Container.Resolve<IAddNewRequestViewModel>(new NamedParameter("currentUser", CurrentUser));
                           addRFIViewModel.ShowDialog();

                       }
                    );
                }
                return this.addRFICommand;
            }
        }

        private ICommand addSystemCommand;
        public ICommand AddSystemCommand
        {
            get
            {
                if (this.addSystemCommand is null)
                {
                    this.addSystemCommand = new RelayCommand(
                       (param) =>
                       {
                           var addSystemViewModel = App.Container.Resolve<IAddNewSystemViewModel>();
                           addSystemViewModel.ShowDialog();

                       }
                    );
                }
                return this.addSystemCommand;
            }
        }

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
                           var addSubsystemViewModel = App.Container.Resolve<IAddNewSubsystemViewModel>();
                           addSubsystemViewModel.ShowDialog();

                       }
                    );
                }
                return this.addSubsystemCommand;
            }
        }

        private ICommand requestsCommand;
        public ICommand RequestsCommand
        {
            get
            {
                if (this.requestsCommand is null)
                {
                    this.requestsCommand = new RelayCommand(
                       (param) =>
                       {
                           var requestsViewModel = App.Container.Resolve<IRequestViewModel>(new NamedParameter("currentUser", CurrentUser));
                           requestsViewModel.ShowDialog();

                       }
                    );
                }
                return this.requestsCommand;
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
                           var addPunchViewModel = App.Container.Resolve<IAddNewPunchViewModel>(new NamedParameter("currentUser", CurrentUser));
                           addPunchViewModel.ShowDialog();

                       }
                    );
                }
                return this.addPunchCommand;
            }
        }

        private ICommand punchClosingCommand;
        public ICommand PunchClosingCommand
        {
            get
            {
                if (this.punchClosingCommand is null)
                {
                    this.punchClosingCommand = new RelayCommand(
                       (param) =>
                       {
                           var punchClosingViewModel = App.Container.Resolve<IPunchClosingViewModel>(new NamedParameter("currentUser", CurrentUser));
                           punchClosingViewModel.ShowDialog();

                       }
                    );
                }
                return this.punchClosingCommand;
            }
        }

        private ICommand addEmployeeCommand;
        public ICommand AddEmployeeCommand
        {
            get
            {
                if (this.addEmployeeCommand is null)
                {
                    this.addEmployeeCommand = new RelayCommand(
                       (param) =>
                       {
                           var addEmployeeViewModel = App.Container.Resolve<IAddNewEmployeeViewModel>();
                           addEmployeeViewModel.ShowDialog();
                       }
                    );
                }
                return this.addEmployeeCommand;
            }
        }

        private ICommand changePasswordCommand;
        public ICommand ChangePasswordCommand
        {
            get
            {
                if (this.changePasswordCommand is null)
                {
                    this.changePasswordCommand = new RelayCommand(
                       (param) =>
                       {
                           var changePasswordViewModel = App.Container.Resolve<IChangePasswordViewModel>(new NamedParameter("currentUser", CurrentUser));
                           var res = changePasswordViewModel.ShowDialog();
                       }
                    );
                }
                return this.changePasswordCommand;
            }
        }

        private ICommand programMailCommand;
        public ICommand ProgramMailCommand
        {
            get
            {
                if (this.programMailCommand is null)
                {
                    this.programMailCommand = new RelayCommand(
                       (param) =>
                       {
                           var programMailViewModel = App.Container.Resolve<IProgramMailViewModel>();
                           var res = programMailViewModel.ShowDialog();
                       }
                    );
                }
                return this.programMailCommand;
            }
        }

        private ICommand logOutCommand;
        public ICommand LogOutCommand
        {
            get
            {
                if (this.logOutCommand is null)
                {
                    this.logOutCommand = new RelayCommand(
                       (param) =>
                       {
                           _view.Close(true);
                       }
                    );
                }
                return this.logOutCommand;
            }
        }

        public IView View { get { return this._view; } }

        public MainViewModel(IMainView view, IBusinessLogic logic, Employee currentUser)
        {
            this._view = view;
            this._view.SetBindingContext(this);
            this._businessLogic = logic;
            this.CurrentUser = currentUser;
        }

        public bool? ShowDialog()
        {
            return this._view.ShowDialog();
        }
    }
}
