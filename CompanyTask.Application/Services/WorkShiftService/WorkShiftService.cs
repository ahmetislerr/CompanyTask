using AutoMapper;
using CompanyTask.Application.Models.DTOs.WorkShiftDTOs;
using CompanyTask.Application.Models.Vms.WorkShiftVMs;
using CompanyTask.Application.Services.PersonelService;
using CompanyTask.Domain.Entities;
using CompanyTask.Domain.Enums;
using CompanyTask.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CompanyTask.Application.Services.WorkShiftService
{
    public class WorkShiftService : IWorkShiftService
    {
        private readonly IMapper _mapper;
        private readonly IWorkShiftRepository _workShiftRepository;
        private readonly IPersonelService _personelService;
        private readonly IAppUserRepository _appUserRepository;

        public WorkShiftService(IWorkShiftRepository workShiftRepository, IMapper mapper, IPersonelService personelService, IAppUserRepository appUserRepository)
        {
            _workShiftRepository = workShiftRepository;
            _mapper = mapper;
            _personelService = personelService;
            _appUserRepository = appUserRepository;
        }

        public async Task<bool> Create(CreateWorkShiftDTO model, string userName)
        {
            WorkShift workShift = _mapper.Map<WorkShift>(model);
            workShift.UserId = await _personelService.GetPersonelId(userName);
            return await _workShiftRepository.Add(workShift);
        }

        public async Task Delete(int id)
        {
            WorkShift workShift = await _workShiftRepository.GetDefault(x => x.Id == id);
            if (workShift == null)
            {
                throw new Exception("No expenditure has been entered!");
            }
            else
            {
                workShift.StatuId = Status.Deleted.GetHashCode();
                workShift.DeletedDate = DateTime.Now;
                await _workShiftRepository.Delete(workShift);
            }
        }

        public async Task<UpdateWorkShiftDTO> GetById(int id)
        {
            WorkShift workShift = await _workShiftRepository.GetDefault(x => x.Id == id);
            return _mapper.Map<UpdateWorkShiftDTO>(workShift);
        }

        public async Task<List<WorkShiftVM>> GetWorkShiftForPersonel(Guid id)
        {
            var workShifs = await _workShiftRepository.GetFilteredList(
                select: x => new WorkShiftVM()
                {
                    Id = x.Id,
                    Date = x.Date.ToShortDateString(),
                    StartTime = x.StartTime.ToString("HH:mm"),
                    EndTime = x.EndTime.ToString("HH:mm"),
                    UserName = x.User.UserName + " " + x.User.LastName,
                    CompanyManagerName = x.User.Manager.FirstName + " " + x.User.Manager.LastName,
                    Statu = x.Statu.Name
                },
                where: x => x.User.Id == id && x.StatuId != Status.Deleted.GetHashCode(),
                orderby: x => x.OrderByDescending(x => x.StartTime),
                include: x => x.Include(x => x.User).Include(x => x.User.Manager).Include(x => x.Statu)
                );

            return workShifs;
        }

        public async Task<bool> Update(UpdateWorkShiftDTO model, string userName)
        {
            WorkShift workShift = _mapper.Map<WorkShift>(model);
            workShift.UserId = await _personelService.GetPersonelId(userName);
            return await _workShiftRepository.Add(workShift);
        }
    }
}
