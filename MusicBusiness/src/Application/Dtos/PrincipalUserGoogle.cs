using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace MusicBusiness.src.Application.Dtos
{
    public class PrincipalUserGoogle : EntityBase
    {
        [EmailAddress]
        public string IdToken { get; set; }
        public string Email { get; init; } = default!;
        public string Password { get; init; } = default!;

        [Display(Name = "Remember me")]
        public bool RememberMe { get; init; } = default!;
        public string ReturnUrl { get; init; } = default!;
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}