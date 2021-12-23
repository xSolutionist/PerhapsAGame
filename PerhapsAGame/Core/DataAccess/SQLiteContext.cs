using Microsoft.EntityFrameworkCore;
using PerhapsAGame.Core.Entities;


namespace PerhapsAGame.Core.DataAccess
{
    public class SQLiteContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Score> Scores { get; set; }

        public string DbPath { get; private set; }

        public SQLiteContext()
        {
            var folder = Environment.SpecialFolder.Desktop;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{Path.DirectorySeparatorChar}PerhapsGameDB.db";
        }
        public SQLiteContext(DbContextOptions options)
        {
            var folder = Environment.SpecialFolder.Desktop;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{Path.DirectorySeparatorChar}TestDb.db";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
              => options.UseSqlite($"Data Source={DbPath}");
    }
}
