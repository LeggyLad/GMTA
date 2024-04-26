
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GymMGT.Models
{
    // DbContext class for interacting with the database
    public class GymDbContext : DbContext
    {
        // Constructor to initialize DbContext options
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
            //options.UseSqlServer(@"Data Source=PREDATOR;Initial Catalog=GymDb;Integrated Security=True;Trust Server Certificate=True";
        }

        // Method to configure DbContext options
        protected override void OnConfiguring(DbContextOptionsBuilder options) =>

            options.UseSqlServer(@"Data Source=PREDATOR;Initial Catalog=GymDb;Integrated Security=True;Trust Server Certificate=True");

        public DbSet<GymTrainee> Trainees { get; set; }
        public DbSet<BloodGroup> BloodGroups { get; set; }
        public DbSet<TrainingLevel> TrainingLevels { get; set; }
        public DbSet<MonthlyFeeVoucher> MonthlyFeeVouchers { get; set; }
        public DbSet<GymTrainee_result> GymTrainee_result { get; set; }
    }
}
