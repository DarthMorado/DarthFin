using DarthFin.DB.Repositories;
using DarthFin.Dto;
using DarthFin.Models.Graph;

namespace DarthFin.Services
{
    public interface IGraphService
    {
        public Task<BarGraphModel> GetMonthBarData(int userId, int? year = null, int? month = null);
    }

    public class GraphService : IGraphService
    {
        private readonly IFinEntryRepository _finEntryRepository;

        public GraphService(IFinEntryRepository finEntryRepository)
        {
            _finEntryRepository = finEntryRepository;
        }

        public async Task<BarGraphModel> GetMonthBarData(int userId, int? year = null, int? month = null)
        {
            if (!year.HasValue)
            {
                year = DateTime.Now.Year;
            }
            if (!month.HasValue)
            {
                month = DateTime.Now.Month;
            }

            var data = await _finEntryRepository.GetAllAsync(x => 
                                x.UserId == userId && 
                                (x.RealDate ?? x.EntryDate).Value.Year == year && 
                                (x.RealDate ?? x.EntryDate).Value.Month == month &&
                                x.EntryType == ((int)FinEntryType.Transaction)
                                );

            DateTime date = new DateTime(year.Value, month.Value, 1);

            var result = new BarGraphModel()
            {
                Name = date.ToString("MMMM"),
                Headers = new List<string>(),
                Categories = new List<GraphValues>()
                {
                    new GraphValues()
                    {
                        Name = "Unknown",
                        Values = new List<double>()
                    }
                }
            };

            

            for (int i = 0; i < DateTime.DaysInMonth(date.Year, date.Month); i++)
            {
                result.Headers.Add((i + 1).ToString());
                result.Categories.First().Values.Add(data.Where(x => (x.IsExpense ?? false) && (x.RealDate ?? x.EntryDate).HasValue && (x.RealDate ?? x.EntryDate).Value.Day == i + 1).Sum(x => x.Amount ?? 0));
            }

            return result;
        }
    }
}
