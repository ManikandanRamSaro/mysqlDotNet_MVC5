using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MysqlConnect.Services;
using MysqlConnect.Models;
namespace MysqlConnect.Controllers
{
    public class DefaultController : Controller
    {
        DBService dbs = new DBService();
        // GET: Default
        public ActionResult Index()
        {
            IList<Registeration> coll = dbs.getRecordsOfUser();
            return View("View",coll);
        }

        [HttpGet]
        public ActionResult createNew()
        {
            Registeration reg = new Registeration();
            return View(reg);
        }

        [HttpPost]
        public ActionResult AddNewUser(Registeration reg)
        {
            try
            {
                dbs.addDataUsingQuery(reg);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult updateNewUser(Registeration reg)
        {
            try
            {
                dbs.updateDataUsingQuery(reg);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            Registeration reg = dbs.getRecordsOfUserById(id);
            return View(reg);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Registeration reg = dbs.getRecordsOfUserById(id);
            return View("editDetails", reg);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                dbs.deleteRecordsFromSP(id);
            }
            catch (Exception e)
            {
                Registeration red = new Registeration();
                red.statuss = e.ToString();
                return RedirectToAction("ErrorResponse", red);
            }
            return RedirectToAction("Index");
        }
        public string DB()
        {
            return dbs.checkConnection();
        }
        [HttpGet]
        public string ErrorResponse(Registeration reg)
        {
            return reg.statuss;
        }
    }
}