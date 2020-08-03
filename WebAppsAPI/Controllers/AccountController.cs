using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAppsAPI.DTO;
using WebAppsAPI.Models;

namespace RecipeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AccountController(
          SignInManager<IdentityUser> signInManager,
          UserManager<IdentityUser> userManager,
          IUserRepository userRepository,
          IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _config = config;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model">the login details</param>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> CreateToken(LoginDTO model)
        {
            IdentityUser user = await _userManager.FindByNameAsync(model.Email);

            if (user != null)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (result.Succeeded)
                {
                    string token = GetToken(user);
                    return Created("", token); //returns only the token                    
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="model">the user details</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterDTO model)
        {
            IdentityUser IdentityUser = new IdentityUser { UserName = model.Email, Email = model.Email };
            User user = new User { Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
            IdentityResult result = await _userManager.CreateAsync(IdentityUser, model.Password);

            if (result.Succeeded)
            {
                _userRepository.Add(user);
                _userRepository.SaveChanges();
                string token = GetToken(IdentityUser);
                return Created("", token);
            }
            return BadRequest();
        }

        private string GetToken(IdentityUser IdentityUser)
        {
            // Create the token
            Claim[] claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, IdentityUser.Email),
              new Claim(JwtRegisteredClaimNames.UniqueName, IdentityUser.UserName)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
              null, null,
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        /// <summary>
        /// Checks if an email is available as username
        /// </summary>
        /// <returns>true if the email is not registered yet</returns>
        /// <param name="email">Email.</param>/
        [AllowAnonymous]
        [HttpGet("checkusername")]
        public async Task<ActionResult<bool>> CheckAvailableUserName(string email)
        {
            IdentityUser user = await _userManager.FindByNameAsync(email);
            return user == null;
        }
    }
}