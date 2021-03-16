using GithubJobsEnterpriseProject.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace GithubJobsEnterpriseProject.Services
{
    public class LoginService : ILoginService
    {

        private const int _SALTSIZE = 16;
        private const int _HASHSIZE = 20;
        private readonly UserContext _context;

        public LoginService(UserContext userContext)
        {
            _context = userContext;
        }

        public bool Login(string username, string password)
        {
            string savedPasswordHash = "";

            foreach (var user in _context.Users)
            {
                if (user.Username == username)
                {
                    savedPasswordHash = user.Password;
                }
            }
            try
            {
                var splittedHashString = savedPasswordHash.Replace("$MYHASH$V1$", "").Split('$');
                var iterations = int.Parse(splittedHashString[0]);
                var base64Hash = splittedHashString[1];
                // Get hash bytes
                var hashBytes = Convert.FromBase64String(base64Hash);

                // Get salt
                var salt = new byte[_SALTSIZE];
                Array.Copy(hashBytes, 0, salt, 0, _SALTSIZE);

                // Create hash with given salt
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
                byte[] hash = pbkdf2.GetBytes(_HASHSIZE);

                // Get result
                for (var i = 0; i < _HASHSIZE; i++)
                {
                    if (hashBytes[i + _SALTSIZE] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
    }
}
