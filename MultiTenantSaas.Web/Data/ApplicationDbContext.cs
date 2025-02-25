using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiTenantSaas.Web.Services;

namespace MultiTenantSaas.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

	    private readonly string tenant;
	    public DbSet<TimeEntry> TimeEntries { get; set; } = default!;


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantService tenantService)
	        : base(options)
        {
	        tenant = tenantService.Tenant;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
	        base.OnModelCreating(modelBuilder);
	        modelBuilder
	         .Entity<TimeEntry>()
	         .HasQueryFilter(a => a.Tenant == tenant)
	         .HasData(
	          new() {Id = 1, Description = "2025 Audit", Hours =  5.2m, Tenant = "PeechtreeConsulting"},
	          new() {Id = 2, Description = "LA City Tax License Submission", Hours = 2.5m, Tenant = "PeechtreeConsulting"},
	          new() {Id = 3, Description = "Estate Planning Intake Meeting", Hours = 4.1m, Tenant = "GoldmanLaw"},
	          new() {Id = 4, Description = "Phone call to discuss estate planning", Hours = 0.5m, Tenant = "GoldmanLaw"}
	         );
        }


        public class TimeEntry
        {
	        public int Id { get; set; }
	        public string Description { get; set; } = string.Empty;
	        public decimal Hours { get; set; } 
	        public string Tenant { get; set; } = MockTenantRepository.PeechtreeConsulting;
        }

    }
}
