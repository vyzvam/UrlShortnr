using App.Models;
using App.Repository;
using App.Services;
using App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class HomeController: BaseController
    {
        UrlService ServiceUnit;

        public HomeController() : base()
        {
            ServiceUnit = new UrlService(DataContext);

        }

        public ActionResult Index()
        {
            UrlShortViewModel vModel = new UrlShortViewModel();

            return View("Form", vModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UrlShortViewModel vModel)
        {
            if (ServiceUnit.GetByName(vModel.Name) != null)
            {
                ModelState.AddModelError(
                    "Name",
                    string.Format("Cannot add '{0}', it already exists!", vModel.Name));
            }

            if (!ModelState.IsValid)
            {
                AssignMessage(NotificationType.Error, String.Format("{0}", BuildErrorMessage(ModelState.Values)));
                AssignNotification();
                return GoToRefferer();
            }

            vModel.SetHandler(0);
            ServiceUnit.Create(vModel);

            return RedirectToAction("Index");

        }    
    }
}
