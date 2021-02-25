using GithubJobsEnterpriseProject.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace GithubJobsEnterpriseProject.Services
{
    public class LoginService
    {

        private const int _SALTSIZE = 16;

        private const int _HASHSIZE = 20;
        private string _username { get; set; }
        private string _password { get; set; }
        private List<User> _userList { get; set; }

        public LoginService(string username, string password,List<User> users)
        {
            _password = password;
            _username = username;
            _userList = users;
        }

        public bool Login()
        {
            string savedPasswordHash = "";

            foreach (var user in _userList)
            {
                if(user.Username == _username)
                {
                    savedPasswordHash = user.Password;
                }
            }

            var splittedHashString = savedPasswordHash.Replace("$MYHASH$V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            // Get hash bytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            // Get salt
            var salt = new byte[_SALTSIZE];
            Array.Copy(hashBytes, 0, salt, 0, _SALTSIZE);

            // Create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(_password, salt, iterations);
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
    }
}
