using GithubJobsEnterpriseProject.Models;
using System.Collections.Generic;

namespace GithubJobsEnterpriseProject.Controllers
{
    public interface IJobApiService
    {

        public IEnumerable<GithubJob> GetGithubJobsFromUrl();

        public IEnumerable<GithubJob> GetGithubJobsByParameters(string descriptionParameter, string locationParameter);


    }
}