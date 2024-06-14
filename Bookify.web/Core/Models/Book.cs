using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookify.web.Core.Models
{
    [Index(nameof(Title), nameof(AuthorId), IsUnique = true)]
    public class Book : BaseModel
    {
        [MaxLength(500, ErrorMessage = Errors.MaxLength)]
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        [MaxLength(200, ErrorMessage = Errors.MaxLength)]
        public string Publisher { get; set; } = null!;
        public DateTime PublishingDate { get; set; }
        public string? ImageUrl { get; set; }
        [MaxLength(50, ErrorMessage = Errors.MaxLength)]
        public string Hall { get; set; } = null!;

        public bool IsAvaliableForRental { get; set; }
        public string Description { get; set; } = null!;

        public ICollection<BookCategory> Categories { get; set; }= new List<BookCategory>();
    }
}