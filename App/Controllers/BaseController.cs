using App.Repository;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace App.Controllers
{

    public enum NotificationType
    {
        Default,
        Primary,
        Success,
        Info,
        Warning,
        Error,
    }

    public class BaseController : Controller
    {
        public IAppContext DataContext;

        public BaseController()
        {
            DataContext = new AppContext();
        }

        public ActionResult GoToRefferer()
        {
            if (Request.UrlReferrer != null)
            {
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            }

            return RedirectToAction("Index");
        }

        public ActionResult GoAfterAccessDenied()
        {
            if (Request.UrlReferrer != null)
            {
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            }
            return RedirectToAction("Index", new { Controller = "Main" });
        }

        protected void AssignMessage(NotificationType errorType = NotificationType.Default, string message = null)
        {
            TempData["MessageType"] = String.Format(errorType.ToString()).ToLower();
            TempData["Message"] = String.Format("{0}", message);

        }


        protected void AssignNotification()
        {
            if (TempData.Any(t => t.Key == "MessageType")) { ViewBag.MessageType = TempData["MessageType"]; }
            if (TempData.Any(t => t.Key == "Message")) { ViewBag.Message = TempData["Message"]; }
        }

        protected string BuildErrorMessage(ICollection<ModelState> values)
        {
            string errors = string.Empty;
            foreach (ModelState state in values)
            {
                foreach (ModelError error in state.Errors)
                {
                    errors += error.ErrorMessage;

                }
            }
            return errors;
        }

        protected override void Dispose(bool disposing)
        {
            if (DataContext != null)
            {
                try { DataContext.Dispose(); }
                catch { }
            }
            base.Dispose(disposing);
        }
    }
}
