using Lexify.Services;
using System.Windows.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            // İlk görünümü DashboardViewModel olarak ayarla
            CurrentView = new DashboardViewModel(_databaseService);

            // Komutları oluştur
            NavigateToDashboardCommand = new RelayCommand(param => NavigateToDashboard());
            NavigateToWordsCommand = new RelayCommand(param => NavigateToWords());
            NavigateToAddWordCommand = new RelayCommand(param => NavigateToAddWord());
            NavigateToTestsCommand = new RelayCommand(param => NavigateToTests());
            NavigateToStatisticsCommand = new RelayCommand(param => NavigateToStatistics());
            NavigateToSettingsCommand = new RelayCommand(param => NavigateToSettings());
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

        // Navigasyon metodları
        private void NavigateToDashboard()
        {
            CurrentView = new DashboardViewModel(_databaseService);
        }

        private void NavigateToWords()
        {
            CurrentView = new WordsViewModel(_databaseService);
        }

        private void NavigateToAddWord()
        {
            CurrentView = new AddEditWordViewModel(_databaseService);
        }

        private void NavigateToTests()
        {
            CurrentView = new TestsViewModel(_databaseService);
        }

        private void NavigateToStatistics()
        {
            CurrentView = new StatisticsViewModel(_databaseService);
        }

        private void NavigateToSettings()
        {
            CurrentView = new SettingsViewModel();
        }
    }
}