using CompanyTask.Application.Models.DTOs.AccountDTOs;
using CompanyTask.Application.Models.Vms.PersonelVMs;
using Microsoft.AspNetCore.Identity;

namespace CompanyTask.Application.Services.AccountServices
{
    public interface IAccountService
    {
        Task<RegisterVM> Register(RegisterDTO model);
        Task<SignInResult> Login(LoginDTO model);
        Task<UpdateProfileDTO> GetByUserName(string userName);
        Task UpdateUser(UpdateProfileDTO model);
        Task LogOut();
        Task<bool> IsCompanyManager(string userName);
    }
}
