using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StokTakip.Bll.Abstract;
using StokTakip.Bll.ResultType.Enums;
using StokTakip.Dal.Entities;
using StokTakip.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StokTakip.Mvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly UserManager<User> _userManager;
        public HomeController(ILogger<HomeController> logger, ICategoryService categoryService, IBookService bookService, IAuthorService authorService, UserManager<User> userManager)
        {
            _logger = logger;
            _categoryService = categoryService;
            _bookService = bookService;
            _authorService = authorService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var categoryCountResult = await _categoryService.CountAsync();
            var authorCountResult = await _authorService.CountAsync();
            var bookCountResult = await _bookService.CountAsync();
            var usersCount = await _userManager.Users.CountAsync();
            if (categoryCountResult.ResultStatus == ResultStatus.Success && authorCountResult.ResultStatus == ResultStatus.Success && bookCountResult.ResultStatus == ResultStatus.Success && usersCount > -1 )
            {
                return View(new AnaSayfaViewModel
                {
                    CategoriesCount = categoryCountResult.Data,
                    AuthorsCount = authorCountResult.Data,
                    BooksCount = bookCountResult.Data,
                    UsersCount = usersCount,
                    
                });

            }
            return NotFound();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
