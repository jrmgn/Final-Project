using Microsoft.AspNetCore.Identity;

namespace loginpage.Models
{
    public class Users : IdentityUser   
    {
        public string FirstName { get; set; }
    }
}
