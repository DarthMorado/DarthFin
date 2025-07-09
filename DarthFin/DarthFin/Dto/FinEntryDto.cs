namespace DarthFin.Dto
{
    public class FinEntryDto
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Comment { get; set; }
        //konts
        public string Account { get; set; }
        public bool IsExpense { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? RealDate { get; set; }
        public string Company { get; set; }
        public string Information { get; set; }
        public FileDto FromFile { get; set; }
        public int FromFileId { get; set; }
        public int UserId { get; set; }
        public string ExternalId { get; set; }
    }
}
