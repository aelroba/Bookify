using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Bookify.web.Views.Shared.partials
{
    public class _toolbar : PageModel
    {
        private readonly ILogger<_toolbar> _logger;

        public _toolbar(ILogger<_toolbar> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}