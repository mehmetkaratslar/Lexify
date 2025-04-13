using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Services/TestGeneratorService.cs
using Lexify.Models;

namespace Lexify.Services
{
    public class TestGeneratorService
    {
        private readonly DatabaseService _databaseService;
        private readonly Random _random = new();

        public TestGeneratorService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<List<TestQuestion>> GenerateDefinitionToWordQuestionsAsync(int count = 5)
        {
            var words = await _databaseService.GetAllWordsAsync();
            var questions = new List<TestQuestion>();

            var definitionsPool = words.SelectMany(w => w.Definitions.Select(d => (word: w.WordText, def: d.DefinitionText))).ToList();

            for (int i = 0; i < count; i++)
            {
                var selected = definitionsPool[_random.Next(definitionsPool.Count)];
                var correct = selected.word;
                var definition = selected.def;

                var incorrectOptions = definitionsPool
                    .Where(p => p.word != correct)
                    .OrderBy(_ => _random.Next())
                    .Select(p => p.word)
                    .Distinct()
                    .Take(3)
                    .ToList();

                var options = incorrectOptions.Append(correct).OrderBy(_ => _random.Next()).ToList();

                questions.Add(new TestQuestion
                {
                    QuestionText = $"Tanımı: \"{definition}\" olan kelime nedir?",
                    Options = options,
                    CorrectAnswer = correct
                });
            }

            return questions;
        }
    }
}
