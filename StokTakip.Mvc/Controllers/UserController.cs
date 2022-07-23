using AutoMapper;
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
        public UserController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UserList()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto {
            Users=users,
            ResultStatus=ResultStatus.Success
            });
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
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

        public async Task<IActionResult> Delete(int Id)
        {
            var result =await _userManager.FindByIdAsync(Id.ToString());
            await _userManager.DeleteAsync(result);
            return RedirectToAction("UserList", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            return View(userUpdateDto);
        }
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
    }
}
