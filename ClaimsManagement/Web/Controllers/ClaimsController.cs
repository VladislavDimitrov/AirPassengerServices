using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Claims;

namespace Web.Controllers
{
    [Authorize(Roles = "Member")]
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
                if (claimDto.FlightDate.Year < 2000)
                {
                    ModelState.AddModelError(string.Empty, $"Please enter a valid FLight Date. Air Passenger Services does not handle claims regarding flights before the 2000-th year.");

                    return View(claimDto);
                }

                if (claimDto.Category != "Voluntary Cancellation" && claimDto.FlightDate > DateTime.UtcNow)
                {
                    ModelState.AddModelError(string.Empty, $"Only claims in the category \"Voluntary Cancellation\" can be submitted with Flight Date that is in the future.");

                    return View(claimDto);
                }

                claimDto.CountryCode = claimDto.CountryCode.ToUpper();
                if (!phoneNumberServices.IsValidNumber(claimDto.PhoneNumber, claimDto.CountryCode))
                {
                    ModelState.AddModelError(string.Empty, $"Invalid phone number for {claimDto.CountryCode}.");

                    return View(claimDto);
                }

                claimDto.User = await userManager.GetUserAsync(HttpContext.User);
                await claimServices.CreateAsync(claimDto);
                ViewData["Message"] = "Success";

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
            if (claimDto.FlightDate.Year < 2000)
            {
                ModelState.AddModelError(string.Empty, $"Please enter a valid FLight Date. Air Passenger Services does not handle claims regarding flights before the 2000-th year.");

                return View(claimDto);
            }

            claimDto.User = await userManager.GetUserAsync(HttpContext.User);
            if (claimDto.Category != "Voluntary Cancellation" && claimDto.FlightDate > DateTime.UtcNow)
            {
                ModelState.AddModelError(string.Empty, $"Only claims in the category \"Voluntary Cancellation\" can be submitted with Flight Date that is in the future.");

                return View("Details", claimDto);
            }

            claimDto.CountryCode = claimDto.CountryCode.ToUpper();
            if (!phoneNumberServices.IsValidNumber(claimDto.PhoneNumber, claimDto.CountryCode))
            {
                ModelState.AddModelError(string.Empty, $"Invalid phone number for {claimDto.CountryCode}. You claim was not updated.");

                return View("Details", claimDto);
            }

            var updatedClaim = await claimServices.UpdateAsync(claimDto);
            ViewData["Message"] = "Success";

            return View("Details", updatedClaim);
        }

        public async Task<IActionResult> UserClaims()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var claims = await claimServices.GetClaimsByUserAsync(user);
            if (claims.Count() == 0)
                return View();

            var vm = new UserClaimsViewModel();
            vm.Claims = claims;

            return View(vm);
        }
    }
}