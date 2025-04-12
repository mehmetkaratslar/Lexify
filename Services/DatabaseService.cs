using Lexify.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lexify.Services
{
    public class DatabaseService
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseInitializer _initializer;

        public DatabaseService()
        {
            _context = new ApplicationDbContext();
            _initializer = new DatabaseInitializer(_context);

            // Veritabanını başlat
            InitializeDatabaseAsync();
        }

        private async void InitializeDatabaseAsync()
        {
            await _initializer.InitializeAsync();
        }

        // Kelime işlemleri
        public async Task<List<Word>> GetAllWordsAsync()
        {
            return await _context.Words
                .Include(w => w.Definitions)
                .Include(w => w.Examples)
                .ToListAsync();
        }

        public async Task<Word> GetWordByIdAsync(int id)
        {
            return await _context.Words
                .Include(w => w.Definitions)
                .Include(w => w.Examples)
                .Include(w => w.RelatedWordsFrom)
                    .ThenInclude(wr => wr.RelatedWord)
                .Include(w => w.RelatedWordsTo)
                    .ThenInclude(wr => wr.Word)
                .FirstOrDefaultAsync(w => w.WordID == id);
        }

        public async Task<Word> AddWordAsync(Word word)
        {
            _context.Words.Add(word);
            await _context.SaveChangesAsync();
            return word;
        }

        public async Task UpdateWordAsync(Word word)
        {
            _context.Entry(word).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWordAsync(int id)
        {
            var word = await _context.Words.FindAsync(id);
            if (word != null)
            {
                _context.Words.Remove(word);
                await _context.SaveChangesAsync();
            }
        }

        // Tanım işlemleri
        public async Task<Definition> AddDefinitionAsync(Definition definition)
        {
            _context.Definitions.Add(definition);
            await _context.SaveChangesAsync();
            return definition;
        }

        // Örnek cümle işlemleri
        public async Task<Example> AddExampleAsync(Example example)
        {
            _context.Examples.Add(example);
            await _context.SaveChangesAsync();
            return example;
        }

        // Kelime ilişkisi (eş/zıt anlamlı) işlemleri
        public async Task<WordRelation> AddWordRelationAsync(WordRelation relation)
        {
            _context.WordRelations.Add(relation);
            await _context.SaveChangesAsync();
            return relation;
        }
    }
}