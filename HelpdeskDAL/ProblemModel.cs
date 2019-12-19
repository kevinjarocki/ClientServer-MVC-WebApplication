using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HelpdeskDAL;

namespace HelpdeskDAL
{
     public class ProblemModel
     {
         IRepository<Problem> problemRepo;

         public ProblemModel()
         {
             problemRepo = new HelpdeskRepository<Problem>();
         }


         public Problem GetByDescription(string description)
         {
             List<Problem> selectedProblem = null;

             try
             {
                 selectedProblem = problemRepo.GetByExpression(prob => prob.Description == description);
             }
             catch (Exception ex)
             {
                 Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
             }

             return selectedProblem.FirstOrDefault();
         }

         public Problem GetById(int id)
         {
             List<Problem> problems = null;
             try
             {
                 problems = problemRepo.GetByExpression(prob => prob.Id == id);
             }
             catch (Exception ex)
             {
                 Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                 throw ex;
             }

             return problems.FirstOrDefault();
         }


         public List<Problem> GetAll()
         {
             List<Problem> allproblems = new List<Problem>();
             try
             {
                 allproblems = problemRepo.GetAll();
             }
             catch (Exception ex)
             {
                 Console.WriteLine("Problem in " + GetType().Name + " " +
                                   MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                 throw ex;
             }

             return allproblems;
         }

     }
}
