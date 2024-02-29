using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using WebBcciCons.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;
using System.Web.Http;
using System.Net;
using HttpGet = System.Web.Http.HttpGetAttribute;
using HttpPost = System.Web.Http.HttpPostAttribute;


namespace WebBcciCons.Controllers
{
    public class bcciController : Controller
    {
        // GET: bcci

        Uri baseAddress = new Uri("http://localhost:61681/api");
        HttpClient client;

        public bcciController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;

        }



        [HttpGet]
        // GET: Series
        public ActionResult Index()
        {
            List<series1> modellist = new List<series1>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/bcci").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modellist = JsonConvert.DeserializeObject<List<series1>>(data);
            }
            return View(modellist);


        }


        //create logic 

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(series1 series)
        {
            try
            {
                string data = JsonConvert.SerializeObject(series);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/bcci", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle the case where the creation is not successful
                    ViewBag.ErrorMessage = "Failed to create series.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                ViewBag.ErrorMessage = "An error occurred while creating the series.";
                return View();
            }
        }


        //delete logic

        public ActionResult Delete(int id)
        {
            series1 model = new series1();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/bcci/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<series1>(data);
            }

            return View(model);
        }

        [HttpPost, System.Web.Http.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/bcci/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            // Handle the case where deletion is not successful
            ViewBag.ErrorMessage = "Failed to delete given series.";
            return View();
        }


        //edit logic

        public ActionResult Edit(int id)
        {
            series1 model = new series1();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/bcci/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<series1>(data);
            }

            return View("Create", model);
        }

        [System.Web.Http.HttpPost]
        public ActionResult Edit(series1 series)
        {
            try
            {
                string data = JsonConvert.SerializeObject(series);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/bcci/" + series.MatchID, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                ViewBag.ErrorMessage = "An error occurred while updating the series.";
                return View();
            }
        }


    }
}




