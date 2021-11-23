using CMPG323.P2.Applications.Web.Contracts;
using CMPG323.P2.Services.BusinessLogic;
using CMPG323.P2.Services.DataAccess;
using CMPG323.P2.Services.Library.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPG323.P2.Applications.Web.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {

        #region Private Fields

        private readonly ImageContext _db;

        private readonly IAuthenticationService _authenticationService;

        #endregion

        #region Constructors


        public UsersController(ImageContext db, IAuthenticationService authenticationService)
        {
            _db = db;
            _authenticationService = authenticationService;
        }

        #endregion

        #region Public Methods

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            return Ok(await _authenticationService.AuthenticateApplicationUser(new LoginDetail 
            { 
                Username = loginRequest.Username,
                Password = loginRequest.Password
            }));
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            int userId = await _authenticationService.AuthorizeUser(Request.Headers[HeaderNames.Authorization]) ??
                throw new Exception("Unauthorized");
            return Ok(await _db.Users.SingleOrDefaultAsync(x => x.Id == userId));
        }

        #endregion

    }
}
