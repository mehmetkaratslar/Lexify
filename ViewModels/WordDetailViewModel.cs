using Lexify.Models;
using Lexify.Services;
using System.Threading.Tasks;

namespace Lexify.ViewModels
{
    public class WordDetailViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private Word _word;

        public WordDetailViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public Word Word
        {
            get => _word;
            set => SetProperty(ref _word, value);
        }

        public async Task LoadWordAsync(int wordId)
        {
            Word = await _databaseService.GetWordByIdAsync(wordId);
        }
    }
}