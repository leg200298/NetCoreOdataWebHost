using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreOdataConsole
{
    public class ThingController : Controller
    {
        private readonly DBContext _db;
        public ThingController(DBContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("Odata/Thing")]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_db.Thing.AsQueryable());
        }

        [HttpGet]
        [Route("Odata/Thing({id})")]
        [EnableQuery]
        public IActionResult Get(int id)
        {
            return Ok(_db.Thing.Where(s => s.Id == id).AsQueryable());
        }
    }
}
