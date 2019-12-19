/*
 *Name: Kevin Jarocki
 *Filename: EmployeeController.cs
 *Purpose: A component of the web layer of HelpdeskWebsite, consists of the
 * following functions: Get, Post, Put and Delete.
 * Date: 2018-10-24
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpdeskViewModels;

namespace HelpdeskWebsite.Controllers
{
 
    public class EmployeeController : ApiController
    {

        //Get function will get a certain employee from the view model layer using the "GetbyLastname" function. Lastname is specified in the parameters.
        [Route("api/employees/{name}")]
        public IHttpActionResult Get(string name)
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                emp.Lastname = name;
                emp.GetByLastname();
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }
        [Route("api/employees/{id}")]
        public IHttpActionResult Get(int id)
        {
            try { 
            EmployeeViewModel emp = new EmployeeViewModel();
            emp.Id = id;
            emp.GetById();
            return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }

        //Put function is primarily used when updating an employee on the client side. An employee is updated using Update() from the view model.
        //This can result in 4 cases from UpdateStatus.cs based on how the end user is updating the database.
        [Route("api/employees")]
        public IHttpActionResult Put(EmployeeViewModel emp)
        {
            try
            {
                int retVal = emp.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok("Employee " + emp.Lastname + " updated!");
                    case -1:
                        return Ok("Employee " + emp.Lastname + " not updated!");
                    case -2:
                        return Ok("Data is stale for " + emp.Lastname + ", Employee not updated!");
                    default:
                        return Ok("Employee " + emp.Lastname + " Not updated!");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Update failed - " + e.Message);
            }
        }


        //Post is primarily used when adding an employee. A certain employee is specified from the view model. 
        //Add() from the view model is used to add an employee into the data layer.
        [Route("api/employees")]
        public IHttpActionResult Post(EmployeeViewModel emp)
        {
            try
            {
                emp.Add();
                if (emp.Id > 0)
                {
                    return Ok("employee " + emp.Lastname + " added!");
                }
                else
                {
                    return Ok("employee " + emp.Lastname + " not added!");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Creation failed - Contact Tech Support");
            }
        }


        //Delete is primarily used to delete an entity from the Data layer so that it in not seen in the view layer.
        //An Id is specified in the parameters and the employee is deleted from the data layer.
        [Route("api/employees/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                emp.Id = id;
                if (emp.Delete() == 1)
                {
                    return Ok("Employee " + id + " deleted!");
                }
                else
                {
                    return Ok("Employee not deleted!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Delete failed- Contact Tech Support");
            }
        }
        [Route("api/employees")]
        public IHttpActionResult GetAll()
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                List<EmployeeViewModel> allEmployees = emp.GetAll();
                return Ok(allEmployees);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }
    }
}
