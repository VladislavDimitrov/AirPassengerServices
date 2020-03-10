using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System.Threading.Tasks;
using Web.Models.Claims;

namespace Web.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly IClaimServices claimServices;
        private readonly IPhoneNumberServices phoneNumberServices;
        private readonly UserManager<User> userManager;

        public ClaimsController(IClaimServices claimServices, IPhoneNumberServices phoneNumberServices, UserManager<User> userManager)
        {
            this.claimServices = claimServices;
            this.phoneNumberServices = phoneNumberServices;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClaimDto claimDto)
        {
            if (ModelState.IsValid)
            {
                claimDto.CountryCode = claimDto.CountryCode.ToUpper();
                if (!phoneNumberServices.IsValidNumber(claimDto.PhoneNumber, claimDto.CountryCode))
                {
                    ModelState.AddModelError(string.Empty, $"Invalid phone number for {claimDto.CountryCode}.");

                    return View(claimDto);
                }
                
                claimDto.User = await userManager.GetUserAsync(HttpContext.User);
                await claimServices.CreateAsync(claimDto);

                return View();
            }

            return View(claimDto);
        }

        public async Task<IActionResult> Details(int id)
        {
            var claimDto = await claimServices.GetAsync(id);

            return View(claimDto);
        }

        public async Task<IActionResult> Update(ClaimDto claimDto)
        {
            claimDto.User = await userManager.GetUserAsync(HttpContext.User);
            await claimServices.UpdateAsync(claimDto);

            return View("Details", claimDto);
        }

        [HttpGet]
        public async Task<ActionResult> Reporting()
        {
            var vm = new ReportViewModel();
            vm.Claims = await claimServices.Get20LatestClaimsAsync();

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Filter(ReportViewModel vm)
        {
            var claimsResult = await claimServices.FilterByMultipleCriteriaAsync(vm.Airline, vm.FlightNumber, vm.From, vm.To);

            if (claimsResult.Count == 0)
            {
                ModelState.AddModelError(string.Empty, $"No results to display.");

                return View();
            }

            vm.Claims = claimsResult;

            return View(vm);
        }
    }
}