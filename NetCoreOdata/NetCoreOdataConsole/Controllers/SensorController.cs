using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreOdataConsole
{
    public class SensorController : Controller
    {
        private readonly DBContext _db;
        public SensorController(DBContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("Odata/Sensor")]
        [EnableQuery]
        public IActionResult GetSensor()
        {
            return Ok(_db.Sensor.AsQueryable());
        }
        [HttpGet]
        [Route("Odata/Sensor({id})")]
        [EnableQuery]
        public IActionResult GetSensor(int id)
        {
            return Ok(_db.Sensor.Where(s => s.Id == id).AsQueryable());
        }
    }
}
