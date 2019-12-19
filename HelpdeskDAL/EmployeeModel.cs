/*
 *Name: Kevin Jarocki
 *Filename: EmployeeModel.cs
 *Purpose: A component of the data access layer of Helpdesk.dll, consists of the
 * following functions: GetByEmail, GetByLastname, GetById, GetAll, Add, Update and Delete.
 * Date: 2018-10-24
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
//added
using System.Data.Entity.Infrastructure;

namespace HelpdeskDAL
{
    public class EmployeeModel
    {
        private IRepository<Employee> repo;

        public EmployeeModel()
        {
            repo = new HelpdeskRepository<Employee>();
        }

        //Get an employee from the data layer using the email field specifified in the parameters of the function
        public Employee GetByEmail(string email)
        {
            List<Employee> selectedEmployee = null;

            try
            {
                selectedEmployee = repo.GetByExpression(emp => emp.Email == email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
            }
            return selectedEmployee.FirstOrDefault();
        }

        //Get an employee from the data layer using the lastname field specifified in the parameters of the function
        public Employee GetByLastname(string name)
        {
            List<Employee> selectedEmployee = null;

            try
            {
                selectedEmployee = repo.GetByExpression(stu => stu.LastName == name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
            }

            return selectedEmployee.FirstOrDefault();
        }

        //Get an employee from the data layer using the id field specifified in the parameters of the function
        public Employee GetById(int id)
        {
            List<Employee> selectedEmployee = null;

            try
            {
                selectedEmployee = repo.GetByExpression(emp => emp.Id == id);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedEmployee.FirstOrDefault();
        }

        //Get all employees that are withing the data layer of the employees table
        public List<Employee> GetAll()
        {
            List<Employee> allEmployees = new List<Employee>();

                try
                {
                    allEmployees = repo.GetAll();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                    throw ex;
                }
                return allEmployees;
        
            
        }

        //Add a new employee field in the employees table within the data layer 
        public int Add(Employee newEmployee)
        {
            try
            {
                repo.Add(newEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " "
                   + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return newEmployee.Id;
        }
        //Update an existing employee in the employees table within the data layer 
        public UpdateStatus Update(Employee updateEmployee)
        {
            UpdateStatus opStatus = UpdateStatus.Failed;
            try
            {
                opStatus = repo.Update(updateEmployee);
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
        //Delete an employee in the employees table within the data layer 
        public int Delete(int id)
        {
            int employeesDeleted = -1;

            try
            {
                employeesDeleted = repo.Delete(id);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " "
                                   + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return employeesDeleted;
        }
    }
}
