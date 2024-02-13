using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using MyPassionProject.Models;
using System.Web.Script.Serialization;

namespace MyPassionProject.Controllers
{
    public class VaporizerController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static VaporizerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44324/api/Vaporizerdata/");
        }
        // GET: Vaporizer List
        public ActionResult Index()
        {
            //objective: communicate with our animal data api to retrieve a list of animals
            //curl https://localhost:44324/api/Vaporizerdata/listVaporizer


            string url = "listVaporizers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<VaporizerDto> animals = response.Content.ReadAsAsync<IEnumerable<VaporizerDto>>().Result;
            //Debug.WriteLine("Number of animals received : ");
            //Debug.WriteLine(animals.Count());
            return View();
        }
        // GET: Vaporizer/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our animal data api to retrieve one Vaporizer
            //curl https://localhost:44324/api/Vaporizerdata/findVaporizer/{id}

            string url = "findvaporizer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            VaporizerDto selectedvaporizer = response.Content.ReadAsAsync<VaporizerDto>().Result;
            Debug.WriteLine("animal received : ");
            Debug.WriteLine(selectedvaporizer.VaporizerName);


            return View(selectedvaporizer);

        }
        public ActionResult Error()
        {

            return View();
        }
        // GET: Vaporizer/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Vaporizer/Create
        [HttpPost]
        public ActionResult Create (Vaporizer vaporizer)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(animal.AnimalName);
            //objective: add a new animal into our system using the API
            //curl -H "Content-Type:application/json" -d @animal.json https://localhost:44324/api/Vaporizerdata/addanimal 
            string url = "addvaporizer";


            string jsonpayload = jss.Serialize(vaporizer);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

           
        }
            // GET: Vaporizer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }


    }
}