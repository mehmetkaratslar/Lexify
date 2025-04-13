using Lexify.Models;
using Lexify.Services;
using Lexify.Helpers;
using System.Windows.Input;
using System.Threading.Tasks;

namespace Lexify.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private object _currentView;

        public MainViewModel()
        {
            _databaseService = new DatabaseService();

            // İlk görünüm: Dashboard
            CurrentView = new DashboardViewModel(_databaseService);

            // Komutları oluştur
            NavigateToDashboardCommand = new RelayCommand(_ => NavigateToDashboard());
            NavigateToWordsCommand = new RelayCommand(_ => NavigateToWords());
            NavigateToAddWordCommand = new RelayCommand(_ => NavigateToAddWord());
            NavigateToTestsCommand = new RelayCommand(_ => NavigateToTests());
            NavigateToStatisticsCommand = new RelayCommand(_ => NavigateToStatistics());
            NavigateToSettingsCommand = new RelayCommand(_ => NavigateToSettings());

            // Test ekranı komutları
            NavigateToMixedTestCommand = new RelayCommand(_ => NavigateToMixedTest());
            NavigateToDefinitionFromWordCommand = new RelayCommand(_ => NavigateToDefinitionFromWord());
            NavigateToDefinitionToWordTestCommand = new RelayCommand(_ => NavigateToDefinitionToWordTest());
            NavigateToPronunciationTestCommand = new RelayCommand(_ => NavigateToPronunciationTest());
        }


        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        // Navigasyon komutları
        public ICommand NavigateToDashboardCommand { get; }
        public ICommand NavigateToWordsCommand { get; }
        public ICommand NavigateToAddWordCommand { get; }
        public ICommand NavigateToTestsCommand { get; }
        public ICommand NavigateToStatisticsCommand { get; }
        public ICommand NavigateToSettingsCommand { get; }

        // Test komutları
        public ICommand NavigateToMixedTestCommand { get; }
        public ICommand NavigateToDefinitionFromWordCommand { get; }
        public ICommand NavigateToDefinitionToWordTestCommand { get; }
        public ICommand NavigateToPronunciationTestCommand { get; }

        // Dashboard'a yönlendir
        public void NavigateToDashboard()
        {
            CurrentView = new DashboardViewModel(_databaseService);
        }

        // Tüm kelimeleri listeleyen sayfaya yönlendir
        public void NavigateToWords()
        {
            CurrentView = new WordsViewModel(_databaseService, this);
        }

        // Yeni kelime ekleme sayfasına yönlendir
        public void NavigateToAddWord()
        {
            var addEditViewModel = new AddEditWordViewModel(_databaseService);
            addEditViewModel.NavigationCompleted += (sender, e) => NavigateToDashboard();
            CurrentView = addEditViewModel;
        }

        // Kelime düzenleme sayfasına yönlendir
        public async void NavigateToEditWord(Word word)
        {
            if (word == null) return;

            var addEditViewModel = new AddEditWordViewModel(_databaseService);
            await addEditViewModel.LoadWordAsync(word.WordID);
            addEditViewModel.NavigationCompleted += (sender, e) => NavigateToDashboard();
            CurrentView = addEditViewModel;
        }

        // Kelime detay sayfasına yönlendir
        public async void NavigateToWordDetail(Word word)
        {
            if (word == null) return;

            var wordDetailViewModel = new WordDetailViewModel(_databaseService);
            await wordDetailViewModel.LoadWordAsync(word.WordID);
            CurrentView = wordDetailViewModel;
        }

        // Test menüsüne yönlendir
        public void NavigateToTests()
        {
            CurrentView = new TestsViewModel(_databaseService, this);
        }

        // Test ekranlarına yönlendirmeler
        public void NavigateToMixedTest()
        {
            CurrentView = new MixedTestViewModel(_databaseService);
        }

        public void NavigateToDefinitionFromWord()
        {
            CurrentView = new WordToDefinitionTestViewModel(_databaseService);
        }

        public void NavigateToDefinitionToWordTest()
        {
            CurrentView = new TestSessionViewModel(_databaseService); // veya DefinitionToWordTestViewModel
        }

        public void NavigateToPronunciationTest()
        {
            CurrentView = new PronunciationTestViewModel(_databaseService);
        }

        // İstatistik sayfasına yönlendir
        public void NavigateToStatistics()
        {
            CurrentView = new StatisticsViewModel(_databaseService);
        }

        // Ayarlar sayfasına yönlendir
        public void NavigateToSettings()
        {
            CurrentView = new SettingsViewModel();
        }
    }
}
