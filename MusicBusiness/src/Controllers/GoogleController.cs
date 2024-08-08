// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Google.Apis.Auth;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Infrastructure;
// using MongoDB.Driver;
// using MusicBusiness.src.Application.Dtos;
// using MusicBusiness.src.Infrastructure;

// namespace MusicBusiness.src.Configuration.Controllers
// {
//     public class GoogleController : Controller
//     {
//         private readonly IMongoCollection<PrincipalUserGoogle> _userGoogle;
//         private readonly IUserGoogleRepositorie _repositorie;

//         public GoogleController(IMongoClient mongoClient, IUserGoogleRepositorie repositorie)
//         {
//             _repositorie = repositorie;
//             var database = mongoClient.GetDatabase("");
//             _userGoogle = database.GetCollection<PrincipalUserGoogle>("");
//         }

//         [HttpPost("google")]
//         public async Task<IActionResult> GoogleLogin([FromBody] PrincipalUserGoogle google)
//         {
//             try
//             {
//                 var loginGoogle = await _repositorie.PostUserGoogle(google);
//                 return Ok(loginGoogle);
//             } catch (Exception e)
//             {
//                 throw new Exception($"User can't be added in Google Sign-In -> {e}");
//             }
            
            
//         }

//         [HttpGet("profile")]
//         public async Task<IActionResult> GetProfile([FromQuery] string token)
//         {
//             try
//             {
//                 var getUserGoogle = await _repositorie.GetUserByToken(id);
//             }catch (Exception e)
//             {
//                 throw new Exception($"Can't find the user -> {e}");
//             }
//         }
//     }
// }