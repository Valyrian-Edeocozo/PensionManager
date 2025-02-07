using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PensionManager.PensionManger.Domain;

namespace PensionManager.PensionManager.Infrastructure
{
    public interface IApplicationDbContext
    {
        DbSet<Employer> Employers { get; }
        DbSet<PensionPlan> PensionPlans { get; }
        //DbSet<Address> Addresses { get; }
        DbSet<Contribution> Contributions { get; }
    }
}