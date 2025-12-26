using System.ComponentModel.DataAnnotations.Schema;

namespace DarthFin.DB.Entities
{
    [Table("FIN_Entries")]
    public class FinEntryEntity : BaseEntity
    {
        [Column("FIN_Prices")]
        public double? Amount { get; set; }
        [Column("FIN_Entry_Type")]
        public int EntryType { get; set; } //FinEntryType
        [Column("FIN_Comment")]
        public string? Comment { get; set; }
        [Column("FIN_Account")]
        public string? Account { get; set; }
        [Column("FIN_Is_Expense")]
        public bool? IsExpense { get; set; }
        [Column("FIN_Entry_Date")]
        public DateTime? EntryDate { get; set; }
        [Column("FIN_Real_Date")]
        public DateTime? RealDate { get; set; }
        [Column("FIN_Company")]
        public string? Correspondent { get; set; }
        [Column("FIN_Information")]
        public string? Information { get; set; }
        public FileEntity FromFile { get; set; }
        [Column("FIN_FIL_Id")]
        public int FromFileId { get; set; }
        [Column("FIN_USR_Id")]
        public int UserId { get; set; }
        [Column("FIN_External_Id")]
        public string? ExternalId { get; set; }
        [Column("FIN_Document_Number")]
        public string? DocumentNumber { get; set; }

        public CategoryEntity? Category { get; set; }
        [Column("FIN_CAT_Id")]
        public int? CategoryId { get; set; }
        [Column("FIN_FLT_Id")]
        public int? FilterId { get; set; }
    }
}
