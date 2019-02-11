using Core.ApplicationServices;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace UserViewer.Controllers.API
{
    public class UserController : ApiController
    {
        private UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        // GET: /api/user/:username
        [Route("api/user/{username}")]
        public async Task<IHttpActionResult> Get(string username, bool includeRepos = false)
        {
            try
            {
                var result = await _service.GetUser(username, includeRepos);
                return Ok(result);
            }
            catch(ApiException ex)
            {
                // If user hasn't been found log request and serve this to user
                if (ex.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    Configuration.Services.GetTraceWriter().Warn(Request, "UserService: User requested user that doesn't exist.", ex);
                    return NotFound();
                }

                throw;
            }

        }
    }
}
