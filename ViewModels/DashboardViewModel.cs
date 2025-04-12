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
    public class DashboardViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        private int _totalWordsCount;
        private int _learnedWordsCount;
        private int _learningWordsCount;
        private int _newWordsCount;
        private ObservableCollection<Word> _recentWords;
        private Word _wordOfTheDay;

        public DashboardViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _recentWords = new ObservableCollection<Word>();

            // Verileri yükle
            LoadDashboardDataAsync();
        }

        public int TotalWordsCount
        {
            get => _totalWordsCount;
            set => SetProperty(ref _totalWordsCount, value);
        }

        public int LearnedWordsCount
        {
            get => _learnedWordsCount;
            set => SetProperty(ref _learnedWordsCount, value);
        }

        public int LearningWordsCount
        {
            get => _learningWordsCount;
            set => SetProperty(ref _learningWordsCount, value);
        }

        public int NewWordsCount
        {
            get => _newWordsCount;
            set => SetProperty(ref _newWordsCount, value);
        }

        public ObservableCollection<Word> RecentWords
        {
            get => _recentWords;
            set => SetProperty(ref _recentWords, value);
        }

        public Word WordOfTheDay
        {
            get => _wordOfTheDay;
            set => SetProperty(ref _wordOfTheDay, value);
        }

        private async Task LoadDashboardDataAsync()
        {
            try
            {
                var allWords = await _databaseService.GetAllWordsAsync();

                // İstatistikleri güncelle
                TotalWordsCount = allWords.Count;
                LearnedWordsCount = allWords.Count(w => w.LearningStatus == "Öğrenildi");
                LearningWordsCount = allWords.Count(w => w.LearningStatus == "Öğreniliyor");
                NewWordsCount = allWords.Count(w => w.LearningStatus == "Yeni");

                // Son eklenen kelimeleri yükle (en fazla 5 tane)
                var recentWords = allWords
                    .OrderByDescending(w => w.DateAdded)
                    .Take(5)
                    .ToList();

                RecentWords.Clear();
                foreach (var word in recentWords)
                {
                    RecentWords.Add(word);
                }

                // Günün kelimesini belirle (rastgele veya algoritma ile)
                if (allWords.Any())
                {
                    // Basit bir örnek: Rastgele bir kelime seç
                    Random random = new Random();
                    int randomIndex = random.Next(allWords.Count);
                    WordOfTheDay = allWords[randomIndex];
                }
            }
            catch (Exception ex)
            {
                // Hata işleme
                System.Diagnostics.Debug.WriteLine($"Dashboard veri yükleme hatası: {ex.Message}");
            }
        }
    }
}