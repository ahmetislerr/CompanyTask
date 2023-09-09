using CompanyTask.Application.Models.DTOs.CompanyDTO;
using CompanyTask.Application.Models.DTOs.CompanyManagerDTO;
using CompanyTask.Application.Models.Vms.CompanyManagerVMs;
using CompanyTask.Application.Models.Vms.PersonelVMs;
using Microsoft.AspNetCore.Identity;

namespace CompanyTask.Application.Services.CompanyManagerService
{
    public interface ICompanyManagerService
    {
        Task<List<EmployeeVM>> GetEmployees(int companyId);
        Task<List<DepartmentVM>> GetDepartments(int? companyId);
        Task<List<TitleVM>> GetTitles(int? companyId);
        Task<CreateEmployeeVM> CreateEmployee(CreateEmployeeDTO model);
        Task<IdentityResult> UpdateEmployee(UpdateEmployeeDTO model);
        Task<List<CompanyManagerVM>> GetCompanyManagers(int? companyId);
        Task<bool> IsCompanyManager(string userName);
        Task<UpdateEmployeeDTO> GetByUserName(Guid id);
        Task Delete(Guid id);

        Task<UpdateCompanyDTO> GetCompany(int? id);
        Task<bool> UpdateCompany(UpdateCompanyDTO model);
    }
}
