using dotnet_vsa_template.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace dotnet_vsa_template.Infrastructure.DbContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
{
  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    base.OnModelCreating(builder);
  }
  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
  {
    foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
        .Where(q => q.State is EntityState.Added || q.State is EntityState.Modified))
    {
      entry.Entity.UpdatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

      if (entry.State is EntityState.Added)
        entry.Entity.CreatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
    }

    return base.SaveChangesAsync(cancellationToken);
  }
}