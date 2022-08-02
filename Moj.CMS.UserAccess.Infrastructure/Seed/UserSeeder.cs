using Microsoft.AspNetCore.Identity;
using Moj.CMS.Application.Models;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Shared.Constants.Permission;
using Moj.CMS.Shared.Constants.Role;
using Moj.CMS.Shared.Constants.User;
using Moj.CMS.Shared.Infrastructure.Seed;
using Moj.CMS.UserAccess.Application.Extensions;
using Moj.CMS.UserAccess.Application.Models;
using Moj.CMS.UserAccess.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moj.CMS.UserAccess.Infrastructure.Seed
{
    public class UserSeeder : IDatabaseSeeder
    {
        private readonly IPermissionsProvider _permissionsProvider;
        private readonly UserAccessDbContext _db;
        private readonly CMSDbContext _CMSDbContext;
        private readonly UserManager<CMSUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserSeeder(UserManager<CMSUser> userManager, RoleManager<IdentityRole> roleManager, UserAccessDbContext db,
            IPermissionsProvider permissionsProvider, CMSDbContext cMSDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _permissionsProvider = permissionsProvider;
            _CMSDbContext = cMSDbContext;
        }

        private IEnumerable<CMSUser> _users = new List<CMSUser>
        {
            new CMSUser
            {
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@cms.com",
                UserName = "admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                CreationTime = DateTime.Now,
                IsActive = true,
            },
            new CMSUser
            {
                Id= UserConstants.SystemUserId,
                UserName = "system",
                Email = "system@cms.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                CreationTime = DateTime.Now,
                IsActive = true,
            }
        };

        public async Task SeedAsync()
        {
            foreach (var user in _users)
                await AddUserAsync(user);

            await _db.SaveChangesAsync();
            await SyncUsersAsync();
        }

        private async Task AddUserAsync(CMSUser _user)
        {
            //Check if Role Exists
            var adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
            if (adminRoleInDb == null)
            {
                adminRoleInDb = new IdentityRole(RoleConstants.AdministratorRole);
                await _roleManager.CreateAsync(adminRoleInDb);
            }
            var userInDb = await _userManager.FindByNameAsync(_user.UserName);
            if (userInDb == null)
            {
                await _userManager.CreateAsync(_user, UserConstants.DefaultPassword);
                await _userManager.AddToRoleAsync(_user, RoleConstants.AdministratorRole);
            }
            await _roleManager.GeneratePermissionClaims(adminRoleInDb, _permissionsProvider);
        }

        private async Task SyncUsersAsync()
        {
            var exists = _db.Users;
            var cmsExists = _CMSDbContext.Users;
            foreach (var existsUser in exists)
            {
                var cmsUser = await cmsExists.FindAsync(existsUser.Id);
                if (cmsUser == null)
                    await _CMSDbContext.Users.AddAsync(new User
                    {
                        Id = existsUser.Id,
                        Name = existsUser.UserName,
                        Email = existsUser.Email
                    });
                else if (cmsUser.LastUpdate != existsUser.LastModificationTime)
                {
                    cmsUser.Name = existsUser.UserName;
                    cmsUser.Email = existsUser.Email;
                    cmsUser.LastUpdate = existsUser.LastModificationTime;
                    _CMSDbContext.Users.Update(cmsUser);
                }
            }
            await _CMSDbContext.SaveChangesAsync();
        }
    }
}