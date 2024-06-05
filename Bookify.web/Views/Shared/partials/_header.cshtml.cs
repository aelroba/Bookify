using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Bookify.web.Views.Shared.partials
{
    public class _header : PageModel
    {
        private readonly ILogger<_header> _logger;

        public _header(ILogger<_header> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}