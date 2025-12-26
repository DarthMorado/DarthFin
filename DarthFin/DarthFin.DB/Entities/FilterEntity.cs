using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarthFin.DB.Entities
{
    [Table("FLT_FILTER")]
    public class FilterEntity : BaseEntity
    {
        [Column("FLT_Correspondent")]
        public string? Correspondent { get; set; }
        [Column("FLT_Information")]
        public string? Information { get; set; }
        [Column("FLT_DateFrom")]
        public DateTime? DateFrom { get; set; }
        [Column("FLT_DateTill")]
        public DateTime? DateTill { get; set; }
        [Column("FLT_AmountFrom")]
        public double? AmountFrom { get; set; }
        [Column("FLT_AmountTill")]
        public double? AmountTill { get; set; }

        public CategoryEntity Category { get; set; }
        public int CategoryId { get; set; }
    }
}
