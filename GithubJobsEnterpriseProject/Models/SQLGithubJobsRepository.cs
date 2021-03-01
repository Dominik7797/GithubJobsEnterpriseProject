using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubJobsEnterpriseProject.Models
{
    public class SQLGithubJobsRepository : IGithubJobsRepository
    {
        private readonly JobContext _context;

        public SQLGithubJobsRepository(JobContext context)
        {
            this.context = context;
        }

        public GithubJob Add(GithubJob job)
        {
            _context.JobItems.Add(job);
            _context.SaveChanges();
            return job;
        }

        public GithubJob DeleteJob(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GithubJob> GetAllJobs()
        {
            throw new NotImplementedException();
        }

        public GithubJob GetJob(int id)
        {
            throw new NotImplementedException();
        }

        public GithubJob UpdateJob(GithubJob updatedJob)
        {
            throw new NotImplementedException();
        }
    }
}
