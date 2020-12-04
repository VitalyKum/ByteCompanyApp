using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ByteCompany.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ByteCompany.ViewModel;

namespace ByteCompany.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _rolesManager;
        private readonly UserManager<User> _usersManager;
        public RolesController(UserManager<User> usersManager, RoleManager<IdentityRole> roleManager)
        {
            _usersManager = usersManager;
            _rolesManager = roleManager;
        }
        public async Task<IActionResult> Index()
        { 
            return View(await _rolesManager.Roles.OrderBy(r => r.Name).ToListAsync());
        }

        public IActionResult Create() => View();

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    await _rolesManager.CreateAsync(new IdentityRole(name));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("CreateRole Error", "Unable to save changes. " +
                                                 "Try again, and if the problem persists " +
                                                 "see your system administrator.");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await _rolesManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();
            try
            {
                await _rolesManager.DeleteAsync(role);
            }
            catch (DbUpdateException)
            { 
                
                    ModelState.AddModelError("DeleteRole Error", "Unable to save changes. " +
                                                 "Try again, and if the problem persists " +
                                                 "see your system administrator.");
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UserList(string searchString)
        {
            var users = _usersManager.Users.ToList();

            if (!String.IsNullOrEmpty(searchString))
                users = users.Where(u => u.Email.Contains(searchString)).ToList();
                       
            return View(users);            
        }

        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользователя
            User user = await _usersManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _usersManager.GetRolesAsync(user);
                var allRoles = _rolesManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // получаем пользователя
            User user = await _usersManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _usersManager.GetRolesAsync(user);
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _usersManager.AddToRolesAsync(user, addedRoles);

                await _usersManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }

        [HttpPost, ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            User user = await _usersManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();
            try
            {
                await _usersManager.DeleteAsync(user);
            }
            catch
            {
                ModelState.AddModelError("DeleteRole Error", "Unable to save changes. " +
                                                 "Try again, and if the problem persists " +
                                                 "see your system administrator.");
            }
            return RedirectToAction("UserList");
        }
    }
}