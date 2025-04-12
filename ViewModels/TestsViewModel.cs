using Lexify.Services;

namespace Lexify.ViewModels
{
    public class TestsViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        public TestsViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
    }
}