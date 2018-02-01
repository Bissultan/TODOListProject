using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ElectronicTasks.Models;

namespace ElectronicTasks.App_Start
{
    class ConnectionContext : DbContext
    {
        public ConnectionContext() : base("DBConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}