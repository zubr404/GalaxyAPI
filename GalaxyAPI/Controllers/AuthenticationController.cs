using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GalaxyAPI.DTO;
using GalaxyAPI.Models;
using GalaxyAPI.Repositories;
using GalaxyAPI.Services.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GalaxyAPI.Controllers
{
    [Route("api/v1/auth/")]
    [Produces("application/json")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        DataBaseContext db;
        UserRepository userRepo; // !!!

        public AuthenticationController(DataBaseContext dataBase)
        {
            db = dataBase;
            userRepo = new UserRepository(db);
        }

        /// <summary>
        /// Получить токен авторизации
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("token")]
        public ActionResult<UserAuthResponse> Token(string login, string password)
        {
            if(string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest(new { ErrorMessage = "Не указан Логин или Пароль." });
            }

            var identity = GetIdentity(login, password);
            if (identity == null)
            {
                return BadRequest(new { ErrorMessage = "Неверный Логин или Пароль." });
            }

            var signingKey = new SigningSymmetricKey(AuthOptions.KEY);
            var now = DateTime.UtcNow;
            
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(signingKey.GetKey(), signingKey.SigningAlgorithm));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var user = new UserAuthResponse()
            {
                ID = identity.GetUserId<int>(),
                BirthDate = identity.GetUserBirthDate(),
                AuthToken = encodedJwt,
                FullName = identity.Name,
                RoleID = identity.GetUserRoleId<int>(),
                Amount = identity.GetUserAmount<double>()
            };

            return user;
        }

        [NonAction]
        private ClaimsIdentity GetIdentity(string login, string password)
        {
            var user = userRepo.Get(login, password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.FullName, ClaimValueTypes.String),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleID.ToString(), ClaimValueTypes.String),
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString(), ClaimValueTypes.String),
                    new Claim(ClaimTypes.DateOfBirth, user.BirthDate, ClaimValueTypes.String),
                    new Claim(ClaimTypesMy.Amount.ToString(), user.Amount.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}