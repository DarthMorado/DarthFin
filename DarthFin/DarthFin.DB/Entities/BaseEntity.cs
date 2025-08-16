using System.ComponentModel.DataAnnotations;

namespace DarthFin.DB.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
