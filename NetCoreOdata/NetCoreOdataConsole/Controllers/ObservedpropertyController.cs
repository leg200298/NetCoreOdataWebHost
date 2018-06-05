using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreOdataConsole
{
    public class ObservedpropertyController : Controller
    {
        private readonly DBContext _db;
        public ObservedpropertyController(DBContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("Odata/Observedproperty")]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_db.Observedproperty.AsQueryable());
        }

        [HttpGet]
        [Route("Odata/Observedproperty({id})")]
        [EnableQuery]
        public IActionResult Get(Guid id)
        {
            return Ok(_db.Observedproperty.Where(s => s.Id == id).AsQueryable());
        }
    }
}
