using CompanyTask.Application.Models.DTOs.AccountDTOs;
using CompanyTask.Application.Services.AccountServices;
using CompanyTask.Application.Services.AddressService;
using CompanyTask.Application.Services.CompanyManagerService;
using CompanyTask.Application.Services.PersonelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyTask.Presentation.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountServices;
        private readonly IPersonelService _personelService;
        private readonly IAddressService _addressService;
        private readonly ICompanyManagerService _companyManagerService;

        public AccountController(IAccountService accountServices, IPersonelService personelService, IAddressService addressService, ICompanyManagerService companyManagerService)
        {
            _accountServices = accountServices;
            _personelService = personelService;
            _addressService = addressService;
            _companyManagerService = companyManagerService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("login", "account");
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
            ViewBag.Countries = new SelectList(await _addressService.GetCountries(), "Id", "Name");
            ViewBag.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();
            return View();
        }

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountServices.Register(model);
                if (result.Result.Succeeded)
                {
                    return RedirectToAction("login", "account");
                }
            }
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
            ViewBag.Countries = new SelectList(await _addressService.GetCountries(), "Id", "Name");
            ViewBag.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/")
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("index", "personel", new { Area = "personel" });

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountServices.Login(model);

                if (result.Succeeded)
                {
                    if (await _accountServices.IsCompanyManager(model.UserName))
                        return RedirectToAction("index", "companymanager", new { Area = "CompanyManager" });
                    return RedirectToLocal(returnUrl);
                }

                if (result == Microsoft.AspNetCore.Identity.SignInResult.Failed)
                    TempData["loginError"] = "Username, Email or Password is wrong.";
                else
                    TempData["loginError"] = "Invalid Login Attemp";

            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await _accountServices.LogOut();
            return RedirectToAction("login");
        }

        private IActionResult RedirectToLocal(string returnUrl = "/")
        {

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("index", "");
            }
        }

        [HttpGet, AllowAnonymous]
        public async Task<JsonResult> setDropDownListCity(int id)
        {
            var cities = await _addressService.GetCities(id);
            return Json(cities);
        }

        [HttpGet, AllowAnonymous]
        public async Task<JsonResult> setDropDownListDistrict(int id)
        {
            var districts = await _addressService.GetDistricts(id);
            return Json(districts);
        }
    }
}
