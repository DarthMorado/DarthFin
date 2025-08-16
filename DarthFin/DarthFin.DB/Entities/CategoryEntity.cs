using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace DarthFin.DB.Entities
{
    [Table("CAT_Categories")]
    public class CategoryEntity : BaseEntity
    {
        [Column("CAT_Name")]
        public string Name { get; set; }
        public CategoryEntity? ParentCategory { get; set; }
        [Column("CAT_Parent_CAT_Id")]
        public int? ParentCategoryId { get; set; }
        public UserEntity? User { get; set; }
        [Column("CAT_USR_Id")]
        public int? UserId { get; set; }
    }
}
