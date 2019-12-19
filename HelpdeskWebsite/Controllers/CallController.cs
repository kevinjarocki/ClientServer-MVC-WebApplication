using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using HelpdeskViewModels;

namespace HelpdeskWebsite.Controllers
{
    public class CallController : ApiController
    {
       
        // GET api/<controller>/5
        [Route("api/calls/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                CallViewModel call = new CallViewModel();
                call.Id = id;
                call.GetById();
                return Ok(call);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }

        // POST api/<controller>
        [Route("api/calls")]
        public IHttpActionResult Post(CallViewModel call)
        {
            try
            {
                call.Add();
                if (call.Id > 0)
                {
                    return Ok("Call " + call.Id + " logged!");
                }
                else
                {
                    return Ok("Call " + call.Id + " not added!");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Creation failed - Contact Tech Support");
            }
        }

        // PUT api/<controller>/5
        [Route("api/calls")]
        public IHttpActionResult Put(CallViewModel callVm)
        {
            try
            {
                int retVal = callVm.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok("Call " + callVm.Id + " updated!");
                    case -1:
                        return Ok("Call " + callVm.Id + " not updated!");
                    case -2:
                        return Ok("Data is stale for " + callVm.Id + ", Call not updated!");
                    default:
                        return Ok("Call " + callVm.Id + " Not updated!");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Update failed - " + e.Message);
            }
        }

        // DELETE api/<controller>/5
        [Route("api/calls/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                CallViewModel call = new CallViewModel();
                call.Id = id;
                if (call.Delete() == 1)
                {
                    return Ok("Call " + id + " deleted!");
                }
                else
                {
                    return Ok("Call not deleted!");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Delete failed- Contact Tech Support");
            }
        }

        [Route("api/calls")]
        public IHttpActionResult GetAll()
        {
            try
            {
                CallViewModel call = new CallViewModel();
                List<CallViewModel> allCalls = call.GetAll();
                return Ok(allCalls);
            }
            catch (Exception e)
            {
                return BadRequest("Retrieve failed - " + e.Message);
            }
        }
    }
}