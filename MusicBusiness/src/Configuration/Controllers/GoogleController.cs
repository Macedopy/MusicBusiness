using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicBusiness.src.Application.Dtos;

namespace MusicBusiness.src.Configuration.Controllers
{
    public class GoogleController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = "/")
        {
            PrincipalUserGoogle google = new PrincipalUserGoogle
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await SignInManager)
            };
            var properties = new AuthenticationProperties {RedirectUri = returnUrl};
            return Challenge
        }
    }
}