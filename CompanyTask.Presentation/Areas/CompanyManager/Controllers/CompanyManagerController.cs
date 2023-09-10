using CompanyTask.Application.Models.DTOs.CompanyManagerDTO;
using CompanyTask.Application.Services.AccountServices;
using CompanyTask.Application.Services.AddressService;
using CompanyTask.Application.Services.CompanyManagerService;
using CompanyTask.Application.Services.PersonelService;
using CompanyTask.Application.Services.WorkShiftService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace CompanyTask.Presentation.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager")]
    [Authorize(Roles = "CompanyManager")]
    public class CompanyManagerController : Controller
    {
        private readonly ICompanyManagerService _companyManagerService;
        private readonly IPersonelService _personelService;
        private readonly IAddressService _addressService;
        private readonly IAccountService _accountService;
        private readonly IWorkShiftService _workShiftService;


        public CompanyManagerController(ICompanyManagerService companyManagerService, IPersonelService personelService, IAddressService addressService, IAccountService accountService, IWorkShiftService workShiftService)
        {
            _companyManagerService = companyManagerService;
            _personelService = personelService;
            _addressService = addressService;
            _accountService = accountService;
            _workShiftService = workShiftService;
        }

        public async Task<IActionResult> Employees()
        {
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(await _companyManagerService.GetEmployees((int)((ViewBag.Personel).CompanyId)));
        }


        public async Task<IActionResult> Create()
        {
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
            ViewBag.Departments = new SelectList(await _companyManagerService.GetDepartments(personel.CompanyId), "Id", "Name");
            ViewBag.Titles = new SelectList(await _companyManagerService.GetTitles(personel.CompanyId), "Id", "Name");
            ViewBag.CompanyManagers = new SelectList(await _companyManagerService.GetCompanyManagers(personel.CompanyId), "Id", "FullName");
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
            ViewBag.Countries = new SelectList(await _addressService.GetCountries(), "Id", "Name");
            ViewBag.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _companyManagerService.CreateEmployee(model);
                if (!result.Errors.Any())
                {
                    if (result.Result.Succeeded)
                    {
                        return RedirectToAction("employees", "companymanager", new { Area = "companymanager" });
                    }
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item);
                }

            }
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
            ViewBag.Departments = new SelectList(await _companyManagerService.GetDepartments(personel.CompanyId), "Id", "Name");
            ViewBag.Titles = new SelectList(await _companyManagerService.GetTitles(personel.CompanyId), "Id", "Name");
            ViewBag.CompanyManagers = new SelectList(await _companyManagerService.GetCompanyManagers(personel.CompanyId), "Id", "FullName");
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
            ViewBag.Countries = new SelectList(await _addressService.GetCountries(), "Id", "Name");
            ViewBag.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();
            model.CityId = 0;
            model.CountryId = 0;
            model.DistrictId = 0;
            return View(model);
        }


        public async Task<IActionResult> Update(Guid id)
        {
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
            ViewBag.Departments = new SelectList(await _companyManagerService.GetDepartments(personel.CompanyId), "Id", "Name");
            ViewBag.Titles = new SelectList(await _companyManagerService.GetTitles(personel.CompanyId), "Id", "Name");
            ViewBag.CompanyManagers = new SelectList(await _companyManagerService.GetCompanyManagers(personel.CompanyId), "Id", "FullName");
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
            ViewBag.Countries = new SelectList(await _addressService.GetCountries(), "Id", "Name");
            var model = await _companyManagerService.GetByUserName(id);
            model.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateEmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _companyManagerService.UpdateEmployee(model);
                if (result.Succeeded)
                {
                    TempData["success"] = "Employee updated successfully";
                    return RedirectToAction("employees");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);

                }
            }
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
            ViewBag.Departments = new SelectList(await _companyManagerService.GetDepartments(personel.CompanyId), "Id", "Name");
            ViewBag.Titles = new SelectList(await _companyManagerService.GetTitles(personel.CompanyId), "Id", "Name");
            ViewBag.CompanyManagers = new SelectList(await _companyManagerService.GetCompanyManagers(personel.CompanyId), "Id", "FullName");
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
            model.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(IFormCollection collection)
        {
            Guid id = Guid.Parse(collection["id"]);
            await _companyManagerService.Delete(id);
            TempData["success"] = "Employee was deleted succesfully.";
            return RedirectToAction("employees", "companymanager", new { Area = "companymanager" });
        }

        public async Task<IActionResult> Departments()
        {
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
            return View(await _companyManagerService.GetDepartments(personel.CompanyId));
        }

        public async Task<IActionResult> Titles()
        {
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
            return View(await _companyManagerService.GetTitles(personel.CompanyId));
        }

        public async Task<IActionResult> WorkShifts()
        {
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(await _workShiftService.GetWorkShiftForPersonel(await _personelService.GetPersonelId(User.Identity.Name)));
        }

    }
}
