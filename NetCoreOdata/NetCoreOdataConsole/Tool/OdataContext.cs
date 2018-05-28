using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreOdataConsole
{
    public class OdataContext : IOdataContext
    {
        private string _connectionString;
        private readonly MysqlDBContext db;

        public OdataContext(string connectionString)
        {
            _connectionString = connectionString;
            db = new MysqlDBContext(_connectionString);
        }
        public IQueryable<T> GetData<T>() where T : class
        {
            return db.Set<T>().AsQueryable();
        }
        public IQueryable<T> GetData<T>(object id) where T : class
        {
            return db.Set<T>().Where(o => o.GetType().GetProperty("Id").GetValue(o).ToString() == id.ToString()).AsQueryable();
        }
    }
}
