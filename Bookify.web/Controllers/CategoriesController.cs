namespace Bookify.web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(ILogger<CategoriesController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context; 
            _mapper = mapper; 
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.AsNoTracking().ToList();
            var viewModel = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public IActionResult CreateAjax(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var category = _mapper.Map<Category>(model);
            _context.Add(category);
            _context.SaveChanges();

            var viewModel = _mapper.Map<CategoryViewModel>(category);

            return PartialView("Modal/_CategoryRow", viewModel); 
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

            _context.Add(_mapper.Map<Category>(model));
            _context.SaveChanges();

            return RedirectToAction(nameof(Index)); 
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (model == null) return NotFound();
            var viewModel = _mapper.Map<CreateCategoryViewModel>(model);
            return View("Update", viewModel); 
        }

        [HttpPost]
        public IActionResult Edit(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var category = _context.Categories.FirstOrDefault(c => c.Id == model.Id);
            if (category == null) return NotFound();
            category = _mapper.Map(model, category);
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
            var viewModel = _mapper.Map<CreateCategoryViewModel>(model);

            return PartialView("Modal/_EditFormData", viewModel); 
        }

        [HttpPost]
        [AjaxOnly]
        public IActionResult UpdateAjax(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var category = _context.Categories.FirstOrDefault(c => c.Id == model.Id);
            if (category == null) return NotFound();
            category = _mapper.Map(model, category);
            category.UpdatedOn = DateTime.Now;
            _context.SaveChanges();
            
            var viewModel = _mapper.Map<CategoryViewModel>(category);
            return PartialView("Modal/_CategoryRow", viewModel); 
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

        public IActionResult CheckAvailability(CreateCategoryViewModel model)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Name == model.Name);
            var isAllowed = category is null || category.Id.Equals(model.Id);
            return Json(isAllowed);
        }
    }
}