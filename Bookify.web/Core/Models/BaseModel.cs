using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookify.web.Core.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set;} 
    }
}