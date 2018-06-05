using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreOdataConsole
{
    public class DatastreamController : Controller
    {
        private readonly DBContext _db;
        public DatastreamController(DBContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("Odata/Datastream")]
        [EnableQuery]
        public IActionResult Get()
        {         
            return Ok(_db.Datastream.AsQueryable());
        }

        [HttpGet]
        [Route("Odata/Datastream({id})")]
        [EnableQuery]
        public IActionResult Get(Guid id)
        {
            return Ok(_db.Datastream.Where(s => s.Id == id).AsQueryable());
        }
        /// <summary>
        /// XU.6U4
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
