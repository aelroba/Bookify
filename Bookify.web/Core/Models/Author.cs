using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookify.web.Core.Models
{
    public class Author : BaseModel
    {
        [MaxLength(200)]
        public string Name { get; set; } = null!;

    }
}