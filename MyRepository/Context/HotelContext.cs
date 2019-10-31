using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyRepository.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MyRepository.Context
{

    /// <summary>
    /// Context for Database
    /// </summary>
    public class HotelContext : DbContext
    {

        public IDbSet<Apartment> Apartments { get; set; }
        public IDbSet<Application> Applications { set; get; }
        public IDbSet<BaseApplication> BaseApplications { set; get; }
        public IDbSet<ReservedDates> ReservedDateses { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Apartment>()
                .HasMany(c => c.Applications);
            modelBuilder.Entity<Application>().HasRequired(c => c.ForDates);

        }
    }
}