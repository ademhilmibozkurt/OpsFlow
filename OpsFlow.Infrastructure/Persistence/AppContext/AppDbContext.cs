using Microsoft.EntityFrameworkCore;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Infrastructure.Persistence.AppContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Incident> Incidents {get; set;}
        public DbSet<IncidentTask> Tasks {get; set;}
        public DbSet<User> Users {get; set;}
        public DbSet<IncidentHistory> Histories {get; set;}
    }
}