using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebPlaneten.Models
{
    public class WebPlanetenContext : DbContext
    {


        public WebPlanetenContext(DbContextOptions options)
            : base(options)
        {
            //    Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Mond>().ToTable("wiejfewinf");
        }

        public DbSet<WebPlaneten.Models.Planet> Planet { get; set; }
        public DbSet<WebPlaneten.Models.Mond> Mond { get; set; }
    }
}
