using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WoodPeckerAngular.Core.Models;

namespace WoodPeckerAngular.Data
{
    public class WoodPeckerAngularContext : DbContext
    {
        public WoodPeckerAngularContext (DbContextOptions<WoodPeckerAngularContext> options)
            : base(options)
        {
        }

        public DbSet<WoodPeckerAngular.Core.Models.CupModel> CupModel { get; set; }
    }
}
