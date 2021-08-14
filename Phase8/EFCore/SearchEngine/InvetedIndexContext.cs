using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SearchEngine.Interfaces;

namespace SearchEngine
{
    public class InvertedIndexContext : DbContext,IDatabaseMap<string,string>

    {
        public DbSet<Word> Words { get; set; }
        public DbSet<Document> Documents { get; set; }

        public InvertedIndexContext(DbContextOptions<InvertedIndexContext> options) :
            base(options)
        {
            
        }
        
        public List<string> Get(string key)
        {
            var word = Words.Where(w => w.String == key).Include(w => w.Documents).FirstOrDefault();
            if (word == null)
                return new List<string>();
            return word.Documents.Select(document => document.Name).ToList();
        }

        public void Add(string key, string value)
        {
            var word = Words.Find(key);
            if (word == null)
            {
                word = new Word() { String = key };
                Words.Add(word);
            }

            var document = Documents.Find(value);
            if (document == null)
            {
                document = new Document() { Name = value };
                Documents.Add(document);
            }
            
            if(word.Documents.Contains(document))
                return;
            word.Documents.Add(document);
        }

        public bool Delete()
        {
            return Database.EnsureDeleted();
        }

        public bool Create()
        {
            return Database.EnsureCreated();
        }

        public void Save()
        {
            SaveChanges();
        }
    }
}