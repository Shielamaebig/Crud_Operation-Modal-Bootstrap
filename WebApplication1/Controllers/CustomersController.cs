using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data.Entity;

namespace WebApplication1.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _db;

        public CustomersController()
        {
            _db = new ApplicationDbContext();
        }
        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Getdata()
        {
            List<Customers> customers = _db.Customers.Include(m => m.Gender).ToList<Customers>();
            return Json(new { data = customers }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult postdata(Customers customers)
        {
            if (ModelState.IsValid)
            {
                var customerInDb = new Customers();
                customerInDb.Name = customers.Name;
                customerInDb.Address = customers.Address;
                customerInDb.GenderId = customers.GenderId;
                customerInDb.City = customers.City;
                customerInDb.Country = customers.Country;
                _db.Customers.Add(customers);
                _db.SaveChanges();
            }
            return Json("success", JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult Editdata(int id, Customers customers)
        {
            var updateInDb = _db.Customers.Where(m => m.Id == customers.Id).FirstOrDefault<Customers>();
            updateInDb.Name = customers.Name;
            updateInDb.Address = customers.Address;
            updateInDb.GenderId = customers.GenderId;
            updateInDb.City = customers.City;
            updateInDb.Country = customers.Country;
            _db.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGender()
        {

            return Json(_db.Genders.Select(x => new
            {
                GenderId = x.Id,
                GenderName = x.Name
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var data = _db.Customers.FirstOrDefault(x => x.Id == Id);

            if (data != null)
            {
                _db.Customers.Remove(data);
                _db.SaveChanges();
            }
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEdit(int Id) //get data Id
        {
            var customer = _db.Customers.Where(m => m.Id == Id).FirstOrDefault();

            return Json(customer, JsonRequestBehavior.AllowGet);

        }
    }
}