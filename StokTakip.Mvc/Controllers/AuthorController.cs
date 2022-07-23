using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StokTakip.Bll.Abstract;
using StokTakip.Bll.Dtos;
using StokTakip.Bll.ResultType.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakip.Mvc.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorService.GetAllAsync();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(result.Data);
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(AuthorAddDto authorAddDto)
        {
            if (ModelState.IsValid)
            {
                await _authorService.AddAsync(authorAddDto);
                return RedirectToAction("Index", "Author");
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.HardDeleteAsync(id);
            return RedirectToAction("Index", "Author");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var result = await _authorService.GetAsync(Id);
            var newResult = _mapper.Map<AuthorUpdateDto>(result.Data.Author);
            return View(newResult);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(AuthorUpdateDto authorUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _authorService.UpdateAsync(authorUpdateDto);
            }
            return RedirectToAction("Index", "author");
        }
    }
}
