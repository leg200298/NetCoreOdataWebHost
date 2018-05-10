using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreOdataConsole
{
    public class FeatureofinterestController : Controller
    {
        private readonly DBContext _db;
        public FeatureofinterestController(DBContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("Odata/Featureofinterest")]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_db.Featureofinterest.AsQueryable());
        }

        [HttpGet]
        [Route("Odata/Featureofinterest({id})")]
        [EnableQuery]
        public IActionResult Get(int id)
        {
            return Ok(_db.Featureofinterest.Where(s => s.Id == id).AsQueryable());
        }
    }
}
