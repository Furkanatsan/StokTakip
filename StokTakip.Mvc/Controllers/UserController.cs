using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokTakip.Bll.Dtos;
using StokTakip.Bll.ResultType.Enums;
using StokTakip.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakip.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        public UserController(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View("UserLogin");//action adı ile view uyuşmadığından dolayı belirttik.
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false);//4. parametre lockout değeri fazla deneme varsa kitler
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        ModelState.AddModelError("", "E-posta adresiniz veya şifreniz yanlış");
                        return View("UserLogin");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "E-posta adresiniz veya şifreniz yanlış");
                    return View("UserLogin");
                }
            }
            else
            {
                return View("UserLogin");
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "User");
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UserList()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto {
            Users=users,
            ResultStatus=ResultStatus.Success
            });
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(userAddDto);
                await _userManager.CreateAsync(user, userAddDto.Password);//kullanıcının şifresini hashleyip veri tabanına ekler.
                
                return RedirectToAction("UserList","User");
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result =await _userManager.FindByIdAsync(Id.ToString());
            await _userManager.DeleteAsync(result);
            return RedirectToAction("UserList", "User");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            return View(userUpdateDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var oldUser = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());
                var updatedUser=_mapper.Map<UserUpdateDto,User>(userUpdateDto,oldUser);
                await _userManager.UpdateAsync(updatedUser);
                return RedirectToAction("UserList", "User");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Accessdenied()
        {
            return View();
        }


    }
}
