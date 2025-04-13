using Lexify.Helpers;
using Lexify.Models;
using Lexify.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lexify.ViewModels
{
    public class AddEditWordViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        public AddEditWordViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;

            CurrentWord = new Word
            {
                DateAdded = DateTime.Now,
                Definitions = new List<Definition>(),
                Examples = new List<Example>(),


                ColorCode = "#5E81AC", // varsayılan mavi renk
            };

            Definitions = new ObservableCollection<Definition>();
            Examples = new ObservableCollection<Example>();

            AddDefinitionCommand = new RelayCommand(_ => AddDefinition(), _ => !string.IsNullOrWhiteSpace(NewDefinitionText));
            RemoveDefinitionCommand = new RelayCommand(d => RemoveDefinition(d as Definition));

            AddExampleCommand = new RelayCommand(_ => AddExample(), _ => !string.IsNullOrWhiteSpace(NewExampleText));
            RemoveExampleCommand = new RelayCommand(e => RemoveExample(e as Example));

            SaveCommand = new RelayCommand(_ => Save());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        public event EventHandler NavigationCompleted;

        public async Task LoadWordAsync(int wordId)
        {
            var word = await _databaseService.GetWordByIdAsync(wordId);
            if (word != null)
            {
                CurrentWord = word;
                Definitions = new ObservableCollection<Definition>(word.Definitions);
                Examples = new ObservableCollection<Example>(word.Examples);
            }
        }

        private Word _currentWord;
        public Word CurrentWord
        {
            get => _currentWord;
            set => SetProperty(ref _currentWord, value);
        }


        public ObservableCollection<Definition> Definitions { get; set; }
        public string NewDefinitionText { get; set; }

        public ICommand AddDefinitionCommand { get; }
        public ICommand RemoveDefinitionCommand { get; }

        private void AddDefinition()
        {
            var def = new Definition { DefinitionText = NewDefinitionText };
            Definitions.Add(def);
            NewDefinitionText = string.Empty;
            OnPropertyChanged(nameof(NewDefinitionText));
        }

        private void RemoveDefinition(Definition def)
        {
            if (def != null)
                Definitions.Remove(def);
        }

        public ObservableCollection<Example> Examples { get; set; }
        public string NewExampleText { get; set; }

        public ICommand AddExampleCommand { get; }
        public ICommand RemoveExampleCommand { get; }

        private void AddExample()
        {
            var ex = new Example { ExampleText = NewExampleText };
            Examples.Add(ex);
            NewExampleText = string.Empty;
            OnPropertyChanged(nameof(NewExampleText));
        }

        private void RemoveExample(Example ex)
        {
            if (ex != null)
                Examples.Remove(ex);
        }

        public ICommand SaveCommand { get; }
        private async void Save()
        {
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            CurrentWord.Definitions = Definitions.ToList();
            CurrentWord.Examples = Examples.ToList();

            if (CurrentWord.WordID == 0)
                await _databaseService.AddWordAsync(CurrentWord);
            else
                await _databaseService.UpdateWordAsync(CurrentWord);

            OnNavigationCompleted();
        }

        public ICommand CancelCommand { get; }
        private void Cancel()
        {
            OnNavigationCompleted();
        }

        protected void OnNavigationCompleted()
        {
            NavigationCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
