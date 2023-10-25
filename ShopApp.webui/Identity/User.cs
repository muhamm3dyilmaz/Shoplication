using Microsoft.AspNetCore.Identity;

namespace ShopApp.webui.Identity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}