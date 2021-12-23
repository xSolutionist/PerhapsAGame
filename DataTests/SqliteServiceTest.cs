using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MooTests;
using PerhapsAGame.Core.DataAccess;

namespace InmemoryTests
{
    public class SqliteServiceTest : ScoreServiceTest
    {
        public SqliteServiceTest()
            : base
            (new DbContextOptionsBuilder<SQLiteContext>()
                    .UseSqlite("Filename=Test.db")
                    .Options)
        {
        }

    }
}