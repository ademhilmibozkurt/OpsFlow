using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpsFlow.Domain.Entities;
using OpsFlow.Infrastructure.Identity;

namespace OpsFlow.Infrastructure.Persistence.AppContext
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
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