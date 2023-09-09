using CompanyTask.Application.Models.DTOs.DepartmentDTOs;

namespace CompanyTask.Application.Services.DepartmentService
{
    public interface IDepartmentService
    {
        Task<bool> Create(CreateDepartmentDTO model, int? companyId);
        Task<bool> Update(UpdateDepartmentDTO model);
        Task Delete(int id);
        Task<UpdateDepartmentDTO> GetById(int id);
    }
}
