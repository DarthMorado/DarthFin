using DarthFin.DB.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarthFin.Dto
{
    public class CategoryDto : BaseDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public CategoryDto? ParentCategory { get; set; }
        public int? ParentCategoryId { get; set; }
        public UserDto? User { get; set; }
        public int? UserId { get; set; }
    }
}
