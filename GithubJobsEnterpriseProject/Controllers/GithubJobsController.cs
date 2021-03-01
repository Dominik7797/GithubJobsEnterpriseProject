using GithubJobsEnterpriseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GithubJobsEnterpriseProject.Controllers
{
    [Route("/api")]
    [ApiController]
    public class GithubJobsController : ControllerBase
    {
        private readonly IGithubJobsRepository _githubJobsRepository;
        private readonly IJobApiService _apiService;

        public GithubJobsController(IGithubJobsRepository githubJobsRepository, IJobApiService apiService)
        {
            _githubJobsRepository = githubJobsRepository;
            _apiService = apiService;
        }

        [HttpGet]
        public List<GithubJob> GetJobs()
        {
            var items = _githubJobsRepository.GetAllJobs();
            IEnumerable<GithubJob> GithubJobs = _apiService.GetGithubJobsFromUrl();

            foreach (GithubJob job in GithubJobs)
            {
                if (!items.Contains(job))
                {
                    _githubJobsRepository.Add(job);
                }
            }

            return _githubJobsRepository.GetAllJobs().ToList();
        }

        [HttpGet("{id}")]
        public GithubJob GetGithubJob(string id)
        {
            return _githubJobsRepository.GetJob(id);
        }

        [HttpGet("/search/description={description}&location={location}")]
        public void GetSearchResult(string description, string location)
        {
            var allJobs = _githubJobsRepository.GetAllJobs();
            var searchedJobResults = new List<GithubJob>();


            foreach (var job in allJobs)
            {
                var jobLocation = job.Location.ToLowerInvariant();

                if (job.Description.Contains(description) || jobLocation == location.ToLowerInvariant())
                {
                    searchedJobResults.Add(job);
                }
            }
        }

        [HttpDelete("{id}")]
        public GithubJob DeleteGithubJob(string id)
        {
            return _githubJobsRepository.DeleteJob(id);
        }
    }
}
