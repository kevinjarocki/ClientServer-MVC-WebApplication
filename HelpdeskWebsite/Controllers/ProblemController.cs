using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpdeskViewModels;

namespace HelpdeskWebsite.Controllers
{
    public class ProblemController : ApiController
    {
        // GETALL api/<controller>
        [Route("api/problems")]
        public IHttpActionResult GetAll()
        {
            try
            {
                ProblemViewModel prob = new ProblemViewModel();
                List<ProblemViewModel> allProblems = prob.GetAll();
                return Ok(allProblems);
            }
            catch (Exception e)
            {
                return BadRequest("Retrieve failed - " + e.Message);
            }
        }

        
        
    }
}