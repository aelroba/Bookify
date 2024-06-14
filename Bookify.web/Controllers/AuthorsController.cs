using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bookify.web.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AuthorsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var allData = _context.Authors.AsNoTracking().ToList();
            var list = _mapper.Map<IEnumerable<AuthorViewModel>>(allData);
            return View(list);
        }
        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AuthorFormViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var allData = _context.Authors.FirstOrDefault(d => d.Name == model.Name);
            if (allData == null)
            {
                var author = _mapper.Map<Author>(model);
                _context.Add(author);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int Id)
        {
            var author = _context.Authors.FirstOrDefault(d => d.Id == Id);
            if (author == null)
                return BadRequest();
            else
            return Ok(author);

        }

        [HttpPut]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int Id, AuthorFormViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var author = _context.Authors.FirstOrDefault(d => d.Id == Id);
            if (author == null) return NotFound();
            author.Name = model.Name;
            author.UpdatedOn = DateTime.Now;
            _context.SaveChanges();
            return Ok(author);
        }

        [HttpDelete]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public IActionResult Destroy([FromRoute] int Id)
        {
            var author = _context.Authors.FirstOrDefault(d => d.Id == Id);
            if (author == null)
                return BadRequest();
            
            _context.Remove(author);
            _context.SaveChanges();
            return Ok(author);

        }

        
    }
}