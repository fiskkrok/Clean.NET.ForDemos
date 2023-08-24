using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PROJECT_DIRECTORY.Areas.Identity.Data;

public class PROJECT_DIRECTORYIdentityDbContext : IdentityDbContext<IdentityUser>
{
    public PROJECT_DIRECTORYIdentityDbContext(DbContextOptions<PROJECT_DIRECTORYIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Added to change the default schema from dbo to Authentication
        builder.HasDefaultSchema("Authentication"); // <--- Add this line
    }
}
