
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using bcciApi;  // Assuming your Series1 class is in the bcciApi namespace

namespace bcciApi.Controllers
{
    public class bcciController : ApiController
    {
        private bcciDBEntities db = new bcciDBEntities();

        // GET: api/bcci
        [HttpGet]
        [Route("api/bcci")]
        public IEnumerable<Series1> Get()
        {
            return db.Series1.ToList();
        }

        // GET: api/bcci/5/5
        [HttpGet]
        [Route("api/bcci/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var series = db.Series1.FirstOrDefault(s => s.SeriesID == id);
            if (series == null)
            {
                return NotFound();
            }
            return Ok(series);
        }

       
        // POST: api/bcci
        [HttpPost]
        [Route("api/bcci")]
        public IHttpActionResult Post([FromBody] Series1 series)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Series1.Add(series);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = series.SeriesID }, series);
        }



        // PUT: api/bcci/5
        [HttpPut]
        [Route("api/bcci/{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] Series1 series)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingSeries = db.Series1.FirstOrDefault(s => s.SeriesID == id);
            if (existingSeries == null)
            {
                return NotFound();
            }

            existingSeries.SeriesName = series.SeriesName;
            existingSeries.SeriesStartDate = series.SeriesStartDate;
            existingSeries.SeriesEndDate = series.SeriesEndDate;
            // Update other properties as needed
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/bcci/5
        [HttpDelete]
        [Route("api/bcci/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var series = db.Series1.FirstOrDefault(s => s.SeriesID == id);
            if (series == null)
            {
                return NotFound();
            }

            db.Series1.Remove(series);
            db.SaveChanges();

            return Ok(series);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}




