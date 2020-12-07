using Microsoft.AspNetCore.Identity;

namespace WorkoutKeeper.Models
{
    public class User:IdentityUser
    {
        public int Year { get; set; }
    }
}
