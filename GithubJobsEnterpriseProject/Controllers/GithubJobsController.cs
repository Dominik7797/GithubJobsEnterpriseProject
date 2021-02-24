using GithubJobsEnterpriseProject.Models;
using GithubJobsEnterpriseProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class GithubJobsController : ControllerBase
    {
        private readonly JobContext _context;
        private HashService _hashService = new HashService();
        private readonly UserContext _userContext;
        private readonly IJobApiService _apiService;
             
        public GithubJobsController(JobContext context,UserContext userContext, IJobApiService apiService)
        {

            _context = context;
            _userContext = userContext;
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<List<GithubJob>> GetJobsAsync()
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

            return await _context.JobItems.ToListAsync();
        }

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

        [HttpGet("/username={username}&email={email}&password={password}")]
        public bool GetVerify(string username, string email, string password)
        {
            foreach (var user in _userContext.Users)
            {
                if (user.Email == email || user.Username == username)
                {
                    return false;
                }
            }
            Save(username,email,password);
            return true;

        }

        public void Save(string username, string email, string password)
        {
            var hashedPassword = _hashService.Hash(password);
            _userContext.Users.Add(new User(username, email, hashedPassword));
            _userContext.SaveChanges();
        }

        [HttpGet("/login/username={username}&password={password}")]
        public bool Login(string username, string password)
        {
            var users = _userContext.Users.ToList();
            var loginService = new LoginService(username, password, users);
            
            if (loginService.Login())
            {
                return true;
            }else
            {
                return false;
            }
        }

    }

   
}
