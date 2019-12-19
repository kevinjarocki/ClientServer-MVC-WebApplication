using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpdeskWebsite.Reports;

namespace HelpdeskWebsite.Controllers
{
    public class PDFController : ApiController
    {
        [Route("api/employeereport")]
        public IHttpActionResult GetEmployeeReport()
        {
            try
            {
                EmployeeReport rep = new EmployeeReport();
                rep.doIt();
                return Ok("report generated");
            }
            catch (Exception e)
            {
                //Trace.WriteLine("Error " + e.Message);
                return BadRequest("Retrieve failed - Couldn't generate report" + e.Message);
            }
        }

        [Route("api/callreport")]
        public IHttpActionResult GetCallReport()
        {
            try
            {
                CallReport rep = new CallReport();
                rep.doIt();
                return Ok("report generated");
            }
            catch (Exception e)
            {
                //Trace.WriteLine("Error " + e.Message);
                return BadRequest("Retrieve failed - Couldn't generate report" + e.Message);
            }
        }
    }
}
