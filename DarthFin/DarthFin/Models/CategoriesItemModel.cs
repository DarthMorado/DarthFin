using DarthFin.Dto;

namespace DarthFin.Models
{
    public class CategoriesItemModel
    {
        public CategoryDto Category { get; set; }
        public List<CategoriesItemModel> Children { get; set; }
    }
}
