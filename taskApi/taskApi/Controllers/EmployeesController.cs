using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL1;
using Microsoft.AspNet.Scaffolding.EntityFramework;


namespace taskApi.Controllers
{
    public class EmployeesController : ApiController

    {
        //[HttpGet]
        public HttpResponseMessage Get(string gender = "All")
        {
            using (empDBEntities entities = new empDBEntities())
            {
                switch (gender.ToLower())
                {

                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.ToList());
      
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Value for gender must be All, Male or Female. " + gender + " is invallid");
                }
            }
        }

        [HttpGet]
        public HttpResponseMessage LoadEmployeeById(int id)
        {
            using (empDBEntities entities = new empDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                if (entity != null)
                {

                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id= " + id.ToString() + "not found");
                }

            }
        }

        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {
                using (empDBEntities entities = new empDBEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (empDBEntities entities = new empDBEntities())
                {

                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id " + id.ToString() + "not found to delete");
                    }
                    else
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }



                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }




        public HttpResponseMessage Put(int id, [FromBody] Employee employee)
        {
            try
            {
                using (empDBEntities entities = new empDBEntities())
                {

                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {

                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id " + id.ToString() + " not found to update");

                    }
                    else
                    {

                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Gender = employee.Gender;
                        entity.Salary = employee.Salary;
                        
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


    }
}
