﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ploomers_Project_API.Business;
using Ploomers_Project_API.Mappers.DTOs.InputModels;

namespace Ploomers_Project_API.Controllers
{
    [ApiVersion("1")]
    [Route("api/auth/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin(LoginInputModel user)
        {
            if (user == null) return BadRequest("Invalid client request");
            var token = _loginBusiness.ValidateCredentials(user);
            if (token == null) return Unauthorized();
            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(TokenInputModel tokenDto)
        {
            if (tokenDto == null) return BadRequest("Invalid client request");
            var token = _loginBusiness.ValidateCredentials(tokenDto);
            if (token == null) return BadRequest("Invalid client request");
            return Ok(token);
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var email = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(email);

            if (result == false) return BadRequest("Invalid client request");
            return NoContent();
        }
    }
}
