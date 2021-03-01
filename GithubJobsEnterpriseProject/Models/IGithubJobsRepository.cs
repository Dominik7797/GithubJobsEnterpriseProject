using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubJobsEnterpriseProject.Models
{
    public interface IGithubJobsRepository
    {
        GithubJob GetJob(string id);
        IEnumerable<GithubJob> GetAllJobs();
        GithubJob Add(GithubJob job);
        GithubJob UpdateJob(GithubJob updatedJob);
        GithubJob DeleteJob(string id);
    }
}
