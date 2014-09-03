using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hsr.Core;
using Hsr.Data.Interface;
using Hsr.Models;
using System.Data.Entity;
using Hsr.Models;
using System.Linq.Expressions;
namespace Hsr.Controllers
{   
    public class ControllerFilterDataController : BaseController
    {
		private readonly IRepository<ControllerFilterData> testRepository;
		private readonly IRepository<ControllerFilterData> controllerfilterdataRepository;

	 
        public ControllerFilterDataController(IRepository<ControllerFilterData> testRepository, IRepository<ControllerFilterData> controllerfilterdataRepository)
        {
			this.testRepository = testRepository;
			this.controllerfilterdataRepository = controllerfilterdataRepository;
        }

        //
        // GET: /ControllerFilterData/

        public ViewResult Index()
        {
            return View(controllerfilterdataRepository.Table.Take(10).Include(controllerfilterdata => controllerfilterdata.Test));
        }

        //
        // GET: /ControllerFilterData/Details/5

        public ViewResult Details(int? id)
        {
            return View(controllerfilterdataRepository.GetById(id));
        }

        //
        // GET: /ControllerFilterData/Create

        public ActionResult Create()
        {
            
            return View();
        } 

        //
        // POST: /ControllerFilterData/Create

        [HttpPost]
        public ActionResult Create(ControllerFilterData controllerfilterdata)
        {
            if (ModelState.IsValid) {
                
                controllerfilterdataRepository.Insert(controllerfilterdata);
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleTest = testRepository.Table;
				return View();
			}
        }
        
        //
        // GET: /ControllerFilterData/Edit/5
 
        public ActionResult Edit(int? id)
        {
            var data = controllerfilterdataRepository.GetById(id);
            SelectList selectList = new SelectList(testRepository.Table.Take(10).Select(d => new { method = d.MethodeName}).Distinct(), "method", "method");
          
            ViewBag.PossibleTest = selectList;
           // ViewBag.PossibleTest = controllerfilterdataRepository.Table.Take(10).Select(d => d.Test);
             return View(data);
        }

        //
        // POST: /ControllerFilterData/Edit/5

        [HttpPost]
        public ActionResult Edit(ControllerFilterData controllerfilterdata)
        {
            if (ModelState.IsValid) {
              controllerfilterdataRepository.Update(controllerfilterdata);
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleTest = testRepository.Table;
				return View();
			}
        }

        //
        // GET: /ControllerFilterData/Delete/5
 
        public ActionResult Delete(int? id)
        {
            return View(controllerfilterdataRepository.GetById(id));
        }

        //
        // POST: /ControllerFilterData/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            controllerfilterdataRepository.Delete(id);
            
            return RedirectToAction("Index");
        }

        
    }
}

