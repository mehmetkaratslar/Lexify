using Lexify.Models;
using Lexify.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lexify.ViewModels
{
    public class WordsViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private readonly MainViewModel _mainViewModel;
        private ObservableCollection<Word> _words;
        private string _searchText;

        public WordsViewModel(DatabaseService databaseService, MainViewModel mainViewModel = null)
        {
            _databaseService = databaseService;
            _mainViewModel = mainViewModel;
            _words = new ObservableCollection<Word>();

            // Komutları başlat
            AddNewWordCommand = new RelayCommand(param => AddNewWord());
            ViewWordDetailCommand = new RelayCommand(param => ViewWordDetail(param as Word));
            TestWordCommand = new RelayCommand(param => TestWord(param as Word));
            ShowFiltersCommand = new RelayCommand(param => ShowFilters());
            ShowSortOptionsCommand = new RelayCommand(param => ShowSortOptions());

            // Verileri yükle
            LoadWordsAsync();
        }

        public ObservableCollection<Word> Words
        {
            get => _words;
            set => SetProperty(ref _words, value);
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    // Arama metni değiştiğinde filtreleme yap
                    FilterWords();
                }
            }
        }

        // Komutlar
        public ICommand AddNewWordCommand { get; }
        public ICommand ViewWordDetailCommand { get; }
        public ICommand TestWordCommand { get; }
        public ICommand ShowFiltersCommand { get; }
        public ICommand ShowSortOptionsCommand { get; }

        private async Task LoadWordsAsync()
        {
            var allWords = await _databaseService.GetAllWordsAsync();

            Words.Clear();
            foreach (var word in allWords)
            {
                Words.Add(word);
            }
        }

        private void FilterWords()
        {
            // Basit arama işlevi
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                LoadWordsAsync();  // Arama yoksa tüm kelimeleri yükle
            }
            else
            {
                // Filtreleme işlemi - tüm kelimeleri yükle ve sonra filtrele
                Task.Run(async () => {
                    var allWords = await _databaseService.GetAllWordsAsync();
                    var filteredWords = allWords.Where(w =>
                        w.WordText.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                        (w.Definitions != null && w.Definitions.Any(d => d.DefinitionText.Contains(SearchText, StringComparison.OrdinalIgnoreCase)))
                    ).ToList();

                    // UI thread'de ObservableCollection'ı güncelle
                    App.Current.Dispatcher.Invoke(() => {
                        Words.Clear();
                        foreach (var word in filteredWords)
                        {
                            Words.Add(word);
                        }
                    });
                });
            }
        }

        private void AddNewWord()
        {
            if (_mainViewModel != null)
            {
                _mainViewModel.NavigateToAddWord();
            }
        }

        private void ViewWordDetail(Word word)
        {
            if (word == null || _mainViewModel == null) return;

            _mainViewModel.NavigateToWordDetail(word);
        }

        private void TestWord(Word word)
        {
            if (word == null) return;

            // Bu kısmı ileride test sayfası işlevselliği eklendiğinde tamamlayacağız
        }

        private void ShowFilters()
        {
            // Filtreleme panelini göster (ileride eklenecek)
        }

        private void ShowSortOptions()
        {
            // Sıralama seçeneklerini göster (ileride eklenecek)
        }
    }
}