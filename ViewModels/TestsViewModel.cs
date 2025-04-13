using Lexify.Helpers;
using Lexify.Services;
using System.Windows.Input;

namespace Lexify.ViewModels
{
    public class TestsViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private readonly MainViewModel _mainViewModel;

        // 🔹 Komutlar
        public ICommand NavigateToMixedTestCommand { get; }
        public ICommand NavigateToDefinitionFromWordCommand { get; }
        public ICommand NavigateToDefinitionToWordTestCommand { get; }
        public ICommand NavigateToPronunciationTestCommand { get; }

        public TestsViewModel(DatabaseService databaseService, MainViewModel mainViewModel)
        {
            _databaseService = databaseService;
            _mainViewModel = mainViewModel;

            NavigateToDefinitionToWordTestCommand = new RelayCommand(_ => _mainViewModel.NavigateToDefinitionToWordTest());
            NavigateToMixedTestCommand = new RelayCommand(_ => _mainViewModel.NavigateToMixedTest());
            NavigateToDefinitionFromWordCommand = new RelayCommand(_ => _mainViewModel.NavigateToDefinitionFromWord());
            NavigateToPronunciationTestCommand = new RelayCommand(_ => _mainViewModel.NavigateToPronunciationTest());
        }

    }
}
