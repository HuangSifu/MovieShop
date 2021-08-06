using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestModel model)
        {
            var createdUser = await _userService.RegisterUser(model);

            // send the URL for newly created user also
            // 5000

            return CreatedAtRoute("GetUser", new { id = createdUser.Id }, createdUser);

            // 201
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetUser")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound($"User does not exists for {id}");
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.Login(model.Email, model.Password);
                return Ok(user);
            }
            return BadRequest("Your entered information dosn't exist, please check again.");
        }

        private string GenerateJWT(UserLoginResponseModel model)
        {
            //we will use the token libriaries to create tokens

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,model.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,model.Email),
                new Claim(JwtRegisteredClaimNames.GivenName,model.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName,model.LastName)
            };
            //create identity object and strore claims
            var indentityClaim = new ClaimsIdentity();
            indentityClaim.AddClaims(claims);

            //read the secret key from appsetting, make sure secret key is unique and nor guessable
            //In real world we use something like Azure KeyVaule to store any secret of application
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            //get the expiration time of the token
            var expires = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("JwtSettings:Expiration"));
            //pick an hashing algorithm
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //create the token object that will use to create the token that will include all the information
            //such as credentials, claims, expiration time
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = indentityClaim,
                Expires = expires,
                SigningCredentials = credentials,
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"]

            };
            var encodedJwt = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(encodedJwt);
        }
    }
}

