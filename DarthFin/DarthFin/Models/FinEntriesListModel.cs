using DarthFin.Dto;

namespace DarthFin.Models
{
    public class FinEntriesListModel 
    {
        public List<FinEntryDto> FinEntries { get; set; } = new();
    }
}
