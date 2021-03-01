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
            this._context = context;
        }

        public GithubJob Add(GithubJob job)
        {
            if (!_context.JobItems.Contains(job))
            {
                _context.JobItems.Add(job);
                _context.SaveChanges();
            }

            return job;
        }

        public GithubJob DeleteJob(string id)
        {
            var jobToDelete = _context.JobItems.Find(id);

            if(jobToDelete != null)
            {
                _context.JobItems.Remove(jobToDelete);
                _context.SaveChanges();
            }

            return jobToDelete;
        }

        public IEnumerable<GithubJob> GetAllJobs()
        {
            return _context.JobItems;
        }

        public GithubJob GetJob(string id)
        {
            return _context.JobItems.Find(id);
        }

        public GithubJob UpdateJob(GithubJob updatedJob)
        {
            var jobToUpdate = _context.JobItems.Attach(updatedJob);
            jobToUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return updatedJob;
        }
    }
}
