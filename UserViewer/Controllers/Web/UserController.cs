using Core.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserViewer.Controllers.Web
{
    public class UserController : Controller
    {
        private UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}