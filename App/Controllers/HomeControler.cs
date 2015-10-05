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
    /// <summary>
    /// The default and the only controller used.
    /// </summary>
    public class HomeController: BaseController
    {
        UrlService ServiceUnit;

        public HomeController()
            : base()
        {
            ServiceUnit = new UrlService(DataContext);

        }

        /// <summary>
        /// Default action that wil either redirect to a relevant external page or to the short url creation action
        /// </summary>
        /// <param name="id">
        /// string value of the shortKey
        /// </param>
        /// <returns>redirects to the external page, if registered, else goes to the url creation page</returns>
        public ActionResult Index(string id)
        {

            if (!string.IsNullOrEmpty(id))
            {
                UrlShort url = ServiceUnit.GetByShortKey(id);

                if (url != null)
                {
                    return Redirect(url.Name);
                }                    

            }

            return RedirectToAction("Shorten");
        }

        /// <summary>
        /// The short url creation controller
        /// </summary>
        /// <param name="id">
        /// string value of the shortKey
        /// </param>
        /// <returns>page with the viewmodel containing the ulr and the short url (if exists)</returns>
        public ActionResult Shorten(string id)
        {
            UrlShortViewModel vModel = new UrlShortViewModel();

            if (!string.IsNullOrEmpty(id)) {
                UrlShort model = ServiceUnit.GetByShortKey(id);

                if (model != null) {
                    vModel.Name = model.Name;
                    vModel.ShortKey = model.ShortKey;
                }
            }



            return View("Index", vModel);
        }

        /// <summary>
        /// Creates short ulr or retrieves short url from database
        /// </summary>
        /// <param name="vModel">
        /// ViewModel containing the url and the short url
        /// </param>
        /// <returns>Redirects to the url creation action</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UrlShortViewModel vModel)
        {
            if (!ModelState.IsValid)
            {
                AssignMessage(NotificationType.Error, String.Format("{0}", BuildErrorMessage(ModelState.Values)));
                AssignNotification();
                return GoToRefferer();
            }

            var url = ServiceUnit.GetByName(vModel.Name);

            if (url == null)
            {
                vModel.ShortKey = ServiceUnit.RandomString(5);
                vModel.SetHandler(0);
                ServiceUnit.Create(vModel);
            }
            else
            {
                vModel.ShortKey = url.ShortKey;
            }




            return RedirectToAction("Shorten", new {id = vModel.ShortKey});

        }    
    }
}
