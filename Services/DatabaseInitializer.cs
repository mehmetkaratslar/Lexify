using Lexify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lexify.Services
{
    public class DatabaseInitializer
    {
        private readonly ApplicationDbContext _context;

        public DatabaseInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            // Veritabanını oluştur (yoksa)
            _context.Database.EnsureCreated();

            // Eğer veritabanında hiç kelime yoksa örnek veriler ekle
            if (!_context.Words.Any())
            {
                await SeedSampleDataAsync();
            }
        }

        private async Task SeedSampleDataAsync()
        {
            // Örnek kelimeler ekle
            var words = new List<Word>
            {
                new Word
                {
                    WordText = "Ephemeral",
                    WordType = "Adjective",
                    Language = "English",
                    ColorCode = "#5E81AC",
                    LearningStatus = "Yeni",
                    DateAdded = DateTime.Now.AddDays(-5),
                    Definitions = new List<Definition>
                    {
                        new Definition { DefinitionText = "Lasting for a very short time." },
                        new Definition { DefinitionText = "Transitory; fleeting." }
                    },
                    Examples = new List<Example>
                    {
                        new Example { ExampleText = "The ephemeral nature of cherry blossoms makes them more precious." }
                    }
                },
                new Word
                {
                    WordText = "Serendipity",
                    WordType = "Noun",
                    Language = "English",
                    ColorCode = "#A3BE8C",
                    LearningStatus = "Öğreniliyor",
                    DateAdded = DateTime.Now.AddDays(-3),
                    Definitions = new List<Definition>
                    {
                        new Definition { DefinitionText = "The occurrence and development of events by chance in a happy or beneficial way." }
                    },
                    Examples = new List<Example>
                    {
                        new Example { ExampleText = "Finding that rare book was pure serendipity." }
                    }
                },
                new Word
                {
                    WordText = "Ubiquitous",
                    WordType = "Adjective",
                    Language = "English",
                    ColorCode = "#EBCB8B",
                    LearningStatus = "Öğrenildi",
                    DateAdded = DateTime.Now.AddDays(-10),
                    Definitions = new List<Definition>
                    {
                        new Definition { DefinitionText = "Present, appearing, or found everywhere." }
                    },
                    Examples = new List<Example>
                    {
                        new Example { ExampleText = "Mobile phones have become ubiquitous in modern life." }
                    }
                },
                new Word
                {
                    WordText = "Çekirdek",
                    WordType = "İsim",
                    Language = "Türkçe",
                    ColorCode = "#B48EAD",
                    LearningStatus = "Öğrenildi",
                    DateAdded = DateTime.Now.AddDays(-7),
                    Definitions = new List<Definition>
                    {
                        new Definition { DefinitionText = "Meyvelerin içinde bulunan, çoğu sert bir kabukla kaplı tohum." },
                        new Definition { DefinitionText = "Bir şeyin özü, esası." }
                    },
                    Examples = new List<Example>
                    {
                        new Example { ExampleText = "Elmanın çekirdeğini çıkardı." }
                    }
                },
                new Word
                {
                    WordText = "Gelincik",
                    WordType = "İsim",
                    Language = "Türkçe",
                    ColorCode = "#BF616A",
                    LearningStatus = "Yeni",
                    DateAdded = DateTime.Now.AddDays(-1),
                    Definitions = new List<Definition>
                    {
                        new Definition { DefinitionText = "Kırmızı renkli, uzun saplı, otsu bir bitki." },
                        new Definition { DefinitionText = "Bir tür yırtıcı, etçil hayvan." }
                    },
                    Examples = new List<Example>
                    {
                        new Example { ExampleText = "Tarlalar gelinciklerle dolmuştu." }
                    }
                }
            };

            // Kelimeleri veritabanına ekle
            _context.Words.AddRange(words);
            await _context.SaveChangesAsync();

            // Eş/zıt anlamlı kelime ilişkileri ekle
            var wordRelations = new List<WordRelation>
            {
                new WordRelation
                {
                    WordID = words[0].WordID, // Ephemeral
                    RelatedWordID = words[2].WordID, // Ubiquitous
                    RelationType = "Antonym"
                }
            };

            _context.WordRelations.AddRange(wordRelations);
            await _context.SaveChangesAsync();
        }
    }
}