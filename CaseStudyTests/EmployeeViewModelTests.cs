using System;
using System.Collections.Generic;
using HelpdeskViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CaseStudyTests
{
    [TestClass]
    public class EmployeeViewModelTests
    {
       

        [TestMethod]
        public void EmployeeViewModelAddShouldReturnId()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Title = "Mr.";
            vm.Firstname = "Test";
            vm.Lastname = "Jarocki";
            vm.Email = "ts@abc.com";
            vm.Phoneno = "(555)555-5551";
            vm.DepartmentId = 100;
            vm.Add();
            Assert.IsTrue(vm.Id > 0);
        }

        [TestMethod]
        public void EmployeeViewModelGetAllShouldReturnatLeastOneVm()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            List<EmployeeViewModel> allEmployeeVms = vm.GetAll();
            Assert.IsTrue(allEmployeeVms.Count > 0);
        }

        [TestMethod]
        public void EmployeeViewModelGetbyIdShouldPopulatePropertyFirstname()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Lastname = "Jarocki";
            vm.GetByLastname();
            vm.GetById();
            Assert.IsNotNull(vm.Firstname);
        }

        [TestMethod]
        public void EmployeeViewModelGetbyNameShouldPopulatePropertyFirstname()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Lastname = "Jarocki";
            vm.GetByLastname();
            vm.GetById();
            Assert.IsNotNull(vm.Firstname);

        }

        [TestMethod]
        public void EmployeeModelUpdateShouldReturnOkStatus()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Lastname = "Jarocki";
            vm.GetByLastname();
            vm.Email = (vm.Email.IndexOf(".ca") > 0 ? "ts@abc.com" : "ts@abc.ca");
            int employeesUpdated = vm.Update();
            Assert.IsTrue(employeesUpdated > 0);
        }

        [TestMethod]
        public void EmployeeViewModelUpdateTwiceShouldReturnNegativeTwo()
        {
            EmployeeViewModel vm1 = new EmployeeViewModel();
            EmployeeViewModel vm2 = new EmployeeViewModel();
            vm1.Lastname = "Jarocki";
            vm2.Lastname = "Jarocki";
            vm1.GetByLastname();
            vm2.GetByLastname();
            vm1.Email = (vm1.Email.IndexOf(".ca") > 0) ? "ts@abc.com" : "ts@abc.ca";
            if (vm1.Update() == 1) //update works first time
            {
                vm2.Email = (vm2.Email.IndexOf(".ca") > 0) ? "ts@abc.com" : "ts@abc.ca";
                Assert.IsTrue(vm2.Update() == -2); //-2 = stale
            }
            else
                Assert.Fail();

        }

        [TestMethod]
        public void EmployeeModelDeleteShouldReturnOne()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Lastname = "Jarocki";
            vm.GetByLastname();
            int employeesDeleted = vm.Delete();
            Assert.IsTrue(employeesDeleted == 1);
        }

    }
}
