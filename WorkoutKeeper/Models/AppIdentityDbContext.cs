using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WorkoutKeeper.Models
{
    public class AppIdentityDbContext: IdentityDbContext<User>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) :base(options) 
        {
            Database.EnsureCreated();
        }
    }
}
