using CountyAndTownshipSample.Model.DB;
using CountyAndTownshipSample.Models;
using lib.CountyAndTownship.Models;
using lib.CountyAndTownship.Service;
using lib.CountyAndTownship.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CountyAndTownshipSample.Controllers
{
    public class HomeController : Controller
    {
        MyContext _context = null;
        MyContext context
        {
            get
            {
                if (_context == null)
                {
                    _context = new MyContext();
                }

                return _context;
            }
        }

        protected override void Dispose(bool disposing)
        {


            base.Dispose(disposing);

            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public ActionResult Index()
        {
            ICountyAndTownshipService countyAndTownshipService = new CountyAndTownshipService(context);

            var model = new HomeViewModels()
            {
                Address = countyAndTownshipService.GetFullAddress(2, 5, "XXX路XXX號")
            };
            countyAndTownshipService.Bind(model);

            return View(model);
        }
        
    }
}