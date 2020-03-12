using Data.DTOs;
using System.Collections.Generic;

namespace Web.Models.Claims
{
    public class UserClaimsViewModel
    {
        public List<ClaimDto> Claims { get; set; } = new List<ClaimDto>(64);
    }
}
