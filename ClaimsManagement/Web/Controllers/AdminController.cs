using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Admin;
using Web.Models.Claims;

namespace Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IClaimServices claimServices;
        private readonly UserManager<User> userManager;

        public AdminController(IClaimServices claimServices, UserManager<User> userManager)
        {
            this.claimServices = claimServices;
            this.userManager = userManager;
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
            if (vm.Airline == null && vm.FlightNumber == default && vm.From == default && vm.To == default)
            {
                ModelState.AddModelError(string.Empty, $"No results to display.");

                return View("Reporting");
            }

            var claimsResult = await claimServices.FilterByMultipleCriteriaAsync(vm.Airline, vm.FlightNumber, vm.From, vm.To);

            if (claimsResult.Count == 0)
            {
                ModelState.AddModelError(string.Empty, $"No results to display.");

                return View("Reporting");
            }

            vm.Claims = claimsResult;

            return View("Reporting", vm);
        }

        [HttpGet]
        public async Task<IActionResult> ManageUsers(ManageUsersViewModel manageUsers)
        {

            if (string.IsNullOrEmpty(manageUsers.Input))
                return View();

            ManageUsersViewModel manageUserVm;
            var users = await userManager.Users.Where(u => u.UserName.Contains(manageUsers.Input)).ToListAsync();
            if (users.Count() == 0)
            {
                ModelState.AddModelError(string.Empty, "No user found with this name.");
                manageUserVm = new ManageUsersViewModel();

                return View(manageUsers);
            }

            var usersVm = new List<UserViewModel>(16);
            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                var userRole = roles.OrderBy(r => r).First();
                usersVm = new List<UserViewModel>(16);
                usersVm.Add(new UserViewModel
                {
                    Id = user.Id,
                    Name = user.UserName,
                    Role = userRole,
                });
            }

            manageUserVm = new ManageUsersViewModel(usersVm);
            return View(manageUserVm);
        }
       
        public async Task Promote(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            await userManager.AddToRoleAsync(user, "Administrator");
        }

        public async Task Demote(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            await userManager.RemoveFromRoleAsync(user, "Administrator");
        }
    }
}