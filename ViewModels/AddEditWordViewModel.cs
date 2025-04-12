using Lexify.Models;
using Lexify.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Lexify.ViewModels
{
    public class AddEditWordViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private Word _currentWord;

        public AddEditWordViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _currentWord = new Word
            {
                DateAdded = DateTime.Now,
                LearningStatus = "Yeni",
                ColorCode = "#5E81AC"
            };

            // Tanımlar ve örnekler için koleksiyonları başlat
            Definitions = new ObservableCollection<Definition>();
            Examples = new ObservableCollection<Example>();
        }

        public Word CurrentWord
        {
            get => _currentWord;
            set => SetProperty(ref _currentWord, value);
        }

        public ObservableCollection<Definition> Definitions { get; set; }
        public ObservableCollection<Example> Examples { get; set; }
    }
}