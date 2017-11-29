using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using iotHubricon.Models;

namespace iotHubricon.Controllers
{
    public class SensorRecordsMvcController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SensorRecordsMvc
        public ActionResult Index()
        {
            return View(db.SensorRecords.OrderByDescending(p => p.Date).ToList());
        }

        // GET: SensorRecordsMvc/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorRecord sensorRecord = db.SensorRecords.Find(id);
            if (sensorRecord == null)
            {
                return HttpNotFound();
            }
            return View(sensorRecord);
        }

        // GET: SensorRecordsMvc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SensorRecordsMvc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SensorId,Temperature,Humidity")] SensorRecord sensorRecord)
        {
            sensorRecord.SensorRecordId = Guid.NewGuid();
            sensorRecord.Date = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                sensorRecord.SensorId = Guid.NewGuid();
                db.SensorRecords.Add(sensorRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sensorRecord);
        }

        // GET: SensorRecordsMvc/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorRecord sensorRecord = db.SensorRecords.Find(id);
            if (sensorRecord == null)
            {
                return HttpNotFound();
            }
            return View(sensorRecord);
        }

        // POST: SensorRecordsMvc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SensorRecordId, SensorId,Temperature,Humidity")] SensorRecord sensorRecord)
        {
            sensorRecord.Date = DateTime.UtcNow;
            if (ModelState.IsValid)
            {
                db.Entry(sensorRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sensorRecord);
        }

        // GET: SensorRecordsMvc/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorRecord sensorRecord = db.SensorRecords.Find(id);
            if (sensorRecord == null)
            {
                return HttpNotFound();
            }
            return View(sensorRecord);
        }

        // POST: SensorRecordsMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SensorRecord sensorRecord = db.SensorRecords.Find(id);
            db.SensorRecords.Remove(sensorRecord);
            db.SaveChanges();
            return RedirectToAction("Index");
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
