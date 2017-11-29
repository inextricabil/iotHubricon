using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using iotHubricon.Models;

namespace iotHubricon.Controllers
{
    public class SensorRecordsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SensorRecords
        public IQueryable<SensorRecord> GetSensorRecords()
        {
            return db.SensorRecords;
        }

        // GET: api/SensorRecords/5
        [ResponseType(typeof(SensorRecord))]
        public async Task<IHttpActionResult> GetSensorRecord(Guid id)
        {
            SensorRecord sensorRecord = await db.SensorRecords.FindAsync(id);
            if (sensorRecord == null)
            {
                return NotFound();
            }

            return Ok(sensorRecord);
        }

        // PUT: api/SensorRecords/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSensorRecord(Guid id, SensorRecord sensorRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sensorRecord.SensorId)
            {
                return BadRequest();
            }

            db.Entry(sensorRecord).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Trace.WriteLine(DateTime.Now + e.Message);
                return InternalServerError();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SensorRecords
        [ResponseType(typeof(SensorRecord))]
        public async Task<IHttpActionResult> PostSensorRecord(SensorRecord sensorRecord)
        {
            sensorRecord.SensorRecordId = Guid.NewGuid();
            sensorRecord.Date = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SensorRecords.Add(sensorRecord);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            { 
                Trace.WriteLine(DateTime.Now + e.Message);
                return InternalServerError();
            }

            return CreatedAtRoute("DefaultApi", new { id = sensorRecord.SensorId }, sensorRecord);
        }

        // DELETE: api/SensorRecords/5
        [ResponseType(typeof(SensorRecord))]
        public async Task<IHttpActionResult> DeleteSensorRecord(Guid id)
        {
            SensorRecord sensorRecord = await db.SensorRecords.FindAsync(id);
            if (sensorRecord == null)
            {
                return NotFound();
            }

            db.SensorRecords.Remove(sensorRecord);
            await db.SaveChangesAsync();

            return Ok(sensorRecord);
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