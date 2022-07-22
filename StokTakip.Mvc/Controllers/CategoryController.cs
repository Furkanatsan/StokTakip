using AutoMapper;
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
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllAsync();
                return View(result.Data);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddAsync(categoryAddDto);
                return RedirectToAction("Index","Category");
            }
                return View();
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.HardDeleteAsync(id);

            return RedirectToAction("Index", "Category");
        }


        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var result = await _categoryService.GetAsync(Id);
           var result2 = _mapper.Map<CategoryUpdateDto>(result.Data);
            return View(result2);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.UpdateAsync(categoryUpdateDto);
            }
            return RedirectToAction("Index", "Category");
        }

    }
}
