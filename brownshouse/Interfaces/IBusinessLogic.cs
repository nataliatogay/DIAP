using brownshouse.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Interfaces
{
    public interface IBusinessLogic
    {
        Task NewAcceptanceResultAsync(string newResult);
        Task NewDisciplineAsync(string newDisciplineTitle, string newDisciplineCode);
        Task NewUnitAsync(string newUnitTitle, string newUnitCode);
        Task AddNewITPsAsync(Collection<InspectionTestPlan> plans);
        Task AddNewWorkAsync(string newWorkTitle, Discipline discipline);
        Task AddNewActivityAsync(string newActivityTitle, int newActivityNumber, Work work);
        Task AddNewActivitiesAsync(Collection<Activity> activities);
        Task AddNewSubactivitiesAsync(Collection<Subactivity> subactivities);
        Task AddNewInspectionRoleAsync(string newCode, string newDescription, bool rfiIsRequired);
        Task AddNewFormAsync(string newTitle, string newFilePath);
        Task<Request> AddRequestAsync(Collection<Tag> tags, Subactivity subactivity, Employee raisedBy, Employee respSubcontr, Employee respContr, Employee respOwner, Employee respThirdParty, DateTime? dateToDo, int projectid = 1);
        Task AddSystemAsync(string systemTitle, string systemCode, string systemDescr);
        Task AddSubsystemAsync(string subsystemTitle, string subsystemCode, string subsystemDescr, Syst system);
        Task AddSubsystemsAsync(ICollection<Subsyst> subsystems);
        Task AddProjectAsync(string projectTitle, string projectCode);
        Task AddPunchesAsync(ICollection<Punch> punches, Request request, Employee currentUser);
        Task AddNewEmployeeAsync(string lastName, string firstName, string email, string login, string password, User user);

        Task<ICollection<Discipline>> GetAllDisciplinesAsync();
        ICollection<Discipline> GetAllDisciplines();
        Task<ICollection<Work>> GetAllWorksAsync();
        Task<ICollection<Form>> GetAllFormsAsync();
        ICollection<Form> GetAllForms();
        Task<ICollection<AcceptanceResult>> GetAllAcceptanceResultsAsync();
        Task<ICollection<InspectionRole>> GetAllInspectionRolesAsync();
        ICollection<InspectionRole> GetAllInspectionRoles();
        Task<ICollection<Unit>> GetAllUnitsAsync();
        ICollection<Unit> GetAllUnits();
        Task<ICollection<Syst>> GetAllSystemsAsync();
        ICollection<Syst> GetAllSystems();
        Task<ICollection<Request>> GetOpenedRequestsAsync();
        Task<ICollection<Request>> GetAllRequestsAsync();
        ICollection<Request> GetOpenedRequests();
        ICollection<Punch> GetOpenedPunches(Employee employee);
        Task<ICollection<PunchPriority>> GetPunchesPrioritiesAsync();
        ICollection<PunchPriority> GetPunchesPriorities();
        ICollection<User> GetAllUsers();

        Task<ICollection<Work>> GetWorkByDisciplineAsync(Discipline discipline);
        ICollection<Work> GetWorkByDiscipline(Discipline discipline);
        Task<ICollection<Activity>> GetActivityByWorkAsync(Work work);
        Task<ICollection<Subactivity>> GetSubactivityByActivityAsync(Activity activity);
        ICollection<Subactivity> GetSubactivityByActivity(Activity activity);
        Task<ICollection<Subsyst>> GetSubsystemBySystemAsync(Syst system);
        Task<Employee> GetEmployeeAsync(string login, string password);
        Task<Employee> GetEmployeeByLoginAsync(string login);
        //ICollection<Employee> GetEmployeesByDiscipline(Employee employee);

        ICollection<Employee> GetSubcontractorsByDiscipline(Employee employee);
        ICollection<Employee> GetContractorsByDiscipline(Employee employee);
        ICollection<Employee> GetOwnersByDiscipline(Employee employee);
        ICollection<Employee> GetThirdParties();
        Task<Organization> GetOrganizationByRoleAsync(string role);
        Task<ICollection<Punch>> GetOpenedPunchesByEmployeeAsync(Employee employee);
        Task<ICollection<Punch>> GetOpenedPunchesAsync();
        Task<ICollection<Punch>> GetAllPunchesAsync();
        Task<ICollection<Punch>> GetAllPunchesByDisciplineAsync(Discipline discipline);
        Task<ICollection<Punch>> GetAllPunchesByParamsAsync(Discipline discipline, Unit unit, Syst system, Subsyst subsystem, bool isAll);
        ICollection<Request> GetOpenedRequestForDay(DateTime day, Employee employee);
        Task<ICollection<Request>> GetOpenedRequestByUser(Employee employee);
        Task<ICollection<Request>> GetRaisedRequestByUserAsync(Employee employee);
        Task DeletePunchAsync(Punch punch);

        Task<Request> ClosingRequestAsync(int requestId, bool result, ICollection<Punch> tagsPunch, Employee currentUser);
        Task ClosingPunchesAsync(Collection<Punch> punchList);

        ProgramMail GetProgramMail();
        Task CreateProgramMail(ProgramMail mail, string password);
        Task<bool> ChangePassword(Employee employee, string passwordOld, string passwordNew);
        Task ForgotPassword(Employee employee, string passwordNew);

        Task<PunchReport> GetOpenedPunchesReportAsync(DateTime? fromDate, DateTime? toDate);
        Task<PunchReport> GetClosedPunchesReportAsync(DateTime? fromDate, DateTime? toDate);

        PunchReport GetOpenedPunchesReport(DateTime? fromDate, DateTime? toDate);
        PunchReport GetClosedPunchesReport(DateTime? fromDate, DateTime? toDate);

        int GetNextRequestNo();
        int SendVerificationCode(string address);
        Task SendNotificationNewRFI(string address, Request newRequest);
        Task SendNotificationResultRFI(string address, Request newRequest, Employee employee);
        //string GetEmailByEmployee(Employee employee);

        Task<ICollection<string>> FillRFIFormAsync(Request newRequest);
        // ICollection<string> FillPunchForm(Punch newPunch);
    }
}
