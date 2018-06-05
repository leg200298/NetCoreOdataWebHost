using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreOdataConsole
{
    public class HistoricallocationController : Controller
    {
        private readonly DBContext _db;
        public HistoricallocationController(DBContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("Odata/Historicallocation")]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_db.Historicallocation.AsQueryable());
        }

        [HttpGet]
        [Route("Odata/Historicallocation({id})")]
        [EnableQuery]
        public IActionResult Get(Guid id)
        {
            return Ok(_db.Historicallocation.Where(s => s.Id == id).AsQueryable());
        }
    }
}
