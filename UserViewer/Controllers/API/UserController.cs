using AutoMapper;
using Core.ApplicationServices;
using Core.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Tracing;
using UserViewer.ViewModels;

namespace UserViewer.Controllers.API
{
    public class UserController : ApiController
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

        // GET: /api/user/:username
        [Route("api/user/{username}")]
        public async Task<IHttpActionResult> Get(string username, bool includeRepos = false)
        {
            try
            {
                var result = await _service.GetUser(username, includeRepos);
                var user = _mapper.Map<UserViewModel>(result);
                return Ok(user);
            }
            catch(ApiException ex)
            {
                // If user hasn't been found log request and serve this to user
                if (ex.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    _logger.LogWarning(ex, $"Request for user that doesn't exist: {username}", null);
                    return NotFound();
                }

                throw;
            }

        }
    }
}
