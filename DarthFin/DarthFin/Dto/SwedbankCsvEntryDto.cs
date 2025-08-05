using CsvHelper.Configuration;

namespace DarthFin.Dto
{
    public class SwedbankCsvEntryDto
    {
        //Klienta konts
        public string AccountNumber { get; set; }
        //Ieraksta tips
        public string EntryType { get; set; }
        //Datums
        public string Date { get; set; }
        //Saņēmējs/Maksātājs
        public string Correspondent { get; set; }
        //Informācija saņēmējam
        public string Information { get; set; }
        //Summa
        public string Amount { get; set; }
        //Valūta
        public string Currency { get; set; }
        //Debets/Kredīts
        public string Direction { get; set; }
        //Arhīva kods
        public string ArchiveCode { get; set; }
        //Maksājuma veids
        public string PaymentType { get; set; }
        //Refernces numurs
        public string ReferenceNumber { get; set; }
        //Dokumenta numurs
        public string DocumentNumber { get; set; }

        
    }
}
