using Microsoft.AspNetCore.Identity;

namespace GameCo.Data.Models
{
    // Add profile data for application users by adding properties to the GameCoUser class
    public class GameCoUser : IdentityUser<string>
    {
        public string City { get; set; }     
    }
}
