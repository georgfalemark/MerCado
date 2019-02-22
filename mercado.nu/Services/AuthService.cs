using mercado.nu.Models;
using mercado.nu.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Services
{
    [Authorize]
    public class AuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<bool> AddRole(string role)
        {
            ApplicationRole roleName = new ApplicationRole(role);
            ApplicationRole checkIfRoleExist = await GetRoles(role);

            if (checkIfRoleExist == null)
            {
                var result = await _roleManager.CreateAsync(roleName);
            }
            return true;
        }

        public async Task<IdentityResult> AddRoleToUser(string role, string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            return await _userManager.AddToRoleAsync(user, role);
        }

        private async Task<ApplicationRole> GetRoles(string role)
        {
            return await _roleManager.FindByNameAsync(role);
        }

    }
}
