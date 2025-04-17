using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OptiFabricMVC.Domain.Model;

namespace OptiFabricMVC.Infrastructure;

public class Context: IdentityDbContext<ApplicationUser>
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Operation> Operations { get; set; }
    public DbSet<JobEmployee> JobEmployees { get; set; }
    public DbSet<OperationPattern> OperationPatterns { get; set; }
    
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    

}