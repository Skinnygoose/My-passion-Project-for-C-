using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MyPassionProject.Models;
using System.Diagnostics;
using MyPassionProject.Migrations;

namespace MyPassionProject.Controllers
{
    public class VaporizerDataController : ApiController
    {
        //utilize database connection
        private ApplicationDbContext db = new ApplicationDbContext();


        //list vaporizers

        // GET: api/VaporizerData/ListVaporizer
        [HttpGet]
        public IEnumerable<VaporizerDto> ListVaporizer()
        {
            List<Vaporizer> Vaporizers = db.Vaporizers.ToList();
            List<VaporizerDto> VaporizerDtos = new List<VaporizerDto>();

            Vaporizers.ForEach(v => VaporizerDtos.Add(new VaporizerDto()
            {
                vaporizerID = v.vaporizerId,
                VaporizerName = v.VaporizerName,
                Profit=v.Profit,
                FlavourName=v.FlavourName,
                SupplierName=v.Supplier.SupplierName,
                
            }));

            return VaporizerDtos;
        }

        //find vaporizers
        // GET: api/VaporizerData/FindVaporizer/5

        [ResponseType(typeof(Vaporizer))]
        [HttpGet]
        public IHttpActionResult FindVaporizer(int id)
        {
            Vaporizer Vaporizer = db.Vaporizers.Find(id);
            VaporizerDto VaporizerDto = new VaporizerDto()
            {
                vaporizerID = Vaporizer.vaporizerId,
                VaporizerName = Vaporizer.VaporizerName,
                Profit = Vaporizer.Profit,
                FlavourName = Vaporizer.FlavourName,
                SupplierName = Vaporizer.Supplier.SupplierName,
            };
            if (Vaporizer == null)
                
            {
                return NotFound();
            }
           
             

            return Ok(VaporizerDto);
        }

        //add vaporizer

        // POST: api/VaporizerData/AddVaporizer
        [ResponseType(typeof(Vaporizer))]
        [HttpPost]
        public IHttpActionResult AddVaporizer(Vaporizer Vaporizer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vaporizers.Add(Vaporizer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Vaporizer.vaporizerId }, Vaporizer);
        }
        //Update Vaporizer

        // POST: api/VaporizerData/UpdateVaporizer/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateVaporizer(int id, Vaporizer Vaporizer)
        {
            Debug.WriteLine("I have reached the update animal method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != Vaporizer.vaporizerId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + Vaporizer.vaporizerId);
                Debug.WriteLine("POST parameter" + Vaporizer.VaporizerName);
                Debug.WriteLine("POST parameter " + Vaporizer.FlavourName);
                return BadRequest();
            }

            db.Entry(Vaporizer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(Vaporizer(Id))// I am not able to remove this error i dont understand why this happenning 
                {
                    Debug.WriteLine("Animal not found");
                    return NotFound();
                     
                }
                else
                {
                    throw;
                }
            }

            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }


        //delete vaporizer
        // POST: api/VaporizerData/DeleteVaporizer/5
        [ResponseType(typeof(Vaporizer))]
        [HttpPost]
        public IHttpActionResult DeleteVaporizer(int id)
        {
            Vaporizer animal = db.Vaporizers.Find(id);
            if (animal == null)
            {
                return NotFound();
            }

            db.Vaporizers.Remove(animal);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VaporizerlExists(int id)
        {
            return db.Vaporizers.Count(v => v.vaporizerId == id) > 0;
        }
    }

    //Database transfer class
    public class VaporizerDto
        {
            public int vaporizerID { get; set; }
            public string VaporizerName { get; set; }

            //profit is in percentage
            public decimal Profit { get; set; }

            public string FlavourName { get; set; }
            public string SupplierName { get; set; }

            


        }
    }

