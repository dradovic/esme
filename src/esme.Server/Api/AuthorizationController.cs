﻿using esme.Infrastructure.Data;
using esme.Shared.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace esme.Server.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthorizationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginParameters parameters)
        {
            // FIXME: da, test if !ModelState.IsValid with current configuration
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(state => state.Errors)
                .Select(error => error.ErrorMessage)
                .FirstOrDefault());

            var user = await _userManager.FindByNameAsync(parameters.UserName);
            if (user == null) return BadRequest("Invalid user or password"); // FIXME: da, i18n
            var checkPasswordResult = await _signInManager.CheckPasswordSignInAsync(user, parameters.Password, false);
            if (!checkPasswordResult.Succeeded) return BadRequest("Invalid user or password"); // FIXME: da, i18n

            await _signInManager.SignInAsync(user, parameters.RememberMe);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Signup(SignupParameters parameters)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(state => state.Errors)
                .Select(error => error.ErrorMessage)
                .FirstOrDefault()); // FIXME: da, copy-paste from Login

            var user = new ApplicationUser();
            user.UserName = parameters.UserName;
            var result = await _userManager.CreateAsync(user, parameters.Password);
            if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);

            return await Login(new LoginParameters
            {
                UserName = parameters.UserName,
                Password = parameters.Password
            });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [AllowAnonymous] // this is ok since the client will just get information about itself
        [HttpGet]
        public ActionResult<UserViewModel> Me()
        {
            return new UserViewModel
            {
                UserName = User.Identity.Name,
                IsAuthenticated = User.Identity.IsAuthenticated,
                Claims = User.Claims.ToDictionary(c => c.Type, c => c.Value)
            };
        }
    }
}