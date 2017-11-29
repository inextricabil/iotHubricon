using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iotHubricon.Models;

namespace iotHubricon.Controllers
{
    public class SensorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sensors
        public ActionResult Index()
        {
            var records = db.SensorRecords.ToList();

            var model = new SensorViewModel
            {
                Sensors = db.Sensors.ToList()
            };

            foreach (var sensor in model.Sensors)
            {
                try
                {
                    var sensorRecords = records.Where(s => s.SensorId == sensor.SensorId).ToList();

                    var myTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. Europe Standard Time");
                    var currentDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, myTimeZone);
                    var lastMonthSensorRecords = sensorRecords.Where(s => s.Date > currentDateTime.AddMonths(-12)).ToList();

                    var sensorHumidityAverage = lastMonthSensorRecords.Average(s => s.Humidity);
                    var sensorTemperatureAverage = lastMonthSensorRecords.Average(s => s.Temperature);

                    model.HumidityAverage.Add(sensor.SensorId.ToString(), sensorHumidityAverage);
                    model.TemperatureAverage.Add(sensor.SensorId.ToString(), sensorTemperatureAverage);

                    var lastFiveRecords = sensorRecords.OrderByDescending(p => p.Date).Take(5).ToList();

                    var sensorDateTemperatureValuesList = new List<DateValueRecord>();
                    var sensorDateHumidityValuesList = new List<DateValueRecord>();

                    foreach (var record in lastFiveRecords)
                    {
                        sensorDateTemperatureValuesList.Add(new DateValueRecord(record.Date, record.Temperature));
                        sensorDateHumidityValuesList.Add(new DateValueRecord(record.Date, record.Humidity));
                    }

                    model.LastHumidityRecords.Add(sensor.SensorId.ToString(), sensorDateHumidityValuesList);
                    model.LastTemperatureRecords.Add(sensor.SensorId.ToString(), sensorDateTemperatureValuesList);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }

            }
            return View(model);
        }

        // GET: Sensors/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sensor sensor = db.Sensors.Find(id);
            if (sensor == null)
            {
                return HttpNotFound();
            }
            return View(sensor);
        }

        // GET: Sensors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sensors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SensorId,Name,Latitude,Longitude,Location")] Sensor sensor)
        {
            if (sensor.SensorId == Guid.Empty)
            {
                sensor.SensorId = Guid.NewGuid();
            }
            if (ModelState.IsValid)
            {
                db.Sensors.Add(sensor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sensor);
        }

        // GET: Sensors/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sensor sensor = db.Sensors.Find(id);
            if (sensor == null)
            {
                return HttpNotFound();
            }
            return View(sensor);
        }

        // POST: Sensors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SensorId,Name,Latitude,Longitude,Location")] Sensor sensor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sensor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sensor);
        }

        // GET: Sensors/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sensor sensor = db.Sensors.Find(id);
            if (sensor == null)
            {
                return HttpNotFound();
            }
            return View(sensor);
        }

        // POST: Sensors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Sensor sensor = db.Sensors.Find(id);
            db.Sensors.Remove(sensor);
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
