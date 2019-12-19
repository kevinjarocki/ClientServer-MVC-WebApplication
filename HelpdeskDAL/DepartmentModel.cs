using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskDAL
{
    public class DepartmentModel
    {
        private IRepository<Department> repo;

        public DepartmentModel()
        {
            repo = new HelpdeskRepository<Department>();
        }

        public List<Department> GetAll()
        {
            List<Department> allDepartments = new List<Department>();

            try
            {
                allDepartments = repo.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                                  MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return allDepartments;
        }
    }
}
