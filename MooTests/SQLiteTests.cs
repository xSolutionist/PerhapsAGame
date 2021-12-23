using Microsoft.EntityFrameworkCore;
using MooTests;
using PerhapsAGame.Core.DataAccess;

namespace Tests
{
    public class SQLiteTests : ScoreServiceTest
    {
        public SQLiteTests()
            : base(
                new DbContextOptionsBuilder<SQLiteContext>()
                    .UseSqlite("InMemDB")
                    .Options)
        {
        }
    }
}