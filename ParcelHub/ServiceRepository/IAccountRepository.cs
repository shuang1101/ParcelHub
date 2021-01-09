using Microsoft.AspNetCore.Identity;
using ParcelHub.Models;
using System.Threading.Tasks;

namespace ParcelHub.ServiceRepository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUser signUpUser);
        Task<SignInResult> PasswordSignInAsync(LoginUser loginUser);
        Task<IdentityResult> ConfirmVarification(string uid, string token);
        Task SignOutAsync();
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordUserModel model);


    }
}