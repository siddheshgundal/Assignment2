using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebEmpmvc.Models;

namespace WebEmpmvc.Controllers
{
    public class EmployeesController : Controller

    {
        Uri baseAddress = new Uri("http://localhost:51400/api");
        HttpClient client;

        public EmployeesController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        [HttpGet]
        // GET: Employees
        public ActionResult Index()
        {
            List<Employee> modelList = new List<Employee>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Employees").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<Employee>>(data);
            }
            return View(modelList);
        }

        public ActionResult update()
        {
            List<Employee> modelList = new List<Employee>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Employees/id").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<Employee>>(data);
            }
            return View(modelList);
        }


        public ActionResult edit(int id)
        {
            List<Employee> modelList = new List<Employee>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Employees").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<Employee>>(data);
            }
            return View(modelList);
        }

    }
}



