using Microsoft.AspNetCore.Mvc;
using PRN221_ASM2.Repository;

namespace PRN221_ASM2.ViewComponents
{
    public class CategoryMenuViewComponent: ViewComponent
    {
        private readonly ICategory _category;
        public CategoryMenuViewComponent(ICategory categoryRepository)
        {
            _category = categoryRepository;
        }
        public IViewComponentResult Invoke()
        {
            var category = _category.GetAllCategory().OrderBy(x => x.Loai);
            return View(category);
        }
    }
}
