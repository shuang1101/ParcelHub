using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{
    public class ApplicationUser:IdentityUser
    {
        public int SPWarehouseModelIdIfUserIsAdmin { get; set; } = -1;

        public int AgentCodeId { get; set; }

    }
}
