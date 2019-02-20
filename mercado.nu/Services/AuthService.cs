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



        public async Task<bool> AddRole(string role, string id)
        {
            if (role != null)
            {
                ApplicationRole roleName = new ApplicationRole(role);
                //ApplicationRole checkRole = await _roleManager.FindByNameAsync(role);
                var result = await _roleManager.CreateAsync(roleName);


                if (id != null)
                {
                    //ApplicationUser user = await _userManager.FindByEmailAsync(email);
                    ApplicationUser user = await _userManager.FindByIdAsync(id);

                    var addRoleToUser = await _userManager.AddToRoleAsync(user, role);
                }
            }



            //else
            //{

            //    var role = await _roleManager.FindByIdAsync(addrole.RoleInformation.Id);
            //    IdentityUser identityUser = await _userManager.FindByEmailAsync(addrole.Email);
            //    var addRoleToUser = await _userManager.AddToRoleAsync(identityUser, role.Name);

            //}

            return true;

        }




    }
}
