using Lexify.Models;
using Lexify.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lexify.ViewModels
{
    public class WordsViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private ObservableCollection<Word> _words;
        private string _searchText;

        public WordsViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _words = new ObservableCollection<Word>();

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
            // Şimdilik boş - ileride arama işlevi eklenecek
        }
    }
}