using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreOdataConsole
{
    public class ObservationController : Controller
    {
        private readonly DBContext _db;
        public ObservationController(DBContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("Odata/Observation")]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_db.Observation.AsQueryable());
        }

        [HttpGet]
        [Route("Odata/Observation({id})")]
        [EnableQuery]
        public IActionResult Get(int id)
        {
            return Ok(_db.Observation.Where(s => s.Id == id).AsQueryable());
        }
    }
}
