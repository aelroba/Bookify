using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bookify.web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bookify.web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ApplicationDbContext _context;

        public CategoriesController(ILogger<CategoriesController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; 
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.AsNoTracking().ToList();
            return View(categories);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public IActionResult CreateAjax(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var category = new Category { Name = model.Name};
            _context.Add(category);
            _context.SaveChanges();

            return PartialView("Modal/_CategoryRow", category); 
        }

        [HttpGet]
        [AjaxOnly]
        public IActionResult CreateAjax()
        {
            return PartialView("Modal/_FormData"); 
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _context.Add(new Category { Name = model.Name});
            _context.SaveChanges();

            return RedirectToAction(nameof(Index)); 
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (model == null) return NotFound();
            var viewModel = new CreateCategoryViewModel {
                Id = id,
                Name = model.Name,
            };
            return View("Update", viewModel); 
        }

        [HttpPost]
        public IActionResult Edit(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var category = _context.Categories.FirstOrDefault(c => c.Id == model.Id);
            if (category == null) return NotFound();
            category.Name = model.Name;
            category.UpdatedOn = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index)); 
        }

        [HttpGet]
        [AjaxOnly]
        public IActionResult UpdateAjax(int id)
        {
            var model = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (model == null) return NotFound();
            var viewModel = new CreateCategoryViewModel {
                Id = id,
                Name = model.Name,
            };
            return PartialView("Modal/_EditFormData", viewModel); 
        }

        [HttpPost]
        [AjaxOnly]
        public IActionResult UpdateAjax(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var category = _context.Categories.FirstOrDefault(c => c.Id == model.Id);
            if (category == null) return NotFound();
            category.Name = model.Name;
            category.UpdatedOn = DateTime.Now;
            _context.SaveChanges();
            return PartialView("Modal/_CategoryRow", category); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            category.IsDeleted = !category.IsDeleted;
            category.UpdatedOn = DateTime.Now;
            _context.SaveChanges();
            return Ok(new {IsDeleted = category.IsDeleted, UpdatedOn = category.UpdatedOn.ToString()}); 
        }
    }
}