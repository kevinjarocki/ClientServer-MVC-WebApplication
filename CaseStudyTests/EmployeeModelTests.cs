using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//added using
using HelpdeskDAL;
using System.Collections.Generic;

namespace CaseStudyTests
{
    [TestClass]
    public class EmployeeModelTests
    {
        //[TestMethod]
        //public void EmployeeModelGetbyEmailShouldReturnEmployee()
        //{
        //    EmployeeModel model = new EmployeeModel();
        //    Employee someEmployee = model.GetByEmail("bs@abc.com");
        //    Assert.IsNotNull(someEmployee);
        //}

        //[TestMethod]
        //public void EmployeeModelGetbyEmailShouldNotReturnEmployee()
        //{
        //    EmployeeModel model = new EmployeeModel();
        //    Employee someEmployee = model.GetByEmail("bs@abc.ca");
        //    Assert.IsNull(someEmployee);
        //}

        [TestMethod]
        public void EmployeeModelGetAllShouldReturnList()
        {
            EmployeeModel model = new EmployeeModel();
            List<Employee> allEmployees = model.GetAll();
            Assert.IsTrue(allEmployees.Count > 0);
        }

        [TestMethod]
        public void EmployeeModelAddShouldReturnNewId()
        {
            EmployeeModel model = new EmployeeModel();
            Employee newEmployee = new Employee();
            newEmployee.Title = "Mr.";
            newEmployee.FirstName = "Kevin";
            newEmployee.LastName = "Jarocki";
            newEmployee.Email = "ts@abc.com";
            newEmployee.PhoneNo = "(555)555-5551";
            newEmployee.IsTech = false;
            newEmployee.DepartmentId = 100;
            int newId = model.Add(newEmployee);
            Assert.IsTrue(newId > 0);
        }

        [TestMethod]
        public void EmployeeModelGetbyIdShouldReturnEmployee()
        {
            EmployeeModel model = new EmployeeModel();
            Employee someEmployee = model.GetByLastname("Jarocki");
            Employee anotherEmployee = model.GetById(someEmployee.Id);
            Assert.IsNotNull(anotherEmployee);
        }

        [TestMethod]
        public void EmployeeModelUpdateShouldOkStatus()
        {
            EmployeeModel model = new EmployeeModel();
            Employee updateEmployee = model.GetByLastname("Jarocki");
            updateEmployee.Email = (updateEmployee.Email.IndexOf(".ca") > 0) ? "ts@abc.com" : "ts@abc.ca";
            UpdateStatus EmployeesUpdated = model.Update(updateEmployee);
            Assert.IsTrue(EmployeesUpdated == UpdateStatus.Ok);
        }

        [TestMethod]
        public void EmployeeModelUpdateTwiceShouldReturnStaleStatus()
        {
            EmployeeModel model1 = new EmployeeModel();
            EmployeeModel model2 = new EmployeeModel();
            Employee updateEmployee1 = model1.GetByLastname("Jarocki");
            Employee updateEmployee2 = model2.GetByLastname("Jarocki");
            updateEmployee1.Email = (updateEmployee1.Email.IndexOf(".ca") > 0) ? "ts@abc.com" : "ts@abc.ca";
            if (model1.Update(updateEmployee1) == UpdateStatus.Ok)
            {
                updateEmployee2.Email = (updateEmployee2.Email.IndexOf(".ca") > 0) ? "ts@abc.com" : "ts@abc.ca";
                Assert.IsTrue(model2.Update(updateEmployee2) == UpdateStatus.Stale);
            }
            else
                Assert.Fail();
        }

        [TestMethod]
        public void EmployeeModelDeleteShouldReturnOne()
        {
            EmployeeModel model = new EmployeeModel();
            Employee deleteEmployee = model.GetByLastname("Jarocki");
            int EmployeesDeleted = model.Delete(deleteEmployee.Id);
            Assert.IsTrue(EmployeesDeleted == 1);
        }

        [TestMethod]
        public void LoadPicsShouldReturnTrue()
        {
            DALUtil util = new DALUtil();
            Assert.IsTrue(util.AddEmployeePicsToDb());
        }

    }
}
