using DarthFin.DB.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarthFin.Dto
{
    public class FinEntryDto
    {
        public int Id { get; set; }
        public double? Amount { get; set; }
        public string? Currency { get; set; }
        public FinEntryType EntryType { get; set; }
        public string? Comment { get; set; }
        //konts
        public string? Account { get; set; }
        public bool IsExpense { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? RealDate { get; set; }
        public string? Correspondent { get; set; }
        public string? Information { get; set; }
        public FileDto FromFile { get; set; }
        public int FromFileId { get; set; }
        public int UserId { get; set; }
        public string? ExternalId { get; set; }
        public string? DocumentNumber { get; set; }

        public CategoryEntity? Category { get; set; }
        public int? CategoryId { get; set; }
        public bool IsManualCategory { get; set; }
    }
}
