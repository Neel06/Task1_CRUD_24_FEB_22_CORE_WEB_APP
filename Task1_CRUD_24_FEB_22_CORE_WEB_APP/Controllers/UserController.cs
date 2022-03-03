using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_CRUD_24_FEB_22_CORE_WEB_APP.Interface;
using Task1_CRUD_24_FEB_22_CORE_WEB_APP.Models;

namespace Task1_CRUD_24_FEB_22_CORE_WEB_APP.Controllers
{
    public class UserController : Controller
    {
        IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        public IActionResult Index()
        {
            IEnumerable<User> user = userService.GetAllUser();
            return View(user);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            userService.AddUser(user);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        { 
            return View();
        }
        
        public IActionResult Edit(int id)
        {
            User UserById = userService.GetUserById(id);

            return View(UserById);
        }
        [HttpPost]
        public IActionResult Edit(User user)
        {
            userService.EditUser(user);
            return RedirectToAction("Index");
        }
        
    }
}
