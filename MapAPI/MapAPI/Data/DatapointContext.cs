using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MapAPI.Data
{
    public class DatapointContext : DbContext
    {
        public DatapointContext(DbContextOptions<DatapointContext> options) : base(options)
        {
        }


    }
}
