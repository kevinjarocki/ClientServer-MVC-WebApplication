/*
 *Name: Kevin Jarocki
 *Filename: EmployeeViewModel.cs
 *Purpose: A component of the View model layer of HelpdeskViewModel.dll, consists of the
 * following functions: GetByLastname, GetById, GetAll, Add, Update and Delete.
 * Date: 2018-10-24
 */

using HelpdeskDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskViewModels
{
    public class EmployeeViewModel
    {
        private EmployeeModel _model;
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phoneno { get; set; }
        public string Timer { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int Id { get; set; }
        public bool? IsTech { get; set; }
        public string StaffPicture64 { get; set; }

        //constructor

        public EmployeeViewModel()
        {
            _model = new EmployeeModel();
        }

        //Get an employee from the data layer using the lastname field specifified in the parameters of the function
        public void GetByLastname()
        {
            try
            {
                Employee emp = _model.GetByLastname(Lastname);
                Title = emp.Title;
                Firstname = emp.FirstName;
                Lastname = emp.LastName;
                Phoneno = emp.PhoneNo;
                Email = emp.Email;
                if (emp.IsTech != null)
                {
                    IsTech = emp.IsTech;
                }

                Id = emp.Id;
                DepartmentId = emp.DepartmentId;
                if (emp.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                }
                Timer = Convert.ToBase64String(emp.Timer);

            }
            catch (Exception ex)
            {
                Lastname = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        //Get all employees that are withing the data layer of the employees table
        public List<EmployeeViewModel> GetAll()
        {
            List<EmployeeViewModel> allVms = new List<EmployeeViewModel>();
        try
         { 
            

            List<Employee> allEmployees = _model.GetAll();
            foreach (Employee emp in allEmployees)
            {
                EmployeeViewModel empVm = new EmployeeViewModel();
                empVm.Title = emp.Title;
                empVm.Firstname = emp.FirstName;
                empVm.Lastname = emp.LastName;
                empVm.Phoneno = emp.PhoneNo;
                empVm.Email = emp.Email;
                empVm.Id = emp.Id;
                    if (emp.IsTech != null)
                    {
                        empVm.IsTech = emp.IsTech;
                    }
                    empVm.DepartmentId = emp.DepartmentId;
                empVm.DepartmentName = emp.Department.DepartmentName;
                if (emp.StaffPicture != null)
                {
                    empVm.StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                }
                    empVm.Timer = Convert.ToBase64String(emp.Timer);
                allVms.Add(empVm);
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
        //Add a new employee field in the employees table within the data layer 
        public void Add()
        {
            Id = -1;
            try
            {
                Employee emp = new Employee();
                emp.Title = Title;
                emp.FirstName = Firstname;
                emp.LastName = Lastname;
                emp.PhoneNo = Phoneno;
                emp.Email = Email;
                emp.DepartmentId = DepartmentId;
                if (emp.StaffPicture != null)
                {
                    emp.StaffPicture = Convert.FromBase64String(StaffPicture64);
                }
                Id = _model.Add(emp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        //Update an existing employee in the employees table within the data layer 
        public int Update()
        {
            UpdateStatus opStatus = UpdateStatus.Failed;
            try
            {
                Employee emp = new Employee();
                emp.Title = Title;
                emp.FirstName = Firstname;
                emp.LastName = Lastname;
                emp.PhoneNo = Phoneno;
                emp.Email = Email;
                emp.Id = Id;
                emp.DepartmentId = DepartmentId;
                if (StaffPicture64 != null)
                {
                    emp.StaffPicture = Convert.FromBase64String(StaffPicture64);
                }
                emp.Timer = Convert.FromBase64String(Timer);
                opStatus = _model.Update(emp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return Convert.ToInt16(opStatus);
        }


        //Delete an employee in the employees table within the data layer 
        public int Delete()
        {
            int employeesDeleted = -1;

            try
            {
                employeesDeleted = _model.Delete(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
            }
            return employeesDeleted;
        }

        //Get an employee from the data access layer using the id field specifified in the parameters of the function
        public void GetById()
        {
            try
            {
                Employee emp = _model.GetById(Id);
                Title = emp.Title;
                Firstname = emp.FirstName;
                Lastname = emp.LastName;
                Phoneno = emp.PhoneNo;
                Email = emp.Email;
                Id = emp.Id;
                if (emp.IsTech != null)
                {
                  IsTech = emp.IsTech;  
                }
                
                DepartmentId = emp.DepartmentId;
                DepartmentName = emp.Department.DepartmentName;
                if (emp.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                }
                Timer = Convert.ToBase64String(emp.Timer);
            }
            catch (NullReferenceException nex)
            {
                Lastname = "not found";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        


    }
}
