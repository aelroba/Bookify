namespace Bookify.web.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // categories
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Category, CreateCategoryViewModel>().ReverseMap();

            // authors
            CreateMap<Author, AuthorViewModel>();
            CreateMap<AuthorFormViewModel, Author>().ReverseMap();

        }
    }
}