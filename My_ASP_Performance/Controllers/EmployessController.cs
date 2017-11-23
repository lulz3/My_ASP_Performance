using DataAcces.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Data.SqlTypes;

namespace My_ASP_Performance.Controllers
{
    public class EmployessController : Controller
    {
        // GET: Employess
        public ActionResult Index()
        {
            return View(Persons.GetEmpFromDTB);
        }

        public ActionResult Detail(int id)
        {
            Person per = (from Person person in Persons.GetEmpFromDTB where person.Id == id select person).FirstOrDefault();
            return View(per);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Person p)
        {
            if (ModelState.IsValid)
            {
                DateTime myDateTime = DateTime.Now;
                var sqlFormattedDate = myDateTime.Date.ToString("yyyy-MM-dd HH:mm:ss");
                //p.Id = Persons.Counter;
                SqlDateTime a = new SqlDateTime(DateTime.Now);
                p.Registred = a;
                Persons.AddEmpToDTB().Add(p);
            }
            else
            {
                return View("Create", p);
            }
            return RedirectToAction("Index");
        }
    }
}