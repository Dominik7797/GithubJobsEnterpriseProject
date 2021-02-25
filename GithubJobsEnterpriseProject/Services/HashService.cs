using GithubJobsEnterpriseProject.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace GithubJobsEnterpriseProject.Services
{
    public class HashService
    {

        private const int _SALTSIZE = 16;
        private const int _HASHSIZE = 20;

        public string Hash(string toHash)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[_SALTSIZE]);

            // Create hash
            var pbkdf2 = new Rfc2898DeriveBytes(toHash, salt, 100000);
            var hash = pbkdf2.GetBytes(_HASHSIZE);

            // Combine salt and hash
            var hashBytes = new byte[_SALTSIZE + _HASHSIZE];
            Array.Copy(salt, 0, hashBytes, 0, _SALTSIZE);
            Array.Copy(hash, 0, hashBytes, _SALTSIZE, _HASHSIZE);

            // Convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);

            // Format hash with extra information
            return string.Format("$MYHASH$V1${0}${1}", 100000, base64Hash);
        }
    }
}
