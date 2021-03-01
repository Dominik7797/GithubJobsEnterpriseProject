using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubJobsEnterpriseProject.Models
{
    public class JobContext : DbContext
    {
        public DbSet<GithubJob> JobItems { get; set; }
        public JobContext(DbContextOptions<JobContext> options)
            : base(options)
        {
        }

        
    }
}
