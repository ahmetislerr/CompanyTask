using CompanyTask.Application.Models.Vms.PersonelVMs;

namespace CompanyTask.Application.Services.PersonelService
{
    public interface IPersonelService
    {
        Task<PersonelVM> GetPersonel(string userName);
        Task<Guid> GetPersonelId(string name);
        Task<List<CompanyEmployeesVM>> GetCompanyEmployees(int companyId, string searchString);
    }
}
