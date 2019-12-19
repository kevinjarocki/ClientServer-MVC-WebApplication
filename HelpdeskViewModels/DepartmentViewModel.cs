using HelpdeskDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HelpdeskViewModels
{
    public class DepartmentViewModel
    {
        private DepartmentModel _model;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Timer { get; set; }


        //constructor

        public DepartmentViewModel()
        {
            _model = new DepartmentModel();
        }

        public List<DepartmentViewModel> GetAll()
        {
            List<DepartmentViewModel> allVms = new List<DepartmentViewModel>();
            try
            {
                List<Department> allDepartments = _model.GetAll();
                foreach (Department dep in allDepartments)
                {
                    DepartmentViewModel depVm = new DepartmentViewModel();
                    depVm.Name = dep.DepartmentName;
                    depVm.Id = dep.Id;
                    depVm.Timer = Convert.ToBase64String(dep.Timer);
                    allVms.Add(depVm);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                                  MethodBase.GetCurrentMethod().Name + " " + ex.Message);
            }
            return allVms;
        }
    }
}
