using Microsoft.EntityFrameworkCore;
using TaskManager_Toshmatov.Classes.Database;
using TaskManager_Toshmatov.Models;

namespace TaskManager_Toshmatov.Context
{
    public class TasksContext : DbContext
    {
        public DbSet<Tasks> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.ConnectionConfig, mysqlOptions =>
     mysqlOptions.ServerVersion(Config.Version));
        }
    }
}