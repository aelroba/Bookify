namespace Bookify.web.Core.ViewModels
{
    public class AuthorFormViewModel
    {
        [MaxLength(100, ErrorMessage = Errors.MaxLength), Display(Name = "Authors")]
        public string Name { get; set; } = null!;
    }
}