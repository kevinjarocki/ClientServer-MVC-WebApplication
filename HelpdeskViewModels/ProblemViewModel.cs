using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HelpdeskDAL;

namespace HelpdeskViewModels
{
     public class ProblemViewModel
     {
         private ProblemModel _model;
         public string Description { get; set; }
         public int Id { get; set; }
         public string Timer { get; set; }

         public ProblemViewModel()
         {
             _model = new ProblemModel();
         }

         public void GetByDescription()
         {
             try
             {
                 Problem prob = _model.GetByDescription(Description);
                 Id = prob.Id;
                 Description = prob.Description;
                 Timer = Convert.ToBase64String(prob.Timer);
             }
             catch (Exception ex)
             {
                 Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
            }
         }

         public List<ProblemViewModel> GetAll()
         {
             List<ProblemViewModel> allVms = new List<ProblemViewModel>();

             try
             {
                 List<Problem> problems = _model.GetAll();

                 foreach (Problem p in problems)
                 {
                     ProblemViewModel VM = new ProblemViewModel();
                     VM.Id = p.Id;
                     VM.Description = p.Description;
                     VM.Timer = Convert.ToBase64String(p.Timer);
                     allVms.Add(VM);
                 }
             }
             catch (Exception ex)
             {
                 Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
             }

             return allVms;
         }



         public void GetById(int id)
         {
             try
             {
                 Problem prob = _model.GetById(id);
                 Id = prob.Id;
                 Description = prob.Description;
                 Timer = Convert.ToBase64String(prob.Timer);
             }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
            }

        }



     }
}
