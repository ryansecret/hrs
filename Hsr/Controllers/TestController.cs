using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hsr.Core;
using Hsr.Data.Interface;
using Hsr.Models;
using  System.Data.Entity;
using Hsr.Models;

namespace Hsr.Controllers
{   
    public class TestController : BaseController
    {
		private readonly IRepository<Test> testRepository;

	 
        public TestController(IRepository<Test> testRepository)
        {
			this.testRepository = testRepository;
        }

        //
        // GET: /Test/

        public ViewResult Index()
        {
            return View(testRepository.Table.Include(test => test.Person));
        }

        //
        // GET: /Test/Details/5

        public ViewResult Details(decimal id)
        {
            return View(testRepository.GetById(id));
        }

        //
        // GET: /Test/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Test/Create

        [HttpPost]
        public ActionResult Create(Test test)
        {
            if (ModelState.IsValid) {
                
                testRepository.Insert(test);
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Test/Edit/5
 
        public ActionResult Edit(decimal id)
        {
             return View(testRepository.GetById(id));
        }

        //
        // POST: /Test/Edit/5

        [HttpPost]
        public ActionResult Edit(Test test)
        {
            if (ModelState.IsValid) {
              testRepository.Update(test);
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Test/Delete/5
 
        public ActionResult Delete(decimal id)
        {
            return View(testRepository.GetById(id));
        }

        //
        // POST: /Test/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(decimal id)
        {
            testRepository.Delete(id);
            
            return RedirectToAction("Index");
        }

        
    }
}

