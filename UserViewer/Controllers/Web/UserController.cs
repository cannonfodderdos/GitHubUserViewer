using AutoMapper;
using Core.ApplicationServices;
using Core.Common;
using Microsoft.Extensions.Logging;
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
        private ILogger _logger;

        public UserController(UserService service, IMapper mapper, ILogger logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Vue()
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

                _logger.LogWarning(ex, $"Request for user that doesn't exist: {username}", null);

                return PartialView("UserData", null);
            }
        }
    }
}