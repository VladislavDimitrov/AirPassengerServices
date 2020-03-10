using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Data.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Claim> Claims { get; set; }
    }
}
