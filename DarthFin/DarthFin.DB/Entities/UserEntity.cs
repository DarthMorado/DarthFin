using System.ComponentModel.DataAnnotations.Schema;

namespace DarthFin.DB.Entities
{
    [Table("ADM_Users")]
    public class UserEntity : BaseEntity
    {
        [Column("USR_Gmail")]
        public string Gmail { get; set; }

        [Column("USR_Is_Admin")]
        public bool IsAdmin { get; set; }
    }
}
