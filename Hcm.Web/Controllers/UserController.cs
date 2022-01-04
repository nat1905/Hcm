using Hcm.Api.Client;
using Hcm.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hcm.Web.Controllers
{
    public class UserController : Controller
    {
        
        private readonly IUserClient _userClient;

        public UserController(
            IUserClient userClient)
        {
            
            _userClient = userClient;
        }

        // GET: UserController
        public async Task<ActionResult> Index()
        {
            var users = await _userClient.GetAllAsync();
            return View(users
                .Select(e => new UserViewModel
                {
                    UserId = e.Id,
                    Username = e.Username,
                    Password = e.Password,
                    Phone = e.Phone,                    
                    Email = e.Email,
                    Role = e.Role,
                }).ToArray());

        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [FromForm] UserViewModel userViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                await _userClient.PostAsync(new UserCreateDto
                {
                    Email = userViewModel.Email,
                    Username = userViewModel.Username,
                    Password = userViewModel.Password,                    
                    Phone = userViewModel.Phone,
                    Role = userViewModel.Role
                });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var result = await _userClient.GetAsync(id);
            

            return View(new UserViewModel
            {
                Email = result.Email,
                Username = result.Username,
                Password = result.Password,
                Phone = result.Phone,
                Role = result.Role
                .ToArray()
            });
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [FromRoute] string id,
            [FromForm] UserViewModel userViewModel)
        {
            try
            {
                var result = await _userClient.PutAsync(id, new UserUpdateDto
                {
                    Email = userViewModel.Email,
                    Username = userViewModel.Username,
                    Password = userViewModel.Password,
                    Phone = userViewModel.Phone,
                    Role = userViewModel.Role,
                });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _userClient.GetAsync(id);

            return View(new UserViewModel
            {
                Email = result.Email,
                Username = result.Username,
                Password = result.Password,
                Phone = result.Phone,
                Role = result.Role
            });
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection formCollection)
        {
            try
            {
                await _userClient.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
