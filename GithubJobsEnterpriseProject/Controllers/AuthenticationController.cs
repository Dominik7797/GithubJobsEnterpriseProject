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
        private readonly IUserRepository _userRepository;
        private IHashService _hashService;
        private ILoginService _loginService;

        public AuthenticationController(IUserRepository userRepository, ILoginService loginService, IHashService hashService)
        {
            _userRepository = userRepository;
            _hashService = hashService;
            _loginService = loginService;
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAllUsers();
        }


        [HttpGet("/verify/username={username}&email={email}&password={password}")]
        public bool VerifyUser(string username, string email, string password)
        {
            foreach (var user in _userRepository.GetAllUsers())
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
            _userRepository.Add(new User(username, email, hashedPassword));
        }

        [HttpGet("/login/username={username}&password={password}")]
        public bool Login(string username, string password)
        {
            var users = _userRepository.GetAllUsers().ToList();

            if (_loginService.Login(username,password,users))
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
