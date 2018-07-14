using brownshouse.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
    public class DIAPLogic : IBusinessLogic
    {
        private IRepository _rep;

        public DIAPLogic(IRepository repository)
        {
            this._rep = repository;
        }

        public async Task AddNewActivitiesAsync(Collection<Activity> activities)
        {
            if (activities is null)
            {
                throw new ArgumentNullException("activities");
            }
            try
            {
                await _rep.AddNewActivitiesAsync(activities);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task AddNewActivityAsync(string newActivityTitle, int newActivityNumber, Work work)
        {
            if (work is null)
            {
                throw new Exception($"Work is not selected");
            }
            try
            {
                await _rep.AddNewActivityAsync(newActivityTitle, newActivityNumber, work);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddNewEmployeeAsync(string lastName, string firstName, string email, string login, string password, User user)
        {
            if (String.IsNullOrWhiteSpace(lastName) || String.IsNullOrEmpty(lastName))
            {
                throw new ArgumentNullException("Last name is not set");
            }
            if (String.IsNullOrWhiteSpace(firstName) || String.IsNullOrEmpty(firstName))
            {
                throw new ArgumentNullException("First name is not set");
            }
            if (String.IsNullOrWhiteSpace(login) || String.IsNullOrEmpty(login))
            {
                throw new ArgumentNullException("Login is not set");
            }
            if (String.IsNullOrWhiteSpace(email) || String.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("E-mail is not set");
            }
            try
            {
                MailAddress m = new MailAddress(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (String.IsNullOrWhiteSpace(password) || String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Password is not set");
            }
            if (user is null)
            {
                throw new ArgumentNullException("User is not set");
            }
            try
            {
                var emp = await _rep.GetEmployeeByLoginAsync(login);
                if (emp != null)
                {
                    throw new Exception($"The user with login \'{login}\' already exists in database");
                }
                string passwToDB = SetPassword(password);
                await _rep.AddNewEmployeeAsync(lastName, firstName, email, login, passwToDB, user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddNewFormAsync(string newTitle, string newFilePath)
        {
            if (String.IsNullOrEmpty(newTitle) || String.IsNullOrWhiteSpace(newTitle))
            {
                throw new Exception("Form title is not entered");
            }
            else if (String.IsNullOrEmpty(newFilePath) || String.IsNullOrWhiteSpace(newFilePath))
            {
                throw new Exception("Form filepath is not entered");
            }
            try
            {
                await _rep.AddNewFormAsync(newTitle, newFilePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddNewInspectionRoleAsync(string newCode, string newDescription, bool rfiIsRequired)
        {
            if (String.IsNullOrEmpty(newCode) || String.IsNullOrWhiteSpace(newCode))
            {
                throw new Exception("Inspection role code is not entered");
            }
            else if (String.IsNullOrEmpty(newDescription) || String.IsNullOrWhiteSpace(newDescription))
            {
                throw new Exception("Inspection role description is not entered");
            }
            try
            {
                await _rep.AddNewInspectionRoleAsync(newCode, newDescription, rfiIsRequired);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task AddNewITPsAsync(Collection<InspectionTestPlan> plans)
        {
            if (plans is null)
            {
                throw new ArgumentNullException("inspection test plan collection");
            }
            try
            {
                await _rep.AddNewITPsAsync(plans);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddNewSubactivitiesAsync(Collection<Subactivity> subactivities)
        {
            if (subactivities is null)
            {
                throw new ArgumentNullException("activities");
            }
            try
            {
                await _rep.AddNewSubactivitiesAsync(subactivities);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddNewWorkAsync(string newWorkTitle, Discipline discipline)
        {
            if (discipline is null)
            {
                throw new ArgumentNullException($"Discipline is not selected");
            }
            else if (String.IsNullOrEmpty(newWorkTitle) || String.IsNullOrWhiteSpace(newWorkTitle))
            {
                throw new Exception("The work title is not entered");
            }
            try
            {
                await _rep.AddNewWorkAsync(newWorkTitle, discipline);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddProjectAsync(string projectTitle, string projectCode)
        {
            if (String.IsNullOrEmpty(projectTitle) || String.IsNullOrWhiteSpace(projectTitle))
            {
                throw new Exception("Project title is not entered");
            }
            else if (String.IsNullOrEmpty(projectCode) || String.IsNullOrWhiteSpace(projectCode))
            {
                throw new Exception("System code is not entered");
            }
            try
            {
                await _rep.AddProjectAsync(projectTitle, projectCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddPunchesAsync(ICollection<Punch> punches, Request request, Employee currentUser)
        {
            if (punches is null)
            {
                throw new ArgumentNullException("punches is not set");
            }
            if (currentUser == null)
            {
                throw new ArgumentNullException("user is not set");
            }
            try
            {
                await _rep.AddPunchesAsync(punches, request, currentUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Request> AddRequestAsync(Collection<Tag> tags, Subactivity subactivity, Employee raisedBy, Employee respSubcontr, Employee respContr, Employee respOwner, Employee respThirdParty, DateTime? dateToDo, int projectid = 1)
        {
            if (tags is null)
            {
                throw new ArgumentNullException("Tags are not set");
            }
            if (tags.Count == 0)
            {
                throw new Exception("Tags are not selected");
            }
            if (raisedBy is null)
            {
                throw new ArgumentNullException("User raised RFI is not set");
            }
            if (respContr is null)
            {
                throw new ArgumentNullException("Contractor responsible for RFI is not set");
            }
            if (respSubcontr is null)
            {
                throw new ArgumentNullException("Subcontractor responsible for RFI is not set");
            }
            if (dateToDo is null || !dateToDo.HasValue)
            {
                throw new ArgumentNullException("Date to do for RFI is not set");
            }
            try
            {
                return await _rep.AddRequestAsync(tags, subactivity, raisedBy, respSubcontr, respContr, respOwner, respThirdParty, dateToDo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddSubsystemAsync(string subsystemTitle, string subsystemCode, string subsystemDescr, Syst system)
        {
            if (system is null)
            {
                throw new Exception("System is not set");
            }
            if (String.IsNullOrEmpty(subsystemTitle) || String.IsNullOrWhiteSpace(subsystemTitle))
            {
                throw new Exception("Subsystem title is not entered");
            }
            else if (String.IsNullOrEmpty(subsystemCode) || String.IsNullOrWhiteSpace(subsystemCode))
            {
                throw new Exception("Subsystem code is not entered");
            }
            else if (String.IsNullOrEmpty(subsystemDescr) || String.IsNullOrWhiteSpace(subsystemDescr))
            {
                throw new Exception("Subsystem description is not entered");
            }
            try
            {
                await _rep.AddSubsystemAsync(subsystemTitle, subsystemCode, subsystemDescr, system);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddSubsystemsAsync(ICollection<Subsyst> subsystems)
        {
            if (subsystems is null)
            {
                throw new ArgumentNullException("subsystems");
            }
            try
            {
                await _rep.AddSubsystemsAsync(subsystems);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddSystemAsync(string systemTitle, string systemCode, string systemDescr)
        {
            if (String.IsNullOrEmpty(systemTitle) || String.IsNullOrWhiteSpace(systemTitle))
            {
                throw new Exception("System title is not entered");
            }
            else if (String.IsNullOrEmpty(systemCode) || String.IsNullOrWhiteSpace(systemCode))
            {
                throw new Exception("System code is not entered");
            }
            else if (String.IsNullOrEmpty(systemDescr) || String.IsNullOrWhiteSpace(systemDescr))
            {
                throw new Exception("System description is not entered");
            }
            try
            {
                await _rep.AddSystemAsync(systemTitle, systemCode, systemDescr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool DoesPasswordMatch(string hashedPwdFromDatabase, string userEnteredPassword)
        {
            return BCrypt.Net.BCrypt.Verify(userEnteredPassword + "^Y8~JJ", hashedPwdFromDatabase);
        }

        private string SetPassword(string userPassword)
        {
            string pwdToHash = userPassword + "^Y8~JJ"; // "^Y8~JJ" - hard-coded salt
            return BCrypt.Net.BCrypt.HashPassword(pwdToHash, BCrypt.Net.BCrypt.GenerateSalt());
        }

        public async Task<bool> ChangePassword(Employee employee, string passwordOld, string passwordNew)
        {
            if (String.IsNullOrWhiteSpace(passwordOld) || String.IsNullOrEmpty(passwordOld))
            {
                throw new ArgumentNullException("Old password is not set");
            }
            if (String.IsNullOrWhiteSpace(passwordNew) || String.IsNullOrEmpty(passwordNew))
            {
                throw new ArgumentNullException("New password is not set");
            }
            if (employee is null)
            {
                throw new ArgumentNullException("Employee is not set");
            }
            if (!DoesPasswordMatch(employee.Password, passwordOld))
            {
                return false;
            }
            string passwToDB = SetPassword(passwordNew);
            try
            {
                return await _rep.ChangePassword(employee, passwToDB);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ForgotPassword(Employee employee, string passwordNew)
        {
            if (String.IsNullOrWhiteSpace(passwordNew) || String.IsNullOrEmpty(passwordNew))
            {
                throw new ArgumentNullException("New password is not set");
            }
            if (employee is null)
            {
                throw new ArgumentNullException("Employee is not set");
            }
            string passwToDB = SetPassword(passwordNew);
            try
            {
                await _rep.ChangePassword(employee, passwToDB);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ClosingPunchesAsync(Collection<Punch> punchList)
        {
            if (punchList is null)
            {
                throw new Exception("punches are not set");
            }
            try
            {
                await _rep.ClosingPunchesAsync(punchList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Request> ClosingRequestAsync(int requestId, bool result, ICollection<Punch> tagsPunch, Employee currentUser)
        {
            try
            {
                return await _rep.ClosingRequestAsync(requestId, result, tagsPunch, currentUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task CreateProgramMail(ProgramMail mail, string password)
        {
            if (mail is null)
            {
                throw new ArgumentNullException("Mail is not set");
            }
            try
            {
                await _rep.CreateProgramMail(mail, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<string>> FillRFIFormAsync(Request newRequest)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    try
                    {
                        List<string> files = new List<string>();
                        string newFileName = $"..\\..\\requests\\{newRequest.RaisedByEmployee.User.Discipline.Code}-{newRequest.Number}";
                        // FileInfo template = new FileInfo(@"D:\Downloads\7-Required Forms.xlsx");
                        FileInfo template = new FileInfo(_rep.GetFormPath("MainForm"));
                        FileInfo newFile = new FileInfo($"{newFileName}.xlsx");
                        using (ExcelPackage xlPackage = new ExcelPackage(newFile, template))
                        {
                            ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets["RFI Form"];

                            var cellNumber = worksheet.Cells[4, 8]; // 4-H
                            cellNumber.Value = $"{newRequest.RaisedByEmployee.User.Discipline.Code}-{newRequest.Number}";
                            var cellDateOpen = worksheet.Cells[4, 27]; // 4-AA
                            cellDateOpen.Value = newRequest.DateOpen.ToShortDateString();
                            Organization org = await GetOrganizationByRoleAsync("subcontractor");
                            var cellSubcontractor = worksheet.Cells[5, 8]; // 5-H
                            if (org != null)
                            {
                                cellSubcontractor.Value = org.Title;
                            }
                            var cellDiscipline = worksheet.Cells[6, 8]; // 6-H
                            cellDiscipline.Value = newRequest.InspectionTestPlan.Subactivity.Activity.Work.Discipline.Title;
                            var cellRaisedBy = worksheet.Cells[8, 4]; // 8-D
                            cellRaisedBy.Value = $"{newRequest.RaisedByEmployee.LastName} {newRequest.RaisedByEmployee.FirstName}";
                            var cellResponsible = worksheet.Cells[9, 4]; // 9-D
                            cellResponsible.Value = $"{newRequest.ResponsibleSubcontractor.LastName} {newRequest.ResponsibleSubcontractor.FirstName}";
                            var cellTags = worksheet.Cells[11, 9]; // 11-I
                            StringBuilder tags = new StringBuilder();
                            foreach (var item in newRequest.TagsRequests)
                            {
                                tags.Append(item.Tag.Code).Append("\n");
                            }
                            cellTags.Value = tags;
                            var cellDateToDo = worksheet.Cells[12, 9];
                            cellDateToDo.Value = newRequest.DateToDo.ToShortDateString();
                            var cellUnit = worksheet.Cells[12, 23]; // 12-W
                            cellUnit.Value = newRequest.Unit.Title;
                            var cellITPPhaseNo = worksheet.Cells[14, 9];
                            cellITPPhaseNo.Value = newRequest.InspectionTestPlan.Subactivity.Activity.Number.ToString();
                            var cellITPPhase = worksheet.Cells[14, 23];
                            cellITPPhase.Value = newRequest.InspectionTestPlan.Subactivity.Number.ToString();
                            var cellForm = worksheet.Cells[15, 9];
                            cellForm.Value = newRequest.InspectionTestPlan.Form.Title;


                            var cellContractorRoleH = worksheet.Cells[19, 15]; // 19-O
                            var cellContractorRoleW = worksheet.Cells[19, 23]; // 19-W

                            if (newRequest.InspectionTestPlan.ContractorRole.Code == "H")
                            {
                                cellContractorRoleH.Value = "X";
                            }
                            else if (newRequest.InspectionTestPlan.ContractorRole.Code == "W")
                            {
                                cellContractorRoleW.Value = "X";
                            }

                            var cellOwnerRoleH = worksheet.Cells[22, 15]; // 22-O
                            var cellOwnerRoleW = worksheet.Cells[22, 23]; // 22-w
                            if (newRequest.InspectionTestPlan.OwnerRole != null && newRequest.InspectionTestPlan.OwnerRole.Code == "H")
                            {
                                cellOwnerRoleH.Value = "X";
                            }
                            else if (newRequest.InspectionTestPlan.OwnerRole != null && newRequest.InspectionTestPlan.OwnerRole.Code == "W")
                            {
                                cellOwnerRoleW.Value = "X";
                            }

                            var cellThirdRoleH = worksheet.Cells[25, 15]; // 25-O
                            var cellThirdRoleW = worksheet.Cells[25, 23]; // 25-W
                            var cellThirdRoleN = worksheet.Cells[25, 29]; // 25-AC
                            if (newRequest.InspectionTestPlan.ThirdPartRole == null)
                            {
                                cellThirdRoleN.Value = "X";
                            }
                            else if (newRequest.InspectionTestPlan.ThirdPartRole.Code == "H")
                            {
                                cellThirdRoleH.Value = "X";
                            }
                            else if (newRequest.InspectionTestPlan.ThirdPartRole.Code == "W")
                            {
                                cellThirdRoleW.Value = "X";
                            }
                            var cellITPPhaseInfo = worksheet.Cells[29, 2]; //29-B
                            cellITPPhaseInfo.Value = newRequest.InspectionTestPlan.Subactivity.Title;
                            xlPackage.Save();
                            files.Add($"{newFileName}.xlsx");
                        }

                        string form = newRequest.InspectionTestPlan.Form.Title;


                        string tagFileName = $"{newFileName}-tags.xlsx";
                        FileInfo newTagFile = new FileInfo(tagFileName);
                        FileInfo tagTemplate = new FileInfo(_rep.GetFormPath(form));
                        using (ExcelPackage xlPackage = new ExcelPackage(newTagFile, tagTemplate))
                        {
                            ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[form];

                            var cellRFINo = worksheet.Cells[2, 2]; // 4-B
                            cellRFINo.Value = $"Reference RFI No: {newRequest.RaisedByEmployee.User.Discipline.Code}-{newRequest.Number}";
                            var cellForm = worksheet.Cells[4, 2]; // 4-B
                            cellForm.Value = $"Form no: {form}";
                            var cellDate = worksheet.Cells[4, 12]; // 4-L
                            cellDate.Value = $"Inspection date: {newRequest.DateToDo.ToShortDateString()}";
                            // need??
                            var cellTime = worksheet.Cells[4, 22]; // 4-V
                            cellTime.Value = $"Inspection time: {newRequest.DateToDo.ToShortTimeString()}";
                            var cellUnit = worksheet.Cells[5, 2]; // 5-B
                            cellUnit.Value = $"Unit: {newRequest.Unit.Title}";
                            var cellSystem = worksheet.Cells[5, 12]; // 5-L
                            cellSystem.Value = $"System: {newRequest.Subsyst.Syst.Title}";
                            var cellSubsystem = worksheet.Cells[5, 22]; // 5-V
                            cellSubsystem.Value = $"System: {newRequest.Subsyst.Title}";
                            StringBuilder tags = new StringBuilder();
                            foreach (var item in newRequest.TagsRequests)
                            {
                                tags.Append($"{item.Tag.Code}, ");
                            }
                            string tagsToWrite = tags.ToString().Substring(0, tags.ToString().Length - 2);
                            var cellTag = worksheet.Cells[7, 12]; // 7-L
                            cellTag.Value = tagsToWrite;
                            xlPackage.Save();
                            files.Add(tagFileName);
                        }
                        return files;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<string>> SignatureRFIFormAsync(Request newRequest, Employee employee)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    try
                    {
                        List<string> files = new List<string>();
                        string mainFormPath = $"..\\..\\requests\\{newRequest.RaisedByEmployee.User.Discipline.Code}-{newRequest.Number}";
                        string tagFormPath = $"{mainFormPath}-tags.xlsx";

                        string userRole = employee.User.Organization.OrganizationRole.Role.ToLower();
                        string today = $"{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}";
                        FileInfo template = new FileInfo($"{mainFormPath}.xlsx");
                        FileInfo newFile = new FileInfo($"{mainFormPath}new.xlsx");


                        if (userRole == "subcontractor")
                        {
                            if (newRequest.Status != null)
                            {
                                using (ExcelPackage xlPackage = new ExcelPackage(newFile, template))
                                {
                                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets["RFI Form"];
                                    var cellRes = worksheet.Cells[52, 20]; // 52-T
                                    cellRes.Value = "X";
                                    xlPackage.Save();
                                }
                            }
                            else
                            {
                                files.Add($"{mainFormPath}.xlsx");
                                files.Add(tagFormPath);
                                return files;
                            }

                        }

                        else
                        {
                            FileInfo templateTag = new FileInfo($"{mainFormPath}-tags.xlsx");
                            FileInfo newFileTag = new FileInfo($"{mainFormPath}-tags-new.xlsx");
                            if (userRole == "contractor")
                            {
                                using (ExcelPackage xlPackage = new ExcelPackage(newFile, template))
                                {
                                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets["RFI Form"];
                                    var cellPres = worksheet.Cells[41, 19]; // 41-S
                                    cellPres.Value = "X";
                                    var cellPresSign = worksheet.Cells[41, 23]; // 41-W
                                    cellPresSign.Value = employee.LastName;
                                    var cellPresSign1 = worksheet.Cells[42, 23]; // 42-W
                                    cellPresSign1.Value = "";
                                    var cellRes1 = worksheet.Cells[58, 7]; // 58-G
                                    cellRes1.Value = employee.LastName;
                                    var cellRes2 = worksheet.Cells[59, 7]; // 59-G
                                    cellRes2.Value = employee.LastName;
                                    var cellRes3 = worksheet.Cells[60, 7]; // 60-G
                                    cellRes3.Value = today;
                                    if (newRequest.Status != null)
                                    {
                                        string result = newRequest.Status.Result.ToLower();
                                        ExcelRange cellRes = null;
                                        if (result == "accepted")
                                        {
                                            cellRes = worksheet.Cells[41, 16]; // 52-P
                                        }
                                        else if (result == "accepted with punches")
                                        {
                                            cellRes = worksheet.Cells[41, 24]; // 52-X
                                        }
                                        else
                                        {
                                            cellRes = worksheet.Cells[41, 20]; // 52-T
                                        }
                                        cellRes.Value = "X";
                                    }
                                    xlPackage.Save();
                                }
                                string form = newRequest.InspectionTestPlan.Form.Title;
                                FileInfo tagTemplate = new FileInfo(_rep.GetFormPath(form));
                                using (ExcelPackage xlPackage = new ExcelPackage(newFileTag, templateTag))
                                {
                                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[form];
                                    var cellname = worksheet.Cells[19, 7]; // 19-G
                                    cellname.Value = employee.LastName;
                                    var cellsign = worksheet.Cells[20, 7]; // 20-G
                                    cellsign.Value = employee.LastName;
                                    var cellDate = worksheet.Cells[21, 7]; // 20-G
                                    cellDate.Value = today;
                                    xlPackage.Save();
                                }
                            }
                            else if (userRole == "owner")
                            {
                                using (ExcelPackage xlPackage = new ExcelPackage(newFile, template))
                                {
                                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets["RFI Form"];
                                    var cellPres = worksheet.Cells[44, 19]; // 44-S
                                    cellPres.Value = "X";
                                    var cellPresSign = worksheet.Cells[44, 23]; // 44-W
                                    cellPresSign.Value = employee.LastName;
                                    var cellPresSign1 = worksheet.Cells[45, 23]; // 42-W
                                    cellPresSign1.Value = "";
                                    var cellRes1 = worksheet.Cells[58, 15]; // 58-O
                                    cellRes1.Value = employee.LastName;
                                    var cellRes2 = worksheet.Cells[59, 15]; // 59-O
                                    cellRes2.Value = employee.LastName;
                                    var cellRes3 = worksheet.Cells[60, 15]; // 60-O
                                    cellRes3.Value = today;
                                    if (newRequest.Status != null)
                                    {
                                        string result = newRequest.Status.Result.ToLower();
                                        ExcelRange cellRes = null;
                                        if (result == "accepted")
                                        {
                                            cellRes = worksheet.Cells[52, 16]; // 52-P
                                        }
                                        else if (result == "accepted with punches")
                                        {
                                            cellRes = worksheet.Cells[52, 24]; // 52-X
                                        }
                                        else
                                        {
                                            cellRes = worksheet.Cells[52, 20]; // 52-T
                                        }
                                        cellRes.Value = "X";
                                    }
                                    xlPackage.Save();
                                }
                                string form = newRequest.InspectionTestPlan.Form.Title;
                                FileInfo tagTemplate = new FileInfo(_rep.GetFormPath(form));
                                using (ExcelPackage xlPackage = new ExcelPackage(newFileTag, templateTag))
                                {
                                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[form];
                                    var cellname = worksheet.Cells[19, 15]; // 19-O
                                    cellname.Value = employee.LastName;
                                    var cellsign = worksheet.Cells[20, 15]; // 20-O
                                    cellsign.Value = employee.LastName;
                                    var cellDate = worksheet.Cells[21, 15]; // 21-O
                                    cellDate.Value = today;
                                    xlPackage.Save();
                                }
                            }
                            else if (userRole == "third party")
                            {
                                using (ExcelPackage xlPackage = new ExcelPackage(newFile, template))
                                {
                                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets["RFI Form"];
                                    var cellPres = worksheet.Cells[47, 19]; // 47-S
                                    cellPres.Value = "X";
                                    var cellPresSign = worksheet.Cells[47, 23]; // 47-W
                                    cellPresSign.Value = employee.LastName;
                                    var cellPresSign1 = worksheet.Cells[48, 23]; // 48-W
                                    cellPresSign1.Value = "";
                                    var cellRes1 = worksheet.Cells[58, 24]; // 58-X
                                    cellRes1.Value = employee.LastName;
                                    var cellRes2 = worksheet.Cells[59, 24]; // 59-X
                                    cellRes2.Value = employee.LastName;
                                    var cellRes3 = worksheet.Cells[60, 24]; // 60-X
                                    cellRes3.Value = today;
                                    if (newRequest.Status != null)
                                    {
                                        string result = newRequest.Status.Result.ToLower();
                                        ExcelRange cellRes = null;
                                        if (result == "accepted")
                                        {
                                            cellRes = worksheet.Cells[52, 16]; // 52-P
                                        }
                                        else if (result == "accepted with punches")
                                        {
                                            cellRes = worksheet.Cells[52, 24]; // 52-X
                                        }
                                        else
                                        {
                                            cellRes = worksheet.Cells[52, 20]; // 52-T
                                        }
                                        cellRes.Value = "X";
                                    }
                                    xlPackage.Save();
                                }
                                string form = newRequest.InspectionTestPlan.Form.Title;
                                FileInfo tagTemplate = new FileInfo(_rep.GetFormPath(form));
                                using (ExcelPackage xlPackage = new ExcelPackage(newFileTag, templateTag))
                                {
                                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[form];
                                    var cellname = worksheet.Cells[19, 24]; // 19-X
                                    cellname.Value = employee.LastName;
                                    var cellsign = worksheet.Cells[20, 24]; // 20-X
                                    cellsign.Value = employee.LastName;
                                    var cellDate = worksheet.Cells[21, 24]; // 21-X
                                    cellDate.Value = today;
                                    xlPackage.Save();
                                }
                            }
                            templateTag.Delete();
                            File.Move($"{mainFormPath}-tags-new.xlsx", $"{mainFormPath}-tags.xlsx");
                            files.Add($"{mainFormPath}-tags.xlsx");
                        }
                        template.Delete();
                        File.Move($"{mainFormPath}new.xlsx", $"{mainFormPath}.xlsx");
                        files.Add($"{mainFormPath}.xlsx");


                        return files;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Activity>> GetActivityByWorkAsync(Work work)
        {
            if (work is null)
            {
                throw new ArgumentNullException("Work is not set");
            }
            try
            {
                return await _rep.GetActivityByWorkAsync(work);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<AcceptanceResult>> GetAllAcceptanceResultsAsync()
        {
            try
            {
                return await _rep.GetAllAcceptanceResultsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<Discipline> GetAllDisciplines()
        {
            try
            {
                return _rep.GetAllDisciplines();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Discipline>> GetAllDisciplinesAsync()
        {
            try
            {
                return await _rep.GetAllDisciplinesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<Form> GetAllForms()
        {
            try
            {
                return _rep.GetAllForms();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Form>> GetAllFormsAsync()
        {
            try
            {
                return await _rep.GetAllFormsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<InspectionRole> GetAllInspectionRoles()
        {
            try
            {
                return _rep.GetAllInspectionRoles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<InspectionRole>> GetAllInspectionRolesAsync()
        {
            try
            {
                return await _rep.GetAllInspectionRolesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Punch>> GetAllPunchesAsync()
        {
            try
            {
                return await _rep.GetAllPunchesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Punch>> GetAllPunchesByDisciplineAsync(Discipline discipline)
        {
            if (discipline is null)
            {
                throw new ArgumentNullException("Discipline is not set");
            }
            try
            {
                return await _rep.GetAllPunchesByDisciplineAsync(discipline);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Punch>> GetAllPunchesByParamsAsync(Discipline discipline, Unit unit, Syst system, Subsyst subsystem, bool isAll)
        {
            try
            {
                return await _rep.GetAllPunchesByParamsAsync(discipline, unit, system, subsystem, isAll);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Request>> GetAllRequestsAsync()
        {
            try
            {
                return await _rep.GetAllRequestsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<Syst> GetAllSystems()
        {
            try
            {
                return _rep.GetAllSystems();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Syst>> GetAllSystemsAsync()
        {
            try
            {
                return await _rep.GetAllSystemsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<Unit> GetAllUnits()
        {
            try
            {
                return _rep.GetAllUnits();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Unit>> GetAllUnitsAsync()
        {
            try
            {
                return await _rep.GetAllUnitsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<User> GetAllUsers()
        {
            try
            {
                return _rep.GetAllUsers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Work>> GetAllWorksAsync()
        {
            try
            {
                return await _rep.GetAllWorksAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PunchReport GetClosedPunchesReport(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                return _rep.GetClosedPunchesReport(fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PunchReport> GetClosedPunchesReportAsync(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                return await _rep.GetClosedPunchesReportAsync(fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> GetEmployeeAsync(string login, string password)
        {
            try
            {
                Employee emp = await _rep.GetEmployeeByLoginAsync(login);
                if (emp is null)
                {
                    return emp;
                }
                if (!DoesPasswordMatch(emp.Password, password))
                {
                    return null;
                }
                return emp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> GetEmployeeByLoginAsync(string login)
        {
            try
            {
                return await _rep.GetEmployeeByLoginAsync(login);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<Employee> GetSubcontractorsByDiscipline(Employee employee)
        {
            try
            {
                var subs = _rep.GetSubcontractorsByDiscipline(employee);
                var emp = _rep.GetEmployeeById(employee.Id);
                if (emp != null)
                {
                    subs.Remove(emp);
                }
                return subs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<Employee> GetContractorsByDiscipline(Employee employee)
        {
            try
            {
                return _rep.GetContractorsByDiscipline(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<Employee> GetOwnersByDiscipline(Employee employee)
        {
            try
            {
                return _rep.GetOwnersByDiscipline(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ICollection<Employee> GetThirdParties()
        {
            try
            {
                return _rep.GetThirdParties();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNextRequestNo()
        {
            try
            {
                return _rep.GetLastRequestNo() + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<Punch> GetOpenedPunches(Employee employee)
        {
            try
            {
                return _rep.GetOpenedPunches(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Punch>> GetOpenedPunchesAsync()
        {
            try
            {
                return await _rep.GetOpenedPunchesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Punch>> GetOpenedPunchesByEmployeeAsync(Employee employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException("Employee is not set");
            }
            try
            {
                return await _rep.GetOpenedPunchesByEmployeeAsync(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PunchReport GetOpenedPunchesReport(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                return _rep.GetOpenedPunchesReport(fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PunchReport> GetOpenedPunchesReportAsync(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                return await _rep.GetOpenedPunchesReportAsync(fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<Request> GetOpenedRequestForDay(DateTime day, Employee employee)
        {
            DateTime dayStart = new DateTime(day.Year, day.Month, day.Day, 0, 0, 0);
            DateTime dayFinish = new DateTime(day.Year, day.Month, day.Day, 23, 59, 59);
            try
            {
                return _rep.GetOpenedRequestForDay(dayStart, dayFinish, employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Request>> GetOpenedRequestByUser(Employee employee)
        {

            try
            {
                return await _rep.GetOpenedRequestByUser(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Request>> GetRaisedRequestByUserAsync(Employee employee)
        {

            try
            {
                return await _rep.GetRaisedRequestByUserAsync(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeletePunchAsync(Punch punch)
        {
            if (punch.DateClosed != null)
            {
                throw new Exception("Punch is already closed");
            }
            try
            {

                await _rep.DeletePunchAsync(punch);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<Request> GetOpenedRequests()
        {
            try
            {
                return _rep.GetOpenedRequests();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Request>> GetOpenedRequestsAsync()
        {
            try
            {
                return await _rep.GetOpenedRequestsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Organization> GetOrganizationByRoleAsync(string role)
        {
            try
            {
                return await _rep.GetOrganizationByRoleAsync(role);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramMail GetProgramMail()
        {
            try
            {
                return _rep.GetProgramMail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<PunchPriority> GetPunchesPriorities()
        {
            try
            {
                return _rep.GetPunchesPriorities();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<PunchPriority>> GetPunchesPrioritiesAsync()
        {
            try
            {
                return await _rep.GetPunchesPrioritiesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<Subactivity> GetSubactivityByActivity(Activity activity)
        {
            if (activity is null)
            {
                throw new ArgumentNullException("Activity is not set");
            }
            try
            {
                return _rep.GetSubactivityByActivity(activity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Subactivity>> GetSubactivityByActivityAsync(Activity activity)
        {
            if (activity is null)
            {
                throw new ArgumentNullException("Activity is not set");
            }
            try
            {
                return await _rep.GetSubactivityByActivityAsync(activity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Subsyst>> GetSubsystemBySystemAsync(Syst system)
        {
            if (system is null)
            {
                throw new ArgumentNullException("System is not set");
            }
            try
            {
                return await _rep.GetSubsystemBySystemAsync(system);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<Work> GetWorkByDiscipline(Discipline discipline)
        {
            if (discipline is null)
            {
                throw new ArgumentNullException("Discipline is not set");
            }
            try
            {
                return _rep.GetWorkByDiscipline(discipline);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Work>> GetWorkByDisciplineAsync(Discipline discipline)
        {
            if (discipline is null)
            {
                throw new ArgumentNullException("Discipline is not set");
            }
            try
            {
                return await _rep.GetWorkByDisciplineAsync(discipline);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task NewAcceptanceResultAsync(string newResult)
        {
            try
            {
                await _rep.NewAcceptanceResultAsync(newResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task NewDisciplineAsync(string newDisciplineTitle, string newDisciplineCode)
        {
            if (String.IsNullOrEmpty(newDisciplineTitle) || String.IsNullOrWhiteSpace(newDisciplineTitle))
            {
                throw new Exception("Discipline title is not entered");
            }
            else if (String.IsNullOrEmpty(newDisciplineCode) || String.IsNullOrWhiteSpace(newDisciplineCode))
            {
                throw new Exception("Discipline code is not entered");
            }
            try
            {
                await _rep.NewDisciplineAsync(newDisciplineTitle, newDisciplineCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task NewUnitAsync(string newUnitTitle, string newUnitCode)
        {
            if (String.IsNullOrEmpty(newUnitTitle) || String.IsNullOrWhiteSpace(newUnitTitle))
            {
                throw new Exception("Unit title is not entered");
            }
            else if (String.IsNullOrEmpty(newUnitCode) || String.IsNullOrWhiteSpace(newUnitCode))
            {
                throw new Exception("Unit code is not entered");
            }
            try
            {
                await _rep.NewUnitAsync(newUnitTitle, newUnitCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SendVerificationCode(string address)
        {
            int code = new Random().Next(100000, 1000000);
            ProgramMail programMail = _rep.GetProgramMail();
            ISender Sender = new MailSender("smtp.gmail.com", 587, new NetworkCredential(programMail.Address, programMail.Password));
            Sender.Send(address, "Verification code", $"Your verification code: {code}", null);
            return code;
        }

        public async Task SendNotificationNewRFI(string address, Request newRequest)
        {
            ICollection<string> files = await FillRFIFormAsync(newRequest);
            ProgramMail programMail = _rep.GetProgramMail();
            ISender Sender = new MailSender("smtp.gmail.com", 587, new NetworkCredential(programMail.Address, programMail.Password));
            Sender.Send(address, "RFI", "new rfi is opened", files);
        }

        public async Task SendNotificationResultRFI(string address, Request request, Employee employee)
        {
            ICollection<string> files = await SignatureRFIFormAsync(request, employee);
            ProgramMail programMail = _rep.GetProgramMail();
            ISender Sender = new MailSender("smtp.gmail.com", 587, new NetworkCredential(programMail.Address, programMail.Password));
            Sender.Send(address, "RFI", "new rfi is opened", files);
        }
    }
}
