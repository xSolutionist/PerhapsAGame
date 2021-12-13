using Microsoft.EntityFrameworkCore;
using MooTests;
using PerhapsAGame.Core.DataAccess;

namespace InmemoryTests
{
    public class InMemoryItemsControllerTest : ScoreServiceTest
    {
        public InMemoryItemsControllerTest()
            : base(
                new DbContextOptionsBuilder<SQLiteContext>()
                    .UseInMemoryDatabase("TestDatabase")
                    .Options)
        {

        }
    }
}