using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole
                {
                    Id = "a209a3c1-93b3-44c8-aa97-933ff0a6dd24",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE",
                    ConcurrencyStamp = "b8b3c98f-249e-49bf-9187-628419c01c4e"
                },
                new IdentityRole
                {
                    Id = "92d294e3-ed18-4f51-b59a-3b30545a4780",
                    Name = "Supervisor",
                    NormalizedName = "SUPERVISOR",
                    ConcurrencyStamp = "dfbc0ceb-64af-4625-9ae7-4caf432b203a"
                },
                new IdentityRole
                {
                    Id = "9156a4db-70fe-4e43-92bd-0d7aa6c8468d",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    ConcurrencyStamp = "eec1e307-bcc4-4671-b88b-7c25b6c603a8"
                });
    }

    public DbSet<LeaveType> LeaveTypes { get; set; }
}
