namespace Bookify.web.Core.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category : BaseModel
    {
       
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        public ICollection<BookCategory> Books { get; set; } = new List<BookCategory>();

    }
}