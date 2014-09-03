using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hsr.Models
{
    public class HsrContext : DbContext
    {
        
        public DbSet<Hsr.Models.Test> Test { get; set; }
    }
}