using CsvHelper.Configuration;
using DarthFin.Dto;

namespace DarthFin.Mapping.CSV
{
    public class SwedbankCsvMapping : ClassMap<SwedbankCsvEntryDto>
    {
        public SwedbankCsvMapping()
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
