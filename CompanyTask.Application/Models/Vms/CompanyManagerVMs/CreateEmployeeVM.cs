using CompanyTask.Application.Models.DTOs.CompanyManagerDTO;
using Microsoft.AspNetCore.Identity;

namespace CompanyTask.Application.Models.Vms.CompanyManagerVMs
{
    public class CreateEmployeeVM
    {
        public CreateEmployeeVM()
        {
            Errors = new List<string>();
        }
        public IdentityResult Result { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public CreateEmployeeDTO Model { get; set; }
        public List<string> Errors { get; set; }
    }
}
