using Microsoft.AspNetCore.Identity;

namespace GameCo.Data.Models
{
   
    public class GameCoUser : IdentityUser<string>
    {
        public string City { get; set; }     
    }
}
