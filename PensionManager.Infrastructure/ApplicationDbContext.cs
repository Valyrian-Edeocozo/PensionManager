using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PensionManager.PensionManger.Domain;

namespace PensionManager.PensionManager.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options), IApplicationDbContext
{

    // DbSets for custom entities
    public DbSet<Employer> Employers => this.Set<Employer>();
    public DbSet<PensionPlan> PensionPlans => this.Set<PensionPlan>();
    //public DbSet<Address> Addresses => this.Set<Address>();
    public DbSet<Contribution> Contributions => this.Set<Contribution>();

}