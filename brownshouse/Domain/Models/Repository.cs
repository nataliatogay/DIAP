using brownshouse.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using System.Net.Mail;

namespace brownshouse.Domain.Models
{
    public class Repository : IRepository
    {
        private DbContext _db;

        public Repository(DbContext db)
        {
            _db = db;
        }

        public async Task NewAcceptanceResultAsync(string newResult)
        {
            await Task.Run(() =>
            {
                if (_db.Set<AcceptanceResult>().Where(r => r.Result == newResult).Count() > 0)
                {
                    throw new Exception($"Acceptance result \'{newResult}\' already exists");
                }
                else if (String.IsNullOrEmpty(newResult) || String.IsNullOrWhiteSpace(newResult))
                {
                    throw new Exception("Acceptance result is not entered");
                }
                try
                {
                    _db.Set<AcceptanceResult>().Add(new AcceptanceResult() { Result = newResult });
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });

        }

        public async Task NewDisciplineAsync(string newDisciplineTitle, string newDisciplineCode)
        {
            await Task.Run(() =>
            {
                int disciplineTitle = _db.Set<Discipline>().Where(d => d.Title == newDisciplineTitle).Count();
                int disciplineCode = _db.Set<Discipline>().Where(d => d.Code == newDisciplineCode).Count();
                if (disciplineTitle > 0)
                {
                    throw new Exception($"Discipline with title \'{newDisciplineTitle}\' already exists");
                }
                else if (disciplineCode > 0)
                {
                    throw new Exception($"Discipline with code \'{newDisciplineCode}\' already exists");
                }
                try
                {
                    _db.Set<Discipline>().Add(new Discipline() { Code = newDisciplineCode, Title = newDisciplineTitle });
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task NewUnitAsync(string newUnitTitle, string newUnitCode)
        {
            await Task.Run(() =>
            {
                int unitTitle = _db.Set<Unit>().Where(u => u.Title == newUnitTitle).Count();
                int unitCode = _db.Set<Unit>().Where(u => u.Code == newUnitCode).Count();
                if (unitTitle > 0)
                {
                    throw new Exception($"Unit with title \'{newUnitTitle}\' already exists");
                }
                else if (unitCode > 0)
                {
                    throw new Exception($"Unit with code \'{newUnitCode}\' already exists");
                }
                try
                {
                    _db.Set<Unit>().Add(new Unit() { Code = newUnitCode, Title = newUnitTitle });
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });

        }

        public async Task AddNewWorkAsync(string newWorkTitle, Discipline discipline)
        {
            await Task.Run(() =>
            {
                if (_db.Set<Work>().Where(w => w.Title == newWorkTitle).Count() > 0)
                {
                    throw new Exception($"Work \'{newWorkTitle}\' already exists");
                }
                try
                {
                    _db.Set<Work>().Add(new Work() { Title = newWorkTitle, IdDiscipline = discipline.Id });
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task AddNewActivityAsync(string newActivityTitle, int newActivityNumber, Work work)
        {
            await Task.Run(() =>
            {
                int activityTitle = _db.Set<Activity>().Where(a => a.Title == newActivityTitle).Count();
                int activityNumber = _db.Set<Activity>().Where(a => a.Number == newActivityNumber).Count();
                if (activityTitle > 0 && activityNumber > 0)
                {
                    throw new Exception($"Activity with title \'{newActivityTitle}\' and with number \'{newActivityNumber}\' already exists");
                }
                else if (activityTitle > 0)
                {
                    throw new Exception($"Activity with title \'{newActivityTitle}\' already exists");
                }
                else if (activityNumber > 0)
                {
                    throw new Exception($"Activity with number \'{newActivityNumber}\' already exists");
                }
                try
                {
                    _db.Set<Activity>().Add(new Activity() { Title = newActivityTitle, Number = newActivityNumber, IdWork = work.Id });
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task AddNewActivitiesAsync(Collection<Activity> activities)
        {
            await Task.Run(() =>
            {
                foreach (var item in activities)
                {
                    if (_db.Set<Activity>().Where(a => a.IdWork == item.IdWork && a.Number == item.Number).Count() > 0)
                    {
                        throw new Exception($"Activity with number \'{item.Number}\' already exists in database");
                    }
                    if (_db.Set<Activity>().Where(a => a.IdWork == item.IdWork && a.Title == item.Title).Count() > 0)
                    {
                        throw new Exception($"Activity with title \'{item.Title}\' already exists in database");
                    }
                }
                try
                {
                    foreach (var item in activities)
                    {
                        _db.Set<Activity>().Add(new Activity() { Title = item.Title, Number = item.Number, IdWork = item.IdWork });
                        _db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task AddNewSubactivitiesAsync(Collection<Subactivity> subactivities)
        {
            await Task.Run(() =>
            {
                foreach (var item in subactivities)
                {
                    if (_db.Set<Subactivity>().Where(s => s.IdActivity == item.IdActivity && s.Number == item.Number).Count() > 0)
                    {
                        throw new Exception($"Subactivity with number \'{item.Number}\' already exists in database");
                    }
                    if (_db.Set<Subactivity>().Where(s => s.IdActivity == item.IdActivity && s.Title == item.Title).Count() > 0)
                    {
                        throw new Exception($"Subactivity with title \'{item.Title}\' already exists in database");
                    }
                }
                try
                {
                    foreach (var item in subactivities)
                    {
                        _db.Set<Subactivity>().Add(new Subactivity() { Title = item.Title, Number = item.Number, IdActivity = item.IdActivity });
                        _db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task AddNewInspectionRoleAsync(string newCode, string newDescription, bool rfiIsRequired)
        {
            await Task.Run(() =>
            {
                int roleCode = _db.Set<InspectionRole>().Where(i => i.Code == newCode).Count();
                int roleDescription = _db.Set<InspectionRole>().Where(i => i.Description == newDescription).Count();
                if (roleCode > 0 && roleDescription > 0)
                {
                    throw new Exception($"Inspection role already exists");
                }
                else if (roleCode > 0)
                {
                    throw new Exception($"Inspection role with code \'{newCode}\' already exists");
                }
                else if (roleDescription > 0)
                {
                    throw new Exception($"Inspection role with description \'{newDescription}\' already exists");
                }
                try
                {
                    _db.Set<InspectionRole>().Add(new InspectionRole() { Code = newCode, Description = newDescription, RFIIsRequired = rfiIsRequired });
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task AddNewFormAsync(string newTitle, string newFilePath)
        {
            await Task.Run(() =>
            {
                int formTitle = _db.Set<Form>().Where(f => f.Title == newTitle).Count();
                int formFilePath = _db.Set<Form>().Where(f => f.FilePath == newFilePath).Count();
                if (formTitle > 0 && formFilePath > 0)
                {
                    throw new Exception($"Form already exists");
                }
                else if (formTitle > 0)
                {
                    throw new Exception($"Form with title \'{newTitle}\' already exists");
                }
                else if (formFilePath > 0)
                {
                    throw new Exception($"Form with filepath \'{newFilePath}\' already exists");
                }
                try
                {
                    _db.Set<Form>().Add(new Form() { Title = newTitle, FilePath = newFilePath });
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });

        }

        public async Task AddNewITPsAsync(Collection<InspectionTestPlan> plans)
        {
            await Task.Run(() =>
            {
                foreach (var item in plans)
                {
                    if (_db.Set<InspectionTestPlan>().Where(p => p.IdSubactivity == item.IdSubactivity).Count() > 0)
                    {
                        throw new Exception($"ITP for '/{item.Subactivity.Title}\' already exists in database");
                    }
                }
                if (plans.Where(p => p.Form == null).Count() > 0)
                {
                    throw new Exception("Form is not set for all subactivities");
                }
                if (plans.Where(p => p.SubcontractorRole == null).Count() > 0)
                {
                    throw new Exception("Subcontractor role is not set for all subactivities");
                }
                if (plans.Where(p => p.ContractorRole == null).Count() > 0)
                {
                    throw new Exception("Contractor role is not set for all subactivities");
                }
                try
                {
                    foreach (var item in plans)
                    {
                        item.IdForm = item.Form.Id;
                        item.Form = null;
                        item.IdSubcontractorRole = item.SubcontractorRole.Id;
                        item.SubcontractorRole = null;
                        item.IdContractorRole = item.ContractorRole.Id;
                        item.ContractorRole = null;
                        if (item.OwnerRole != null)
                        {
                            item.IdOwnerRole = item.OwnerRole.Id;
                            item.OwnerRole = null;
                        }
                        if (item.ThirdPartRole != null)
                        {
                            item.IdThirdPartRole = item.ThirdPartRole.Id;
                            item.ThirdPartRole = null;
                        }
                        item.IdSubactivity = item.Subactivity.Id;
                        item.Subactivity = null;
                        _db.Set<InspectionTestPlan>().Add(item);
                        _db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task<Request> AddRequestAsync(Collection<Tag> tags, Subactivity subactivity, Employee raisedBy, Employee respSubcontr, Employee respContr, Employee respOwner, Employee respThirdParty, DateTime? dateToDo, int projectid = 1)
        {
            return await Task.Run(() =>
            {
                try
                {
                    int requestNo = GetLastRequestNo() + 1;
                    var req = _db.Set<Request>().Add(new Request()
                    {
                        Number = requestNo,
                        IdProject = projectid,
                        IdUnit = tags.First().IdUnit,
                        IdSubsyst = tags.First().IdSubsyst,
                        IdRaisedByEmployee = raisedBy.Id,
                        IdResponsibleSubcontractor = respSubcontr.Id,
                        IdResponsibleContractor = respContr.Id,
                        IdResponsibleOwner = respOwner?.Id,
                        IdResponsibleThirdParty = respThirdParty?.Id,
                        IdTestPlan = subactivity.InspectionTestPlan.IdSubactivity,
                        DateOpen = DateTime.Now,
                        DateToDo = dateToDo.Value
                    });
                    _db.SaveChanges();
                    AddTags(tags);
                    AddRFI_Tag(tags, req.Id);
                    return req;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        private void AddTags(ICollection<Tag> tags)
        {
            try
            {
                foreach (var item in tags)
                {
                    int countCode = _db.Set<Tag>().Where(t => t.IdUnit == item.IdUnit && t.IdSubsyst == item.IdSubsyst && t.IdDiscipline == item.IdDiscipline && t.Code == item.Code).Count();
                    if (countCode == 0)
                    {
                        Tag t = _db.Set<Tag>().Add(new Tag()
                        {
                            Code = item.Code,
                            Title = item.Title,
                            IdSubsyst = item.IdSubsyst,
                            IdUnit = item.IdUnit,
                            IdDiscipline = item.IdDiscipline
                        });
                        item.Id = t.Id;
                        _db.SaveChanges();
                    }
                    int tagId = _db.Set<Tag>().Where(t => t.IdUnit == item.IdUnit && t.IdSubsyst == item.IdSubsyst && t.IdDiscipline == item.IdDiscipline && t.Code == item.Code).First().Id;
                    item.Id = tagId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void AddRFI_Tag(ICollection<Tag> tags, int requestId)
        {
            try
            {
                foreach (var item in tags)
                {
                    _db.Set<TagsRequest>().Add(new TagsRequest()
                    {
                        IdRequest = requestId,
                        IdTag = item.Id
                    });
                }

                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddSystemAsync(string systemTitle, string systemCode, string systemDescr)
        {
            await Task.Run(() =>
            {
                int sysTitle = _db.Set<Syst>().Where(s => s.Title == systemTitle).Count();
                int sysCode = _db.Set<Syst>().Where(s => s.Code == systemCode).Count();
                if (sysTitle > 0)
                {
                    throw new Exception($"System with title \'{systemTitle}\' already exists");
                }
                else if (sysCode > 0)
                {
                    throw new Exception($"System with code \'{systemCode}\' already exists");
                }
                try
                {
                    _db.Set<Syst>().Add(new Syst() { Code = systemCode, Title = systemTitle, Description = systemDescr });
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task AddSubsystemAsync(string subsystemTitle, string subsystemCode, string subsystemDescr, Syst system)
        {
            await Task.Run(() =>
            {
                int sysTitle = _db.Set<Subsyst>().Where(s => s.IdSyst == system.Id && s.Title == subsystemTitle).Count();
                int sysCode = _db.Set<Subsyst>().Where(s => s.IdSyst == system.Id && s.Code == subsystemCode).Count();
                if (sysTitle > 0)
                {
                    throw new Exception($"Subsystem with title \'{subsystemTitle}\' already exists");
                }
                else if (sysCode > 0)
                {
                    throw new Exception($"Subsystem with code \'{subsystemCode}\' already exists");
                }
                try
                {
                    _db.Set<Subsyst>().Add(new Subsyst() { Code = subsystemCode, Title = subsystemTitle, Description = subsystemDescr, IdSyst = system.Id });
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task AddSubsystemsAsync(ICollection<Subsyst> subsystems)
        {
            await Task.Run(() =>
            {
                foreach (var item in subsystems)
                {
                    if (_db.Set<Subsyst>().Where(s => s.IdSyst == item.IdSyst && s.Code == item.Code).Count() > 0)
                    {
                        throw new Exception($"Subsystem with code \'{item.Code}\' already exists in database");
                    }
                    if (_db.Set<Subsyst>().Where(s => s.IdSyst == item.IdSyst && s.Title == item.Title).Count() > 0)
                    {
                        throw new Exception($"Subsystem with title \'{item.Title}\' already exists in database");
                    }
                }
                try
                {
                    foreach (var item in subsystems)
                    {
                        _db.Set<Subsyst>().Add(new Subsyst() { Title = item.Title, Code = item.Code, Description = item.Description, IdSyst = item.IdSyst });
                        _db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task AddProjectAsync(string projectTitle, string projectCode)
        {
            await Task.Run(() =>
            {
                int projTitle = _db.Set<Project>().Where(p => p.Title == projectTitle).Count();
                int projCode = _db.Set<Project>().Where(p => p.Code == projectCode).Count();
                if (projTitle > 0)
                {
                    throw new Exception($"Project with title \'{projectTitle}\' already exists");
                }
                else if (projCode > 0)
                {
                    throw new Exception($"System with code \'{projectCode}\' already exists");
                }
                try
                {
                    _db.Set<Project>().Add(new Project() { Code = projectCode, Title = projectTitle });
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task<Request> ClosingRequestAsync(int requestId, bool result, ICollection<Punch> tagsPunch, Employee currentUser)
        {
            return await Task.Run(() =>
            {
                try
                {
                    Request request = _db.Set<Request>().Where(r => r.Id == requestId).First();
                    if (request == null)
                    {
                        throw new ArgumentNullException("RFI is not set");
                    }
                    else if (request.Status != null)
                    {
                        throw new Exception("RFI is already closed");
                    }
                    if (currentUser == null)
                    {
                        throw new ArgumentNullException("user is not set");
                    }
                    AcceptanceResult resAccepted = _db.Set<AcceptanceResult>().Where(r => r.Result == "accepted").First();
                    AcceptanceResult resRejected = _db.Set<AcceptanceResult>().Where(r => r.Result == "rejected").First();
                    AcceptanceResult resPunches = _db.Set<AcceptanceResult>().Where(r => r.Result == "Accepted with punches").First();
                    if (resAccepted == null || resRejected == null || resPunches == null)
                    {
                        throw new Exception("Acceptance results in DB are incorrect");
                    }

                    if (currentUser.User.Organization.OrganizationRole.Role.ToLower() == "subcontractor")
                    {
                        if (!result)
                        {
                            request.IdAcceptanceSubcontractor = resRejected.Id;
                            request.IdStatus = resRejected.Id;
                        }
                        else
                        {
                            if (tagsPunch is null)
                            {
                                request.IdAcceptanceSubcontractor = resAccepted.Id;
                            }
                            else
                            {
                                request.IdAcceptanceSubcontractor = resPunches.Id;
                            }
                        }
                    }
                    else if (currentUser.User.Organization.OrganizationRole.Role.ToLower() == "contractor")
                    {
                        if (!result)
                        {
                            request.IdAcceptanceContractor = resRejected.Id;
                            request.IdStatus = resRejected.Id;
                        }
                        else
                        {
                            if (tagsPunch is null)
                            {
                                request.IdAcceptanceContractor = resAccepted.Id;

                            }
                            else
                            {
                                request.IdAcceptanceContractor = resPunches.Id;
                            }
                            if ((request.InspectionTestPlan.OwnerRole is null || !request.InspectionTestPlan.OwnerRole.RFIIsRequired)
                                    && (request.InspectionTestPlan.ThirdPartRole is null || !request.InspectionTestPlan.ThirdPartRole.RFIIsRequired))
                            {
                                request.IdStatus = resAccepted.Id;
                            }
                        }
                    }
                    else if (currentUser.User.Organization.OrganizationRole.Role.ToLower() == "owner")
                    {
                        if (!result)
                        {
                            request.IdAcceptanceOwner = resRejected.Id;
                            request.IdStatus = resRejected.Id;
                        }
                        else
                        {
                            if (tagsPunch is null)
                            {
                                request.IdAcceptanceOwner = resAccepted.Id;

                            }
                            else
                            {
                                request.IdAcceptanceOwner = resPunches.Id;
                            }
                            if (request.InspectionTestPlan.ThirdPartRole is null || !request.InspectionTestPlan.ThirdPartRole.RFIIsRequired)
                            {
                                request.IdStatus = resAccepted.Id;
                            }
                        }
                    }
                    else // third part
                    {
                        if (!result)
                        {
                            request.IdAcceptanceThirdParty = resRejected.Id;
                            request.IdStatus = resRejected.Id;
                        }
                        else
                        {
                            if (tagsPunch is null)
                            {
                                request.IdAcceptanceThirdParty = resAccepted.Id;
                                request.IdStatus = resAccepted.Id;
                            }
                            else
                            {
                                request.IdAcceptanceThirdParty = resPunches.Id;
                                request.IdStatus = resPunches.Id;
                            }
                        }
                    }
                    _db.SaveChanges();
                    return _db.Set<Request>().Where(r => r.Id == request.Id).First();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task ClosingPunchesAsync(Collection<Punch> punchList)
        {
            await Task.Run(() =>
            {
                try
                {
                    foreach (var item in punchList)
                    {
                        if (item.DateClosed == null)
                        {
                            Punch punch = _db.Set<Punch>().Where(p => p.Id == item.Id).First();
                            if (punch != null)
                            {
                                punch.DateClosed = DateTime.Now;
                            }
                        }
                    }
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task AddPunchesAsync(ICollection<Punch> punches, Request request, Employee currentUser)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (request is null)
                    {
                        List<Tag> tags = new List<Tag>();
                        foreach (var item in punches)
                        {
                            tags.Add(item.Tag);
                        }
                        AddTags(tags);
                    }

                    foreach (var item in punches)
                    {
                        _db.Set<Punch>().Add(new Punch()
                        {
                            IdTag = item.Tag.Id,
                            IdPriority = item.PunchPriority.Id,
                            Description = item.Description,
                            DateOpened = DateTime.Now,
                            IdRaisedByEmployee = currentUser.Id
                        });
                    }
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task AddNewEmployeeAsync(string lastName, string firstName, string email, string login, string passwToDB, User user)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (_db.Set<Employee>().Where(e => e.Login == login).Count() > 0)
                    {
                        throw new Exception($"The user with login \'{login}\' already exists in database");
                    }
                    _db.Set<Employee>().Add(new Employee()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Login = login,
                        Password = passwToDB,
                        IdUser = user.Id
                    });
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task CreateProgramMail(ProgramMail mail, string password)
        {
            await Task.Run(() =>
            {
                try
                {
                    MailAddress m = new MailAddress(mail.Address);
                    var activeMail = _db.Set<ProgramMail>().Where(e => e.IsActive).Count();
                    if (activeMail > 0)
                    {
                        _db.Set<ProgramMail>().Where(e => e.IsActive).First().IsActive = false;
                    }

                    _db.Set<ProgramMail>().Add(new ProgramMail()
                    {
                        Address = mail.Address,
                        IsActive = true,
                        Password = password
                    });
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task ChangeProgramMail(ProgramMail mail, string password)
        {
            if (mail is null)
            {
                throw new ArgumentNullException("Mail is not set");
            }
            await Task.Run(() =>
            {
                try
                {
                    MailAddress m = new MailAddress(mail.Address);
                    var activeMail = _db.Set<ProgramMail>().Where(e => e.IsActive).Count();
                    if (activeMail > 0)
                    {
                        _db.Set<ProgramMail>().Where(e => e.IsActive).First().IsActive = false;
                    }
                    _db.Set<ProgramMail>().Add(new ProgramMail()
                    {
                        Address = mail.Address,
                        IsActive = true,
                        Password = password
                    });
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task<ICollection<Discipline>> GetAllDisciplinesAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _db.Set<Discipline>().OrderBy(d => d.Title).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public ICollection<Discipline> GetAllDisciplines()
        {
            try
            {
                return _db.Set<Discipline>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Work>> GetAllWorksAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _db.Set<Work>().OrderBy(w => w.Title).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task<ICollection<AcceptanceResult>> GetAllAcceptanceResultsAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _db.Set<AcceptanceResult>().OrderBy(r => r.Result).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task<ICollection<Form>> GetAllFormsAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _db.Set<Form>().OrderBy(f => f.Title).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public ICollection<Form> GetAllForms()
        {
            try
            {
                return _db.Set<Form>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<InspectionRole>> GetAllInspectionRolesAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _db.Set<InspectionRole>().OrderBy(r => r.Code).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public ICollection<InspectionRole> GetAllInspectionRoles()
        {
            try
            {
                return _db.Set<InspectionRole>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Unit>> GetAllUnitsAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _db.Set<Unit>().OrderBy(u => u.Title).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public ICollection<Unit> GetAllUnits()
        {
            try
            {
                return _db.Set<Unit>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Syst>> GetAllSystemsAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _db.Set<Syst>().OrderBy(s => s.Title).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public ICollection<Syst> GetAllSystems()
        {
            try
            {
                return _db.Set<Syst>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Request>> GetOpenedRequestsAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _db.Set<Request>().Where(r => r.Status == null).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task<ICollection<Request>> GetAllRequestsAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _db.Set<Request>().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public ICollection<Request> GetOpenedRequests()
        {
            try
            {
                return _db.Set<Request>().Where(r => r.Status == null).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<PunchPriority>> GetPunchesPrioritiesAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _db.Set<PunchPriority>().OrderBy(p => p.Priority).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public ICollection<PunchPriority> GetPunchesPriorities()
        {
            try
            {
                return _db.Set<PunchPriority>().ToList();
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
                return _db.Set<User>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Work>> GetWorkByDisciplineAsync(Discipline discipline)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _db.Set<Work>().Where(w => w.IdDiscipline == discipline.Id).OrderBy(w => w.Title).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public ICollection<Work> GetWorkByDiscipline(Discipline discipline)
        {
            try
            {
                return _db.Set<Work>().Where(w => w.IdDiscipline == discipline.Id).OrderBy(w => w.Title).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Activity>> GetActivityByWorkAsync(Work work)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _db.Set<Activity>().Where(a => a.IdWork == work.Id).OrderBy(a => a.Title).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task<ICollection<Subactivity>> GetSubactivityByActivityAsync(Activity activity)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _db.Set<Subactivity>().Where(s => s.IdActivity == activity.Id).OrderBy(s => s.Title).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public ICollection<Subactivity> GetSubactivityByActivity(Activity activity)
        {
            try
            {
                return _db.Set<Subactivity>().Where(s => s.IdActivity == activity.Id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Subsyst>> GetSubsystemBySystemAsync(Syst system)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _db.Set<Subsyst>().Where(s => s.IdSyst == system.Id).OrderBy(s => s.Title).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }



        public async Task<Employee> GetEmployeeByLoginAsync(string login)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var emp = _db.Set<Employee>().Where(e => e.Login == login).FirstOrDefault();
                    return emp;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public Employee GetEmployeeById(int id)
        {
            try
            {
                return _db.Set<Employee>().Where(e => e.Id == id).FirstOrDefault();

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
                List<Employee> employees;
                if (employee.User.Abbreviation.ToUpper() == "A")
                {
                    employees = _db.Set<Employee>().ToList();
                }
                else
                {
                    employees = _db.Set<Employee>().Where(e => e.User.IdDiscipline == employee.User.IdDiscipline &&
                                                          e.User.Organization.OrganizationRole.Role.ToLower() == "subcontractor").ToList();
                }

                return employees;
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
                List<Employee> employees;
                if (employee.User.Abbreviation.ToUpper() == "A")
                {
                    employees = _db.Set<Employee>().ToList();
                }
                else
                {
                    employees = _db.Set<Employee>().Where(e => e.User.IdDiscipline == employee.User.IdDiscipline &&
                                                          e.User.Organization.OrganizationRole.Role.ToLower() == "contractor").ToList();
                }

                return employees;
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
                List<Employee> employees;
                if (employee.User.Abbreviation.ToUpper() == "A")
                {
                    employees = _db.Set<Employee>().ToList();
                }
                else
                {
                    employees = _db.Set<Employee>().Where(e => e.User.IdDiscipline == employee.User.IdDiscipline &&
                                                          e.User.Organization.OrganizationRole.Role.ToLower() == "owner").ToList();
                }

                return employees;
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
                return _db.Set<Employee>().Where(e => e.User.Organization.OrganizationRole.Role.ToLower() == "third party").ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Organization> GetOrganizationByRoleAsync(string role)
        {
            return await Task.Run(() =>
            {
                try
                {
                    OrganizationRole orgRole = _db.Set<OrganizationRole>().Where(r => r.Role.Equals(role)).FirstOrDefault();
                    if (orgRole != null)
                    {
                        return _db.Set<Organization>().Where(o => o.IdOrganizationRole == orgRole.Id).FirstOrDefault();
                    }
                    else
                    {
                        throw new Exception("there is no such a role");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task<ICollection<Punch>> GetAllPunchesByDisciplineAsync(Discipline discipline)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _db.Set<Punch>().Where(p => p.Tag.IdDiscipline == discipline.Id).OrderBy(p => p.DateOpened).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task<ICollection<Punch>> GetAllPunchesByParamsAsync(Discipline discipline, Unit unit, Syst system, Subsyst subsystem, bool isAll)
        {
            return await Task.Run(() =>
            {
                try
                {
                    ICollection<Punch> res = new List<Punch>();
                    if ((discipline is null || discipline.Id == 0) &&
                        (unit is null || unit.Id == 0) &&
                        (system is null || system.Id == 0) &&
                        (subsystem is null || subsystem.Id == 0))
                    {
                        res = _db.Set<Punch>().ToArray();
                    }
                    else
                    {
                        if (discipline is null || discipline.Id == 0)
                        {
                            if (unit is null || unit.Id == 0)
                            {
                                if (subsystem is null || system.Id == 0)
                                {
                                    res = _db.Set<Punch>().Where(p => p.Tag.Subsyst.Syst.Id == system.Id).OrderBy(p => p.DateOpened).ToList();
                                }
                                else
                                {
                                    res = _db.Set<Punch>().Where(p => p.Tag.IdSubsyst == subsystem.Id).OrderBy(p => p.DateOpened).ToList();
                                }
                            }
                            else
                            {
                                if ((subsystem is null || subsystem.Id == 0) && (system is null || system.Id == 0))
                                {
                                    res = _db.Set<Punch>().Where(p => p.Tag.IdUnit == unit.Id).OrderBy(p => p.DateOpened).ToList();
                                }
                                else
                                {
                                    if (subsystem is null || subsystem.Id == 0)
                                    {
                                        res = _db.Set<Punch>().Where(p => p.Tag.Subsyst.Syst.Id == system.Id && p.Tag.IdUnit == unit.Id).OrderBy(p => p.DateOpened).ToList();
                                    }
                                    else
                                    {
                                        res = _db.Set<Punch>().Where(p => p.Tag.IdSubsyst == subsystem.Id && p.Tag.IdUnit == unit.Id).OrderBy(p => p.DateOpened).ToList();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (unit is null || unit.Id == 0)
                            {
                                if (subsystem is null || subsystem.Id == 0)
                                {
                                    if (system is null || system.Id == 0)
                                    {
                                        res = _db.Set<Punch>().Where(p => p.Tag.IdDiscipline == discipline.Id).OrderBy(p => p.DateOpened).ToList();
                                    }
                                    else
                                    {
                                        res = _db.Set<Punch>().Where(p => p.Tag.Subsyst.Syst.Id == system.Id && p.Tag.IdDiscipline == discipline.Id).OrderBy(p => p.DateOpened).ToList();
                                    }
                                }
                                else
                                {
                                    res = _db.Set<Punch>().Where(p => p.Tag.IdSubsyst == subsystem.Id && p.Tag.IdDiscipline == discipline.Id).OrderBy(p => p.DateOpened).ToList();
                                }
                            }
                            else
                            {
                                if (system is null || system.Id == 0)
                                {
                                    res = _db.Set<Punch>().Where(p => p.Tag.IdDiscipline == discipline.Id && p.Tag.IdUnit == unit.Id).OrderBy(p => p.DateOpened).ToList();
                                }
                                else
                                {
                                    if (subsystem is null || subsystem.Id == 0)
                                    {
                                        res = _db.Set<Punch>().Where(p => p.Tag.Subsyst.Syst.Id == system.Id && p.Tag.IdUnit == unit.Id && p.Tag.IdDiscipline == discipline.Id).OrderBy(p => p.DateOpened).ToList();
                                    }
                                    else
                                    {
                                        res = _db.Set<Punch>().Where(p => p.Tag.IdSubsyst == subsystem.Id && p.Tag.IdUnit == unit.Id && p.Tag.IdDiscipline == discipline.Id).OrderBy(p => p.DateOpened).ToList();
                                    }
                                }

                            }
                        }
                    }
                    if (!isAll)
                    {
                        return res.Where(p => p.DateClosed == null).OrderBy(p => p.DateOpened).ToList();
                    }
                    return res;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public ICollection<Request> GetOpenedRequestForDay(DateTime dayStart, DateTime dayFinish, Employee employee)
        {
            try
            {
                string role = employee.User.Organization.OrganizationRole.Role.ToLower();
                if (role == "subcontractor")
                {
                    return _db.Set<Request>().Where(r => r.Status == null && r.DateToDo >= dayStart && r.DateToDo <= dayFinish && r.ResponsibleSubcontractor.Id == employee.Id && r.AcceptanceSubcontractor == null).ToList();
                }
                else if (role == "contractor")
                {
                    return _db.Set<Request>().Where(r => r.Status == null && r.DateToDo >= dayStart && r.DateToDo <= dayFinish && r.ResponsibleContractor.Id == employee.Id && r.AcceptanceSubcontractor != null && r.AcceptanceContractor == null).ToList();
                }
                else if (role == "owner")
                {
                    return _db.Set<Request>().Where(r => r.Status == null && r.DateToDo >= dayStart && r.DateToDo <= dayFinish && r.ResponsibleOwner.Id == employee.Id && r.AcceptanceContractor != null && r.AcceptanceOwner == null).ToList();
                }
                else if (role == "third party")
                {
                    return _db.Set<Request>().Where(r => r.Status == null && r.DateToDo >= dayStart && r.DateToDo <= dayFinish && r.ResponsibleThirdParty.Id == employee.Id && r.AcceptanceOwner != null && r.AcceptanceThirdParty == null).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<ICollection<Request>> GetOpenedRequestByUser(Employee employee)
        {
            return await Task.Run(() =>
            {
                try
                {
                    string role = employee.User.Organization.OrganizationRole.Role.ToLower();
                    if (role == "subcontractor")
                    {
                        return _db.Set<Request>().Where(r => r.Status == null && r.ResponsibleSubcontractor.Id == employee.Id && r.AcceptanceSubcontractor == null).ToList();
                    }
                    else if (role == "contractor")
                    {
                        return _db.Set<Request>().Where(r => r.Status == null && r.ResponsibleContractor.Id == employee.Id && r.AcceptanceSubcontractor != null && r.AcceptanceContractor == null).ToList();
                    }
                    else if (role == "owner")
                    {
                        return _db.Set<Request>().Where(r => r.Status == null && r.ResponsibleOwner.Id == employee.Id && r.AcceptanceContractor != null && r.AcceptanceOwner == null).ToList();
                    }
                    else if (role == "third party")
                    {
                        return _db.Set<Request>().Where(r => r.Status == null && r.ResponsibleThirdParty.Id == employee.Id && r.AcceptanceOwner != null && r.AcceptanceThirdParty == null).ToList();
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });

        }

        public async Task<ICollection<Request>> GetRaisedRequestByUserAsync(Employee employee)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _db.Set<Request>().Where(r => r.IdRaisedByEmployee == employee.Id).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });            
        }

        public async Task DeletePunchAsync(Punch punch)
        {
            await Task.Run(() =>
            {
                try
                {
                    _db.Set<Punch>().Remove(punch);
                    _db.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            });
        }

        public ProgramMail GetProgramMail()
        {
            try
            {
                return _db.Set<ProgramMail>().Where(m => m.IsActive).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ICollection<Punch>> GetOpenedPunchesByEmployeeAsync(Employee employee)
        {
            return await Task.Run(() =>
            {
                try
                {
                    if (_db.Set<Employee>().Where(e => e.Id == employee.Id).Count() > 0)
                    {
                        return _db.Set<Punch>().Where(p => p.DateClosed == null && p.IdRaisedByEmployee == employee.Id).ToList();
                    }
                    else
                    {
                        throw new Exception("user is incorrect");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task<ICollection<Punch>> GetOpenedPunchesAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _db.Set<Punch>().Where(p => p.DateClosed == null).OrderBy(p => p.DateOpened).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task<ICollection<Punch>> GetAllPunchesAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _db.Set<Punch>().OrderBy(p => p.DateOpened).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });

        }

        public ICollection<Punch> GetOpenedPunches(Employee employee)
        {
            try
            {
                if (_db.Set<Employee>().Where(e => e.Id == employee.Id).Count() > 0)
                {
                    return _db.Set<Punch>().Where(p => p.DateClosed == null && p.IdRaisedByEmployee == employee.Id).ToList();
                }
                else
                {
                    throw new Exception("user is incorrect");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> ChangePassword(Employee employee, string passwToDB)
        {
            return await Task.Run(() =>
            {
                try
                {
                    _db.Set<Employee>().Where(e => e.Id == employee.Id).FirstOrDefault().Password = passwToDB;
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return true;
            });
        }

        public string GetFormPath(string formName)
        {

            try
            {
                var form = _db.Set<Form>().Where(f => f.Title == formName).FirstOrDefault();
                if (form != null)
                {
                    return form.FilePath;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PunchReport> GetOpenedPunchesReportAsync(DateTime? fromDate, DateTime? toDate)
        {
            return await Task.Run(() =>
            {
                try
                {
                    List<Punch> punches = new List<Punch>();
                    if (fromDate is null && toDate is null)
                    {
                        punches = _db.Set<Punch>().ToList();
                    }
                    else if (fromDate is null)
                    {
                        punches = _db.Set<Punch>().Where(p => p.DateOpened <= toDate).ToList();
                    }
                    else if (toDate is null)
                    {
                        punches = _db.Set<Punch>().Where(p => p.DateOpened >= fromDate).ToList();
                    }
                    else
                    {
                        punches = _db.Set<Punch>().Where(p => DateTime.Compare(p.DateOpened, fromDate.Value) >= 0
                        && DateTime.Compare(p.DateOpened, toDate.Value) <= 0).ToList();
                    }
                    PunchReport report = new PunchReport();
                    if (punches != null)
                    {
                        report.PunchAllCount = punches.Count;
                        foreach (var item in punches)
                        {
                            if (item.PunchPriority.Priority == "A")
                            {
                                report.PunchACount++;
                            }
                            else if (item.PunchPriority.Priority == "B")
                            {
                                report.PunchBCount++;
                            }
                            else if (item.PunchPriority.Priority == "C")
                            {
                                report.PunchCCount++;
                            }
                        }
                    }
                    return report;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task<PunchReport> GetClosedPunchesReportAsync(DateTime? fromDate, DateTime? toDate)
        {
            return await Task.Run(() =>
            {
                try
                {
                    List<Punch> punches = new List<Punch>();
                    if (fromDate is null && toDate is null)
                    {
                        punches = _db.Set<Punch>().Where(p => p.DateClosed != null).ToList();
                    }
                    else if (fromDate is null)
                    {
                        punches = _db.Set<Punch>().Where(p => p.DateClosed != null && p.DateClosed <= toDate).ToList();
                    }
                    else if (toDate is null)
                    {
                        punches = _db.Set<Punch>().Where(p => p.DateClosed != null && p.DateClosed >= fromDate).ToList();
                    }
                    else
                    {
                        punches = _db.Set<Punch>().Where(p => p.DateClosed != null && p.DateClosed >= fromDate && p.DateClosed <= toDate).ToList();
                    }
                    PunchReport report = new PunchReport();
                    if (punches != null)
                    {
                        report.PunchAllCount = punches.Count;
                        foreach (var item in punches)
                        {
                            if (item.PunchPriority.Priority == "A")
                            {
                                report.PunchACount++;
                            }
                            else if (item.PunchPriority.Priority == "B")
                            {
                                report.PunchBCount++;
                            }
                            else if (item.PunchPriority.Priority == "C")
                            {
                                report.PunchCCount++;
                            }
                        }
                    }
                    return report;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public PunchReport GetOpenedPunchesReport(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                List<Punch> punches = new List<Punch>();
                if (fromDate is null && toDate is null)
                {
                    punches = _db.Set<Punch>().ToList();
                }
                else if (fromDate is null)
                {
                    punches = _db.Set<Punch>().Where(p => p.DateOpened <= toDate).ToList();
                }
                else if (toDate is null)
                {
                    punches = _db.Set<Punch>().Where(p => p.DateOpened >= fromDate).ToList();
                }
                else
                {
                    punches = _db.Set<Punch>().Where(p => p.DateOpened >= fromDate && p.DateOpened <= toDate).ToList();
                }
                PunchReport report = new PunchReport();
                if (punches != null)
                {
                    report.PunchAllCount = punches.Count;
                    foreach (var item in punches)
                    {
                        if (item.PunchPriority.Priority == "A")
                        {
                            report.PunchACount++;
                        }
                        else if (item.PunchPriority.Priority == "B")
                        {
                            report.PunchBCount++;
                        }
                        else if (item.PunchPriority.Priority == "C")
                        {
                            report.PunchCCount++;
                        }
                    }
                }
                return report;
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
                List<Punch> punches = new List<Punch>();
                if (fromDate is null && toDate is null)
                {
                    punches = _db.Set<Punch>().Where(p => p.DateClosed != null).ToList();
                }
                else if (fromDate is null)
                {
                    punches = _db.Set<Punch>().Where(p => p.DateClosed != null && p.DateClosed <= toDate).ToList();
                }
                else if (toDate is null)
                {
                    punches = _db.Set<Punch>().Where(p => p.DateClosed != null && p.DateClosed >= fromDate).ToList();
                }
                else
                {
                    punches = _db.Set<Punch>().Where(p => p.DateClosed != null && p.DateClosed >= fromDate && p.DateClosed <= toDate).ToList();
                }
                PunchReport report = new PunchReport();
                if (punches != null)
                {
                    report.PunchAllCount = punches.Count;
                    foreach (var item in punches)
                    {
                        if (item.PunchPriority.Priority == "A")
                        {
                            report.PunchACount++;
                        }
                        else if (item.PunchPriority.Priority == "B")
                        {
                            report.PunchBCount++;
                        }
                        else if (item.PunchPriority.Priority == "C")
                        {
                            report.PunchCCount++;
                        }
                    }
                }
                return report;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int GetLastRequestNo()
        {
            if (_db.Set<Request>().Count() == 0)
            {
                return 0;
            }
            return _db.Set<Request>().OrderByDescending(r => r.Number).First().Number;
        }
    }
}