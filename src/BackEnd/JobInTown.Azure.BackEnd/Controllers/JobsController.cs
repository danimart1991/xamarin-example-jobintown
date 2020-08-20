using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using JobInTown.Azure.BackEnd.Models;
using JobInTown.Azure.BackEnd.Models.Results;

namespace JobInTown.Azure.BackEnd.Controllers
{
    public class JobsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/Jobs
        public IEnumerable<Job> GetJobs()
        {
            return db.Jobs;
        }

        // GET api/My/Jobs
        [Route("~/api/my/jobs")]
        [Authorize]
        public IEnumerable<Job> GetMyJobs()
        {
            var userName = User?.Identity?.Name;
            return db.Jobs?.Where(x => x.UserName == userName);
        }

        // GET api/Jobs/5
        [ResponseType(typeof(JobResponse))]
        public IHttpActionResult GetJob(int id)
        {
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return NotFound();
            }

            var user = db.Users.SingleOrDefault(x => x.UserName == job.UserName);

            JobResponse jobResponse = new JobResponse()
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                PostedDate = job.PostedDate,
                Category = job.Category,
                Contract = job.Contract,
                WorkDay = job.WorkDay,
                ImageUrl = job.ImageUrl,
                Location = job.Location,
                Latitude = job.Latitude,
                Longitude = job.Longitude,
                User = new UserResponse()
                {
                    Email = user.Email,
                    FullName = user.FullName,
                    ImageUrl = user.ImageUrl
                }
            };

            return Ok(jobResponse);
        }

        // PUT: api/Jobs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJob(int id, Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != job.Id)
            {
                return BadRequest();
            }

            db.Entry(job).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Jobs
        [Authorize]
        [ResponseType(typeof(Job))]
        public IHttpActionResult PostJob(Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            job.UserName = User.Identity.Name;
            job.PostedDate = DateTime.UtcNow;

            db.Jobs.Add(job);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = job.Id }, job);
        }

        // DELETE: api/Jobs/5
        [Authorize]
        [ResponseType(typeof(Job))]
        public IHttpActionResult DeleteJob(int id)
        {
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return NotFound();
            }

            if (User.Identity.Name != job.UserName)
            {
                return Unauthorized();
            }

            db.Jobs.Remove(job);
            db.SaveChanges();

            return Ok(job);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobExists(int id)
        {
            return db.Jobs.Count(e => e.Id == id) > 0;
        }
    }
}
