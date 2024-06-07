using Microsoft.AspNetCore.Mvc;

namespace Bookify.web.Core.ViewModels
{
    public class CreateCategoryViewModel
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [Remote("CheckAvailability", null, AdditionalFields = "Id", ErrorMessage = "Sorry, This category is already exists!")]
        public string Name { get; set; } = null!;
    }
}