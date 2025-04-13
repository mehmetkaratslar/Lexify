using Lexify.Models;
using Lexify.Services;
using Lexify.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lexify.ViewModels
{
    public class MixedTestViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private ObservableCollection<TestQuestion> _questions = new();
        private int _currentIndex;

        public MixedTestViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            SelectAnswerCommand = new RelayCommand(SelectAnswer);
            _ = LoadQuestionsAsync();
        }

        public ICommand SelectAnswerCommand { get; }

        public TestQuestion? CurrentQuestion => _questions.Count > 0 && _currentIndex < _questions.Count ? _questions[_currentIndex] : null;

        private async Task LoadQuestionsAsync()
        {
            var words = await _databaseService.GetAllWordsAsync();
            _questions = new ObservableCollection<TestQuestion>(
                words.Select(w => new TestQuestion
                {
                    WordID = w.WordID,
                    QuestionText = w.Definitions.FirstOrDefault()?.DefinitionText ?? "(Tanım Yok)",
                    CorrectAnswer = w.WordText,
                    Options = words.OrderBy(_ => Guid.NewGuid()).Take(3).Select(x => x.WordText)
                                   .Append(w.WordText).OrderBy(x => Guid.NewGuid()).ToList()
                })
            );
            _currentIndex = 0;
            OnPropertyChanged(nameof(CurrentQuestion));
        }

        private void SelectAnswer(object? selected)
        {
            if (CurrentQuestion == null || selected is not string answer) return;

            CurrentQuestion.SelectedAnswer = answer;
            OnPropertyChanged(nameof(CurrentQuestion));

            Task.Delay(1000).ContinueWith(_ =>
            {
                _currentIndex++;
                OnPropertyChanged(nameof(CurrentQuestion));
            });
        }
    }
}
