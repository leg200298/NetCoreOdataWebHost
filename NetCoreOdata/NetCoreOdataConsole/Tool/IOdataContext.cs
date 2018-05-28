using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreOdataConsole
{
    public interface IOdataContext
    {
        IQueryable<T> GetData<T>() where T : class;
        IQueryable<T> GetData<T>(object id) where T : class;
    }
}
