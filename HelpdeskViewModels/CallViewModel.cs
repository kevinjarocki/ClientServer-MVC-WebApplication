using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpdeskDAL;
using System.Reflection;

namespace HelpdeskViewModels
{
     public class CallViewModel
     {
         private CallModel _model;
         public int Id { get; set; }
         public int EmployeeId { get; set; }
         public int ProblemId { get; set; }
         public string EmployeeName { get; set; }
         public string ProblemDescription { get; set; }
         public string TechName { get; set; }
         public int TechId { get; set; }

         public string Notes { get; set; }
         public DateTime DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }
        public bool OpenStatus { get; set; }
         public string Timer { get; set; }


         public CallViewModel()
         {
             _model = new CallModel();
         }

         public void GetById()
         {
             try
             {
                 Call call = _model.GetById(Id);
                 Employee employee = new Employee();
                 Id = call.Id;
                 EmployeeId = call.EmployeeId;
                 ProblemId = call.ProblemId;
                 TechId = call.TechId;
                 Notes = call.Notes;
                 DateOpened = call.DateOpened;
                 DateClosed = call.DateClosed;
                 OpenStatus = call.OpenStatus;
                 Timer = Convert.ToBase64String(call.Timer);
             }
             catch (Exception ex)
             {
                 Console.WriteLine("Problem in " + GetType().Name + " " +
                                   MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                 throw ex;
            }
         }

         public List<CallViewModel> GetAll()
         {
             List<CallViewModel> allVms = new List<CallViewModel>();
             try
             {
                 List<Call> allCalls = _model.GetAll();
                 foreach (Call call in allCalls)
                 {
                    CallViewModel cVm = new CallViewModel();
                    EmployeeModel employee = new EmployeeModel();
                    ProblemModel problem = new ProblemModel();
                    cVm.Id = call.Id;
                    cVm.EmployeeId = call.EmployeeId;
                    cVm.ProblemId = call.ProblemId;
                    cVm.TechId = call.TechId;
                    cVm.TechName = employee.GetById(call.TechId).LastName;
                    cVm.EmployeeName = employee.GetById(call.EmployeeId).LastName;
                    cVm.ProblemDescription = problem.GetById(call.ProblemId).Description;
                    cVm.Notes = call.Notes;
                    cVm.DateOpened = call.DateOpened;
                    cVm.DateClosed = call.DateClosed;
                    cVm.OpenStatus = call.OpenStatus;
                    cVm.Timer = Convert.ToBase64String(call.Timer);
                    allVms.Add(cVm);
                }
             }
             catch (Exception ex)
             {
                 Console.WriteLine("Problem in " + GetType().Name + " " +
                                   MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                 throw ex;
             }
             return allVms;
        }


         public void Add()
         {
             Id = -1;
             try
             {
                 Call call = new Call();

                 call.EmployeeId = EmployeeId;
                 call.ProblemId = ProblemId;
                 call.TechId = TechId;
                 call.Notes = Notes;
                 call.DateOpened = DateOpened;
                 call.DateClosed = DateClosed;
                 call.OpenStatus = OpenStatus;
                 Id = _model.Add(call);
             }
             catch (Exception ex)
             {
                 Console.WriteLine("Problem in " + GetType().Name + " " +
                                   MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                 throw ex;
             }
        }

         public int Update()
         {
             UpdateStatus opStatus = UpdateStatus.Failed;
             try
             {
                 Call call = new Call();
                 call.Id = Id;
                 call.Timer = Convert.FromBase64String(Timer);
                 call.EmployeeId = EmployeeId;
                 call.ProblemId = ProblemId;
                 call.TechId = TechId;
                 call.Notes = Notes;
                 call.DateClosed = DateClosed;
                 call.DateOpened = DateOpened;
                 call.OpenStatus = OpenStatus;
                 opStatus = _model.Update(call);
             }
             catch (Exception ex)
             {
                 Console.WriteLine("Problem in " + GetType().Name + " " +
                                   MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                 throw ex;
             }
             return Convert.ToInt16(opStatus);
        }

         public int Delete()
         {
             int callDeleted = -1;
             try
             {
                 callDeleted = _model.Delete(Id);
             }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                                  MethodBase.GetCurrentMethod().Name + " " + ex.Message);
            }

             return callDeleted;
         }


    }
}
