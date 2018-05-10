using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreOdataConsole
{
    public class LocationController : Controller
    {
        private readonly DBContext _db;
        public LocationController(DBContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("Odata/Location")]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_db.Location.AsQueryable());
        }

        [HttpGet]
        [Route("Odata/Location({id})")]
        [EnableQuery]
        public IActionResult Get(int id)
        {
            return Ok(_db.Location.Where(s => s.Id == id).AsQueryable());
        }
    }
}
