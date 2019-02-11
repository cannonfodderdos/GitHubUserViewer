using AutoMapper;
using Core.ApplicationServices;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UserViewer.ViewModels;

namespace UserViewer.Controllers.Web
{
    public class UserController : Controller
    {
        private UserService _service;
        private IMapper _mapper;

        public UserController(UserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> GetUser(string username, bool includeRepos)
        {
            try
            {
                var result = await _service.GetUser(username, includeRepos);
                var user = _mapper.Map<UserViewModel>(result);

                return PartialView("UserData", user);
            }
            catch (ApiException ex)
            {
                var error = "There has been an issue with this request. If this continues please contact support@joebloggs.com.";

                // If user hasn't been found log request and serve this to user
                if (ex.StatusCode == (int)HttpStatusCode.NotFound)
                    error = $"No Github account found for user {username}. Please double check your entry and try again.";

                ViewBag.Error = error;
                return PartialView("UserData", null);
            }
        }
    }
}