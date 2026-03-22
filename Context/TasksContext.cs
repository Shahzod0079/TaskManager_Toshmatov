using Microsoft.EntityFrameworkCore;
using TaskManager_Toshmatov.Classes.Database;
using TaskManager_Toshmatov.Models;

namespace TaskManager_Toshmatov.Context
{
    public class TasksContext : DbContext
    {
        public DbSet<Tasks> Tasks { get; set; }

        public DbSet<Priority> Priorities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.ConnectionConfig, mysqlOptions =>
     mysqlOptions.ServerVersion(Config.Version));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Настроим свзяи
            modelBuilder.Entity<Tasks>()
                 .HasOne(t => t.Priority)
                 .WithMany(p => p.Tasks)
                 .HasForeignKey(t => t.PriorityId);

            //Добавляем начальные данные приоритетов
            modelBuilder.Entity<Priority>().HasData(
                new Priority { Id = 1, Name = "Низкий", Level = 1 },
                new Priority { Id = 2, Name = "Седний", Level = 2 },
                new Priority { Id = 3, Name = "Высокий", Level = 3 },
                new Priority { Id = 4, Name = "Критический", Level = 4 }
                );
        }
    }
}


