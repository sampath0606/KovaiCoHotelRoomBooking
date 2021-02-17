using UserAuthentication.Exceptions;
using UserAuthentication.Models;
using UserAuthentication.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;


namespace UserAuthentication.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService service;
        private readonly ITokenService tokenService;
        private string Internal_error = "Something went wrong, please contact admin!";

        public AuthController(IAuthService _service, ITokenService _tokenService)
        {
            this.service = _service;
            tokenService = _tokenService;
        }

        // POST api/<controller>
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]User user)
        {
            try
            {
                if (service.IsUserExists(user.username) == true)
                {
                    return StatusCode(499, $"A User Alreay Exists with the Same is : {user.username}");
                }
                else
                {
                    service.RegisterUser(user);
                    return Created("", user);
                    //return StatusCode(201, $"You are successfully Registered");
                }

            }
            catch (UserNotCreatedException unc)
            {
                return StatusCode((int)HttpStatusCode.Conflict, unc.Message);
                // return new ConflictResult();
            }
            catch (System.Exception)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, this.Internal_error);
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]User user)
        {
            try
            {
                User usr = service.LoginUser(user.username, user.Password);
                string tokenvalue = tokenService.GetJWTToken(user.username);
                return Ok(tokenvalue);

            }
            catch (UserNotFoundException unf)
            {
                return StatusCode((int)HttpStatusCode.NotFound, unf.Message);
                //return new NotFoundResult();
            }

            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, this.Internal_error);
            }
        }
        [HttpPost]
        [Route("isAuthenticated")]
        public IActionResult isAuthenticated([FromHeader] string token, string username)
        {
            try
            {

                string tokenvalue = tokenService.GetJWTToken(username);
                bool Authenticated = tokenvalue.Equals(Request.Headers["Authorization"], StringComparison.InvariantCultureIgnoreCase);
                return Ok(Authenticated);

            }
            catch (UserNotFoundException unf)
            {
                return StatusCode((int)HttpStatusCode.NotFound, unf.Message);
                //return new NotFoundResult();
            }

            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, this.Internal_error);
            }
        }
    }
}
