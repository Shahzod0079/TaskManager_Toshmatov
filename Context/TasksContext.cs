using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager_Toshmatov.Modell;

namespace TaskManager_Toshmatov.Context
{
    internal class TasksContext : DbContext
    {
        public DbSet<Tasks> Tasks { get; set; }

        public TasksContext()
        {
            Database.EnsureCreated();
            Tasks.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseMySql(Config.connection, Config.Version);

    }
}
