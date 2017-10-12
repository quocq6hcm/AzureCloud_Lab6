using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Specialized;

namespace WebRole_Lab6.Controllers
{
    public class HomeController : Controller
    {
        WebClient wc = new WebClient();
        
        private const string url = "http://localhost:50497/Service1.svc/";
        // GET: Home
        public ActionResult Index()
        {
            var model = JsonConvert.DeserializeObject
                        <IEnumerable<Models.Employee>>
                        (wc.DownloadString(url + "GetEmployees"));
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.Employee newEmployee)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(Models.Employee));
                dcjs.WriteObject(ms, newEmployee);
                wc.Headers["content-type"] = "application/json";
                wc.UploadData(url +"PostEmployee", "POST" , ms.ToArray());

                ModelState.AddModelError("", "Add ok");
            }
            catch (Exception)
            {
                
                ModelState.AddModelError("", "error occured");
            }

            return View();
        }

        public ActionResult Details(int id)
        {
            return View(JsonConvert.DeserializeObject
                        <Models.Employee>(wc.DownloadString(url + "GetEmployee/" +id)));
        }

        [HttpGet]
        public ActionResult Search(string txtSearch)
        {
           
            if (string.IsNullOrEmpty(txtSearch))
            {
                return View("Index", JsonConvert.DeserializeObject
                        <IEnumerable<Models.Employee>>
                        (wc.DownloadString(url + "GetEmployees")));
            }
            else
            {
                return View("Index", JsonConvert.DeserializeObject
                        <IEnumerable<Models.Employee>>
                        (wc.DownloadString(url + "GetByName/" + txtSearch)));
            }
        }

        
        public ActionResult Delete(int id)
        {
            wc.UploadValues(url + "Delete/" + id, "DELETE", new NameValueCollection());
            return RedirectToAction("Index");
        }
        
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Models.Employee e)
        {
            return View();
        }

    }
}