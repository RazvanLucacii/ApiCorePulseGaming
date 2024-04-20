using ApiCorePulseGaming.Helpers;
using ApiCorePulseGaming.Models;
using ApiCorePulseGaming.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiCorePulseGaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IRepositoryUsuarios repo;
        private HelperActionServicesOAuth helper;

        public AuthController(IRepositoryUsuarios repo, HelperActionServicesOAuth helper)
        {
            this.repo = repo;
            this.helper = helper;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            Usuario usuario = await this.repo.LogInUserAsync(model.Email, model.Password);
            if (usuario == null)
            {
                return Unauthorized();
            }
            else
            {
                SigningCredentials credentials =
                    new SigningCredentials(
                        this.helper.GetKeyToken(),
                        SecurityAlgorithms.HmacSha256);

                string jsonUsuario = JsonConvert.SerializeObject(usuario);

                Claim[] informacion = new[]
                {
                    new Claim("UserData", jsonUsuario)
                };

                JwtSecurityToken token = new JwtSecurityToken(
                                            claims: informacion,
                                            issuer: this.helper.Issuer,
                                            audience: this.helper.Audience,
                                            signingCredentials: credentials,
                                            expires: DateTime.UtcNow.AddMinutes(30),
                                            notBefore: DateTime.UtcNow
                                            );
                return Ok(
                    new
                    {
                        response = new JwtSecurityTokenHandler().WriteToken(token)
                    });
            }
        }
    }
}
