using DataAcces.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace My_ASP_Performance.Controllers
{
    public class EmployessController : Controller
    {
        // GET: Employess
        public ActionResult Index()
        {
            return View(Commands.GetEmpFromDTB);
        }

        public ActionResult Detail(int id)
        {
            Person per = (from Person person in Commands.GetEmpFromDTB where person.Id == id select person).FirstOrDefault();
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
                p.Registred = DateTime.Now;
                Commands.AddEmpToDTB(p);
            }
            else
            {
                return View("Create", p);
            }
            return RedirectToAction("Index");
        }
    }
}