using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicBusiness.src.Application.Dtos;
using MusicBusiness.src.Infrastructure;

namespace MusicBusiness.src.Configuration.Controllers
{
    [ApiController, Route("[controller]")]
    public class PrincipalUserController : ControllerBase
    {
        private readonly IUserAcessRepositorie _userAcessRepositorie;
        public PrincipalUserController(IUserAcessRepositorie userAcessRepositorie)
        {
            _userAcessRepositorie = userAcessRepositorie;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var getId = await _userAcessRepositorie.GetUserById(id, HttpContext.RequestAborted);
            return Ok(getId);
        }
        [HttpPost]
        public async Task<IActionResult> PostAcessUser(PrincipalUser principalUser)
        {
            await _userAcessRepositorie.PostUser(principalUser, HttpContext.RequestAborted);
            return CreatedAtAction(nameof(GetById), new { id = principalUser.Id }, principalUser);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAcess(Guid id ,PrincipalUser principalUser)
        {
            await _userAcessRepositorie.UpdateUser(id, principalUser, HttpContext.RequestAborted);
            return NoContent();
        }

        [HttpDelete("deleteUser")]
        public async Task<IActionResult> DeleteDriver([FromQuery]Guid id)
        {
            try
            {
                await _userAcessRepositorie.DeleteUser(id, HttpContext.RequestAborted);
                return Ok();
            }catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}