using CompanyTask.Application.Models.DTOs.DepartmentDTOs;
using CompanyTask.Application.Models.DTOs.WorkShiftDTOs;
using CompanyTask.Application.Models.Vms.WorkShiftVMs;
using CompanyTask.Domain.Entities;

namespace CompanyTask.Application.Services.WorkShiftService
{
    public interface IWorkShiftService
    {
        Task<bool> Create(CreateWorkShiftDTO model, string userName);
        Task<bool> Update(UpdateWorkShiftDTO model, string userName);
        Task Delete(int id);
        Task<UpdateWorkShiftDTO> GetById(int id);
        Task<List<WorkShiftVM>> GetWorkShiftForPersonel(Guid id);
    }
}
