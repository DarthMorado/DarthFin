using DarthFin.DB.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarthFin.Dto
{
    public class FilterDto
    {
        public string Correspondent { get; set; }
        public string Information { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTill { get; set; }
        public double? AmountFrom { get; set; }
        public double? AmountTill { get; set; }

        public CategoryDto Category { get; set; }
        public int CategoryId { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }
}
