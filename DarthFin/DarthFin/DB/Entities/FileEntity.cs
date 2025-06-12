using System.ComponentModel.DataAnnotations.Schema;

namespace DarthFin.DB.Entities
{
    [Table("FIL_Files")]
    public class FileEntity : BaseEntity
    {
        [Column("FIL_Name")]
        public string Name { get; set; }
        [Column("FIL_Content")]
        public byte[] Content { get; set; }
        public UserEntity User { get; set; }
        [Column("FIL_USR_Id")]
        public int UserId { get; set; }
    }
}
