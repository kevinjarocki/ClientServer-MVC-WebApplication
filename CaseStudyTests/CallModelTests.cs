using System;
using HelpdeskDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CaseStudyTests
{
    [TestClass]
    public class CallModelTests
    {
        [TestMethod]
        public void ComprehensiveModelTestsShouldReturnTrue()
        {
            CallModel cmodel = new CallModel();
            EmployeeModel emodel = new EmployeeModel();
            ProblemModel pmodel = new ProblemModel();
            Call call = new Call();
            call.DateOpened = DateTime.Now;
            call.DateClosed = null;
            call.OpenStatus = true;
            call.EmployeeId = emodel.GetByLastname("Jarocki").Id;
            call.TechId = emodel.GetByLastname("Burner").Id;
            call.ProblemId = pmodel.GetByDescription("Hard Drive Failure").Id;
            call.Notes = "Kevin's drive is shot, Burner to fix it";
            int newCallId = cmodel.Add(call);
            Console.WriteLine("New Call Generated - Id = " + newCallId);
            call = cmodel.GetById(newCallId);
            byte[] oldtimer = call.Timer;
            Console.WriteLine("New Call Retrieved");
            call.Notes += "\n Ordered new RAM!";

            if (cmodel.Update(call) == UpdateStatus.Ok)
            {
                Console.WriteLine("Call was updated " + call.Notes);
            }
            else
            {
                Console.WriteLine("Call was not updated!");
            }

            call.Timer = oldtimer;
            if (cmodel.Update(call) == UpdateStatus.Stale)
            {
                Console.WriteLine("Call was not updated due to stale data");
            }
            cmodel = new CallModel();
            call = cmodel.GetById(newCallId);

            if (cmodel.Delete(newCallId) == 1)
            {
                Console.WriteLine("call was deleted!");
            }
            else
            {
                Console.WriteLine("call was not deleted");
            }

            Assert.IsNull(cmodel.GetById(newCallId));
        }
    }
}
