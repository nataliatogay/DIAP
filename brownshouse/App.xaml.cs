using Autofac;
using brownshouse.Domain.EntityFramework;
using brownshouse.Domain.Models;
using brownshouse.Interfaces;
using brownshouse.ViewModels;
using brownshouse.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace brownshouse
{
    public partial class App : Application
    {
        public static IContainer Container { get; private set; }
        public App()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Repository>().As<IRepository>();
            builder.RegisterType<DIAPLogic>().As<IBusinessLogic>();
            builder.RegisterType<BrownsContext>().As<DbContext>();
            builder.RegisterType<MainViewModel>().As<IMainViewModel>();
            builder.RegisterType<MainWindow>().As<IMainView>();

            builder.RegisterType<ContractorMainViewModel>().As<IContractorMainViewModel>();
            builder.RegisterType<ContractorMainWindow>().As<IContractorMainView>();

            builder.RegisterType<LoginViewModel>().As<ILoginViewModel>();
            builder.RegisterType<LoginWindow>().As<ILoginView>();

            builder.RegisterType<AddNewAcceptanceResultWindow>().As<INewAcceptanceResultView>();
            builder.RegisterType<NewAcceptanceResultViewModel>().As<INewAcceptanceResultViewModel>();
            builder.RegisterType<AddNewDisciplineWindow>().As<IAddNewDisciplineView>();
            builder.RegisterType<AddNewDisciplineViewModel>().As<IAddNewDisciplineViewModel>();
            builder.RegisterType<AddNewUnitWindow>().As<IAddNewUnitView>();
            builder.RegisterType<AddNewUnitViewModel>().As<IAddNewUnitViewModel>();
            builder.RegisterType<AddNewWorkWindow>().As<IAddNewWorkView>();
            builder.RegisterType<AddNewWorkViewModel>().As<IAddNewWorkViewModel>();
            builder.RegisterType<AddNewActivityWindow>().As<IAddNewActivityView>();
            builder.RegisterType<AddNewActivityViewModel>().As<IAddNewActivityViewModel>();
            builder.RegisterType<AddNewSubactivityWindow>().As<IAddNewSubactivityView>();
            builder.RegisterType<AddNewSubactivityViewModel>().As<IAddNewSubactivityViewModel>();
            builder.RegisterType<AddNewSystemWindow>().As<IAddNewSystemView>();
            builder.RegisterType<AddNewSystemViewModel>().As<IAddNewSystemViewModel>();
            builder.RegisterType<AddNewSubsystemWindow>().As<IAddNewSubsystemView>();
            builder.RegisterType<AddNewSubsystemViewModel>().As<IAddNewSubsystemViewModel>();

            builder.RegisterType<AddNewInspectionRoleWindow>().As<IAddNewInspectionRoleView>();
            builder.RegisterType<AddNewInspectionRoleViewModel>().As<IAddNewInspectionRoleViewModel>();
            builder.RegisterType<AddNewFormWindow>().As<IAddNewFormView>();
            builder.RegisterType<AddNewFormViewModel>().As<IAddNewFormViewModel>();
            builder.RegisterType<AddNewITPWindow>().As<IAddNewITPView>();
            builder.RegisterType<AddNewITPViewModel>().As<IAddNewITPViewModel>();
            builder.RegisterType<AddNewRequestWindow>().As<IAddNewRequestView>();
            builder.RegisterType<AddNewRequestViewModel>().As<IAddNewRequestViewModel>();
            builder.RegisterType<Requests>().As<IRequestView>();
            builder.RegisterType<RequestsViewModel>().As<IRequestViewModel>();
            builder.RegisterType<RequestClosingWindow>().As<IRequestClosingView>();
            builder.RegisterType<RequestClosingViewModel>().As<IRequestClosingViewModel>();
            builder.RegisterType<AddNewPunchFromRFIWindow>().As<IAddNewPunchFromRFIView>();
            builder.RegisterType<AddNewPunchFromRFIViewModel>().As<IAddNewPunchFromRFIViewModel>();
            builder.RegisterType<AddNewPunchWindow>().As<IAddNewPunchView>();
            builder.RegisterType<AddNewPunchViewModel>().As<IAddNewPunchViewModel>();
            builder.RegisterType<PunchClosingWindow>().As<IPunchClosingView>();
            builder.RegisterType<PunchClosingViewModel>().As<IPunchClosingViewModel>();
            builder.RegisterType<ChangePasswordWindow>().As<IChangePasswordView>();
            builder.RegisterType<ChangePasswordViewModel>().As<IChangePasswordViewModel>();
            builder.RegisterType<ProgramMailWindow>().As<IProgramMailView>();
            builder.RegisterType<ProgramMailViewModel>().As<IProgramMailViewModel>();

            builder.RegisterType<AddNewEmployeeWindow>().As<IAddNewEmployeeView>();
            builder.RegisterType<AddNewEmployeeViewModel>().As<IAddNewEmployeeViewModel>();

            builder.RegisterType<ForgotPasswordWindow>().As<IForgotPasswordView>();
            builder.RegisterType<ForgotPasswordViewModel>().As<IForgotPasswordViewModel>();
            builder.RegisterType<BrownsContext>().As<DbContext>();

            Container = builder.Build();

            var vm = Container.Resolve<ILoginViewModel>();
            var view = vm.View;
            this.MainWindow = view as Window;
            view.ShowDialog();
        }
    }
}
