using ActivitiesManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ActivitiesManager.DAL
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext() : base("ApplicationDBContext") { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
    }
}