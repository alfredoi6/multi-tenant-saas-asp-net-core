using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantSaas.Web.Data;
using MultiTenantSaas.Web.Middleware;
using MultiTenantSaas.Web.Services;

namespace MultiTenantSaas.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ITenantService, TenantService>();


            builder.Services.AddScoped<MultiTenantServiceMiddleware>();

            var app = builder.Build();

            // initialize the database
            using (var scope = app.Services.CreateScope()) {
	            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
	            db.Database.Migrate();    
            }
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // middleware that reads and sets the tenant
            app.UseMiddleware<MultiTenantServiceMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            
            // multi-tenant request, try adding ?tenant=Khalid or ?tenant=Internet (default)
            app.MapGet("/TimeEntries", async (ApplicationDbContext db) => await db
	            .TimeEntries
	            // hide the tenant, which is response noise
	            .Select(x => new {x.Id, x.Hours, x.Description})
	            .ToListAsync());
            
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
