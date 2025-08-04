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

        public class Map : ClassMap<SwedbankCsvEntryDto>
        {
            public Map()
            {
                Map(x => x.AccountNumber).Name("Klienta konts");
                Map(x => x.AccountNumber).Name("Klienta konts");
                Map(x => x.EntryType).Name("Ieraksta tips");
                Map(x => x.Date).Name("Datums");
                Map(x => x.Correspondent).Name("Saņēmējs/Maksātājs");
                Map(x => x.Information).Name("Informācija saņēmējam");
                Map(x => x.Amount).Name("Summa");
                Map(x => x.Currency).Name("Valūta");
                Map(x => x.Direction).Name("Debets/Kredīts");
                Map(x => x.ArchiveCode).Name("Arhīva kods");
                Map(x => x.PaymentType).Name("Maksājuma veids");
                Map(x => x.ReferenceNumber).Name("Refernces numurs");
                Map(x => x.DocumentNumber).Name("Dokumenta numurs");
            }
        }
    }
}
