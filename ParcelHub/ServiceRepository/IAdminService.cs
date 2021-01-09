using Microsoft.AspNetCore.Identity;
using ParcelHub.Models;
using System.Threading.Tasks;

namespace ParcelHub.ServiceRepository
{
    public interface IAdminService
    {
        int GetAdminSPWarehouseId();
        Task<IdentityResult> ChangePasswordForSPAdmin(ApplicationUser spAdmin,string newPassword);

        Task<IdentityResult> AddSPUserToRole(ApplicationUser user, string role);

    }
}