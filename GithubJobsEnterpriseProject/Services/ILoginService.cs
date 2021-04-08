using GithubJobsEnterpriseProject.Models;
using System.Collections.Generic;

namespace GithubJobsEnterpriseProject.Services
{
    public interface ILoginService
    {
        bool Login(string username, string password);
    }
}