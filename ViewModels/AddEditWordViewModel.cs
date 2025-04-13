using Lexify.Models;
using Lexify.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lexify.ViewModels
{
    public class AddEditWordViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private Word _currentWord;
        private string _newDefinitionText;
        private string _newExampleText;

        public AddEditWordViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;

            // Yeni kelime oluştur
            CurrentWord = new Word
            {
                DateAdded = DateTime.Now,
                LearningStatus = "Yeni",
                ColorCode = "#5E81AC"
            };

            // Tanımlar ve örnekler için koleksiyonları başlat
            Definitions = new ObservableCollection<Definition>();
            Examples = new ObservableCollection<Example>();

            // Komutları başlat
            AddDefinitionCommand = new RelayCommand(AddDefinition);
            RemoveDefinitionCommand = new RelayCommand(RemoveDefinition);
            AddExampleCommand = new RelayCommand(AddExample);
            RemoveExampleCommand = new RelayCommand(RemoveExample);
            SaveCommand = new RelayCommand(async param => await SaveWord());
            CancelCommand = new RelayCommand(Cancel);
        }

        public Word CurrentWord
        {
            get => _currentWord;
            set => SetProperty(ref _currentWord, value);
        }

        public ObservableCollection<Definition> Definitions { get; set; }

        public ObservableCollection<Example> Examples { get; set; }

        public string NewDefinitionText
        {
            get => _newDefinitionText;
            set => SetProperty(ref _newDefinitionText, value);
        }

        public string NewExampleText
        {
            get => _newExampleText;
            set => SetProperty(ref _newExampleText, value);
        }

        // Komutlar
        public ICommand AddDefinitionCommand { get; }
        public ICommand RemoveDefinitionCommand { get; }
        public ICommand AddExampleCommand { get; }
        public ICommand RemoveExampleCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        // Navigasyon için event
        public event EventHandler NavigationCompleted;

        // Var olan kelimeyi düzenlemek için (edit modu)
        public async Task LoadWord(int wordId)
        {
            var word = await _databaseService.GetWordByIdAsync(wordId);
            if (word != null)
            {
                CurrentWord = word;

                // Tanımları ve örnekleri yükle
                Definitions.Clear();
                if (word.Definitions != null)
                {
                    foreach (var definition in word.Definitions)
                    {
                        Definitions.Add(definition);
                    }
                }

                Examples.Clear();
                if (word.Examples != null)
                {
                    foreach (var example in word.Examples)
                    {
                        Examples.Add(example);
                    }
                }
            }
        }

        private void AddDefinition(object parameter)
        {
            if (string.IsNullOrWhiteSpace(NewDefinitionText))
                return;

            Definitions.Add(new Definition
            {
                DefinitionText = NewDefinitionText
            });

            NewDefinitionText = string.Empty;
        }

        private void RemoveDefinition(object parameter)
        {
            if (parameter is Definition definition)
            {
                Definitions.Remove(definition);
            }
        }

        private void AddExample(object parameter)
        {
            if (string.IsNullOrWhiteSpace(NewExampleText))
                return;

            Examples.Add(new Example
            {
                ExampleText = NewExampleText
            });

            NewExampleText = string.Empty;
        }

        private void RemoveExample(object parameter)
        {
            if (parameter is Example example)
            {
                Examples.Remove(example);
            }
        }

        private async Task SaveWord()
        {
            try
            {
                // Tanımları ve örnekleri kelimeye ata
                CurrentWord.Definitions = new List<Definition>(Definitions);
                CurrentWord.Examples = new List<Example>(Examples);

                // Yeni kelime mi yoksa düzenleme mi kontrolü
                if (CurrentWord.WordID == 0)
                {
                    // Yeni kelime
                    await _databaseService.AddWordAsync(CurrentWord);
                }
                else
                {
                    // Var olan kelimeyi güncelle
                    await _databaseService.UpdateWordAsync(CurrentWord);
                }

                // Navigasyon event'ini tetikle
                NavigationCompleted?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                // Hata işleme
                System.Diagnostics.Debug.WriteLine($"Kelime kaydedilirken hata: {ex.Message}");
            }
        }

        private void Cancel(object parameter)
        {
            // İptal et ve navigasyon event'ini tetikle
            NavigationCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}