using Lexify.Services;

namespace Lexify.ViewModels
{
    public class StatisticsViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        public StatisticsViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
    }
}