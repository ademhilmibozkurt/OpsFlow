using Microsoft.EntityFrameworkCore;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Infrastructure.Persistence.AppContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);                   
        } 

        public DbSet<Incident> Incidents {get; set;}
        public DbSet<IncidentTask> Tasks {get; set;}
        public DbSet<IncidentHistory> Histories {get; set;}
    }
}