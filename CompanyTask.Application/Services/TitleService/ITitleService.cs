using CompanyTask.Application.Models.DTOs.TitleDTOs;

namespace CompanyTask.Application.Services.TitleService
{
    public interface ITitleService
    {
        Task<bool> Create(CreateTitleDTO model, int? companyId);
        Task<bool> Update(UpdateTitleDTO model);
        Task Delete(int id);
        Task<UpdateTitleDTO> GetById(int id);
    }
}
