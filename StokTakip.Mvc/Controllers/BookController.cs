﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StokTakip.Bll.Abstract;
using StokTakip.Bll.Dtos;
using StokTakip.Bll.ResultType.Enums;
using StokTakip.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StokTakip.Mvc.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, ICategoryService categoryService, IAuthorService authorService, IMapper mapper)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _authorService = authorService;
            _mapper = mapper;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var result = await _bookService.GetAllAsync();

            return View(result.Data);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var resultCategories = await _categoryService.GetAllAsync();
            var resultAuthors = await _authorService.GetAllAsync();

            return View(new BookAddViewModel
            {

                Categories = resultCategories.Data.Categories,
                Authors = resultAuthors.Data.Authors
            });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(BookAddViewModel bookAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var bookAddDto = _mapper.Map<BookAddDto>(bookAddViewModel);
                await _bookService.AddAsync(bookAddDto);
                return RedirectToAction("Index", "Book");
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.HardDeleteAsync(id);
            return RedirectToAction("Index", "book");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var resultCategories = await _categoryService.GetAllAsync();
            var resultAuthors = await _authorService.GetAllAsync();
            var result = await _bookService.GetAsync(Id);
            return View(new BookUpdateViewModel
            {
                ID = result.Data.Books.ID,
                Name = result.Data.Books.Name,
                Stock = result.Data.Books.Stock,
                Categories = resultCategories.Data.Categories,
                Authors = resultAuthors.Data.Authors
            });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(BookUpdateViewModel bookUpdateViewModel)
        {

            if (ModelState.IsValid)
            {
                var bookUpdateDto = _mapper.Map<BookUpdateDto>(bookUpdateViewModel);
                await _bookService.UpdateAsync(bookUpdateDto);
                return RedirectToAction("Index", "Book");
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var result = await _bookService.GetAllByCategoryAsync(categoryId);

            return PartialView("GetByCategory",result.Data);


        }

        //[Authorize]
        //[HttpGet]
        //public async Task<JsonResult> GetByCategory(int Id)
        //{
        //    var result = await _bookService.GetAllByCategoryAsync(Id);
        //    var categories = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
        //    {
        //        ReferenceHandler = ReferenceHandler.Preserve
        //    });
        //    return Json(categories);


        //}
    }
}
