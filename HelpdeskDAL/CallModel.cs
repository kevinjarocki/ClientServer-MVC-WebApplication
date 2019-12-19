using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Reflection;


namespace HelpdeskDAL
{
     public class CallModel
     {
         private IRepository<Call> callRepo;

         public CallModel()
         {
             callRepo = new HelpdeskRepository<Call>();
         }

         public Call GetById(int id)
         {
             List<Call> selectedCall = null;
             try
             {
                 selectedCall = callRepo.GetByExpression(call => call.Id == id);
             }
             catch (Exception ex)
             {
                 Console.WriteLine("Problem in " + GetType().Name + " " +
                                   MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                 throw ex;
            }

             return selectedCall.FirstOrDefault();
         }

         public List<Call> GetAll()
         {
             List<Call> allCalls = new List<Call>();
             try
             {
                 allCalls = callRepo.GetAll();
             }
             catch (Exception ex)
             {
                 Console.WriteLine("Problem in " + GetType().Name + " " +
                                   MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                 throw ex;
            }

             return allCalls;
         }

         public int Add(Call newCall)
         {
             try
             {
                 callRepo.Add(newCall);
             }
             catch (Exception ex)
             {
                 Console.WriteLine("Problem in " + GetType().Name + " "
                                   + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                 throw ex;
            }

             return newCall.Id;
         }


         public UpdateStatus Update(Call updateCall)
         {
             UpdateStatus opStatus = UpdateStatus.Failed;

             try
             {
                 opStatus = callRepo.Update(updateCall);
             }
             catch (DbUpdateConcurrencyException dbuex)
             {
                 opStatus = UpdateStatus.Stale;
                 Console.WriteLine("Problem in " + GetType().Name + " " +
                                   MethodBase.GetCurrentMethod().Name + " " + dbuex.Message);
             }
             catch (Exception ex)
             {
                 Console.WriteLine("Problem in " + GetType().Name + " " +
                                   MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                 throw ex;
             }
             return opStatus;
        }

         public int Delete(int id)
         {
             int callDeleted = -1;
             try
             {
                 callDeleted = callRepo.Delete(id);
             }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " "
                                  + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

             return callDeleted;
         }
     }
}
