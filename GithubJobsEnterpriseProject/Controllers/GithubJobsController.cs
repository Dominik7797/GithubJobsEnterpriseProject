using GithubJobsEnterpriseProject.Models;
using GithubJobsEnterpriseProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GithubJobsEnterpriseProject.Controllers
{
    [Route("api")]
    [ApiController]
    public class GithubJobsController : ControllerBase, IGitHubJobsController
    {
        private readonly JobContext _context;
        private readonly UserContext _userContext;
        private readonly IJobApiService _apiService;
             
        public GithubJobsController(JobContext context,UserContext userContext, IJobApiService apiService)
        {

            _context = context;
            _userContext = userContext;
            _apiService = apiService;
        }

        // GET: api/GithubJobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GithubJob>>> GetJobItems()
        {

            GetJobs();
            return await _context.JobItems.ToListAsync();
        }

        public void GetJobs()
        {
            var items = _context.JobItems;
            if (items != null)
            {
                _context.RemoveRange(_context.JobItems);
            }
            IEnumerable<GithubJob> GithubJobs = _apiService.GetGithubJobsFromUrl();

            foreach (GithubJob job in GithubJobs)
            {
                _context.JobItems.AddRange(job);
                
                _context.SaveChanges();
            }
        }

        // GET: api/GithubJobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GithubJob>> GetGithubJob(string id)
        {
            var githubJob = await _context.JobItems.FindAsync(id);

            if (githubJob == null)
            {
                return NotFound();
            }

            return githubJob;
        }

        [HttpGet("description={description}&location={location}")]
        public async Task<ActionResult<IEnumerable<GithubJob>>> GetGithubJobByDescriptionAndPlace([FromRoute] string description,
                                                                                                  [FromRoute] string location)
        {
            var items = _context.JobItems;
            if (items != null)
            {
                _context.RemoveRange(_context.JobItems);
            }
            IEnumerable<GithubJob> GithubJobs = _apiService.GetGithubJobsByParameters(description, location);

            foreach (GithubJob job in GithubJobs)
            {
                _context.JobItems.AddRange(job);
                _context.SaveChanges();
            }
            return await _context.JobItems.ToListAsync();
        }




        // PUT: api/GithubJobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGithubJob(string id, GithubJob githubJob)
        {
            if (id != githubJob.Id)
            {
                return BadRequest();
            }

            _context.Entry(githubJob).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GithubJobExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GithubJobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GithubJob>> PostGithubJob(GithubJob githubJob)
        {
            _context.JobItems.Add(githubJob);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GithubJobExists(githubJob.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGithubJob", new { id = githubJob.Id }, githubJob);
        }

        // DELETE: api/GithubJobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGithubJob(string id)
        {
            var githubJob = await _context.JobItems.FindAsync(id);
            if (githubJob == null)
            {
                return NotFound();
            }

            _context.JobItems.Remove(githubJob);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GithubJobExists(string id)
        {
            return _context.JobItems.Any(e => e.Id == id);
        }

        [HttpPost("/registration")]
        public ActionResult GetCredentials()
        {
            var username = Request.Form["Username"];
            var email = Request.Form["Email"];
            var password = Request.Form["Password"];

            Save(username, email, password);

            return Redirect("/");

        }

        public void Save(string username, string email, string password)
        {
            var hashedPassword = new PasswordHandlerService(password).HashUserGivenPassword();
            _userContext.Users.Add(new User(username, email, hashedPassword));
            _userContext.SaveChanges();
        }

        [HttpPost("/login")]
        public ActionResult GetLoginCredentials()
        {

            var username = Request.Form["Username"];
            var password = Request.Form["Password"];

            Login(username, password);

            return NoContent();

        }

        private void Login(string username, string password)
        {
            var users = _userContext.Users.ToList();
            var loginService = new LoginService(username, password, users);
            if (loginService.Login()) {
                Console.WriteLine("Works");
            }
        }

    }

   
}
