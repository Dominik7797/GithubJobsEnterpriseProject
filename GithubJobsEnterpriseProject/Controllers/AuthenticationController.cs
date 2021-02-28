using GithubJobsEnterpriseProject.Models;
using GithubJobsEnterpriseProject.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GithubJobsEnterpriseProject.Controllers
{
    [Route("/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserContext _context;
        private HashService _hashService = new HashService();

        public AuthenticationController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }


        [HttpGet("/register/username={username}&email={email}&password={password}")]
        public bool GetVerify(string username, string email, string password)
        {
            foreach (var user in _context.Users)
            {
                if (user.Email == email || user.Username == username)
                {
                    return false;
                }
            }
            Save(username, email, password);
            return true;

        }

        [HttpGet("/getCookieData")]
        public string GetCookieData()
        {
            var user = HttpContext.User;
            return user.Identity.Name;
        }

        public void Save(string username, string email, string password)
        {
            var hashedPassword = _hashService.Hash(password);
            _context.Users.Add(new User(username, email, hashedPassword));
            _context.SaveChanges();
        }

        [HttpGet("/login/username={username}&password={password}")]
        public bool Login(string username, string password)
        {
            var users = _context.Users.ToList();
            var loginService = new LoginService(username, password, users);

            if (loginService.Login())
            {
                CreateCookie(username);
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet("/logout")]
        public RedirectResult Logout(string username, string password)
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }

        private async void CreateCookie(string username)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,username)
                };
            var identity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme
                );
            var principal = new ClaimsPrincipal(identity);
            var props = new AuthenticationProperties();
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
        }

    }
}
