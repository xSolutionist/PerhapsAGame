using Microsoft.EntityFrameworkCore;
using PerhapsAGame.Core.Entities;


namespace PerhapsAGame.Core.DataAccess
{
    public class GameContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Score> Scores { get; set; }

        public string DbPath { get; private set; }

        public GameContext()
        {
            var folder = Environment.SpecialFolder.Desktop;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}Game.db";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
              => options.UseSqlite($"Data Source={DbPath}");
    }
}
