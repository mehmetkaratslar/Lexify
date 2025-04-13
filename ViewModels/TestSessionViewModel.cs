using Lexify.Helpers;
using Lexify.Models;
using Lexify.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Lexify.ViewModels
{
    public class TestSessionViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private ObservableCollection<TestQuestion> _questions;
        private int _currentIndex;

        public TestQuestion CurrentQuestion => _questions != null && _questions.Count > 0 ? _questions[_currentIndex] : null;

        public ICommand SelectAnswerCommand { get; }

        public TestSessionViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _questions = new(); // initialize here

            SelectAnswerCommand = new RelayCommand(SelectAnswer);

            _ = LoadQuestionsAsync(); // fire and forget (idealde dışarıdan await edilmelidir)
        }

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

        private async void LoadQuestions()
        {
            var words = await _databaseService.GetAllWordsAsync();
            _questions = new ObservableCollection<TestQuestion>(
                words.Select(w => new TestQuestion
                {
                    WordID = w.WordID,
                    QuestionText = w.Definitions.FirstOrDefault()?.DefinitionText ?? "",
                    CorrectAnswer = w.WordText,
                    Options = words.OrderBy(_ => Guid.NewGuid())
                                   .Take(3)
                                   .Select(x => x.WordText)
                                   .Append(w.WordText)
                                   .OrderBy(x => Guid.NewGuid())
                                   .ToList()
                })
            );

            _currentIndex = 0;
            OnPropertyChanged(nameof(CurrentQuestion));
        }

        private void SelectAnswer(object selectedOption)
        {
            if (CurrentQuestion == null || selectedOption is not string selected) return;

            CurrentQuestion.SelectedAnswer = selected;
            OnPropertyChanged(nameof(CurrentQuestion));
        }
    }
}
