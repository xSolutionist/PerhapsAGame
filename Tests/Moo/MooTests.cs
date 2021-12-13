using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PerhapsAGame.Core.DataAccess;
using PerhapsAGame.Core.Entities;
using PerhapsAGame.Core.GameBase;
using PerhapsAGame.Core.Moo;
using PerhapsAGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Moo
{

    //public class SqliteInMemoryItemsControllerTest : ItemsControllerTest, IDisposable
    //{
    //    private readonly DbConnection _connection;

    //    public SqliteInMemoryItemsControllerTest()
    //        : base(
    //            new DbContextOptionsBuilder<ItemsContext>()
    //                .UseSqlite(CreateInMemoryDatabase())
    //                .Options)
    //    {
    //        _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
    //    }

    //    private static DbConnection CreateInMemoryDatabase()
    //    {
    //        var connection = new SqliteConnection("Filename=:memory:");

    //        connection.Open();

    //        return connection;
    //    }

    //    public void Dispose() => _connection.Dispose();
    //}


    [TestClass]
    public class MooTests
    {

        //private readonly InMemSQLiteContext context;
        MooGameController mockGame;     
        List<string> mockCommands = new();
        List<string> mockOutput = new();
        IScoreService _service;         


        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<SQLiteContext>()
                .UseInMemoryDatabase(databaseName: "TestDaba")
                .Options;

            var hackatronPrime = new Player() { Name = "HackatronPrime" };
            var chuckaMockTruck = new Player() { Name = "ChuckaMockTruck" };
            Score mockScore = new() { Player = hackatronPrime, AverageScore = 5 };
            Score mockScore2 = new() { Player = chuckaMockTruck, AverageScore = 10 };
            using (var context = new SQLiteContext())
            {
                context.Database.EnsureCreated();
                context.Players.AddRange
                    (
                       hackatronPrime,
                      chuckaMockTruck
                    );

                context.Scores.AddRange
                    (
                       mockScore,
                       mockScore2
                    );
            }

            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder()
            .UseInMemoryDatabase
           (
                Guid.NewGuid().ToString()
           );
            var mockDB = new SQLiteContext();


            IScoreService service = new ScoreService(mockDB);
            _service = service;
            MooState mockState = new MooState()
            {
                Target = new int[] { 2, 4, 6, 8 },
            };
            //IMooOrdinance ordinance = new MooOrdinance(service, input, output, mockState);
            //MooGameController game = new MooGameController(ordinance);
            //mockGame = game;
        }

        [TestMethod]
        public void GetAllTest()
        {
            DbContextOptions<SQLiteContext> options = SeedInMemoryDatabase();

            using (var context = new SQLiteContext())
            {
                context.Database.EnsureCreated();
                ScoreService service = new ScoreService(context);
                List<Player> players = service.GetPlayers().ToList();

                Assert.AreEqual(2, players.Count);

            }

        }

        private static DbContextOptions<SQLiteContext> SeedInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<SQLiteContext>()
                .UseInMemoryDatabase(databaseName: "TestDaba")
                .Options;

            var hackatronPrime = new Player() { Name = "HackatronPrime" };
            var chuckaMockTruck = new Player() { Name = "ChuckaMockTruck" };
            Score mockScore = new() { Player = hackatronPrime, AverageScore = 5 };
            Score mockScore2 = new() { Player = chuckaMockTruck, AverageScore = 10 };

            using (var context = new SQLiteContext())
            {

                context.Players.AddRange
                    (
                       hackatronPrime,
                      chuckaMockTruck
                    );

                context.Scores.AddRange
                    (
                       mockScore,
                       mockScore2
                    );

                context.SaveChanges();
            }

            return options;
        }

        [TestMethod]
        public void Test_DbIsNotEmpty()
        {
            int players = 0;
            foreach (var item in _service.GetPlayers())
            {
                players++;
            }

            Assert.AreEqual(14, players);
        }


        [TestMethod]
        public void Test_Output()
        {

            Assert.AreEqual("BBB,", mockOutput[4]);
        }

        class MockInput : IInputProvider
        {
            public string Read()
            {
                throw new NotImplementedException();
            }
        }

        class MockOutput : IOutputProvider
        {
            public void Write(string message)
            {
                throw new NotImplementedException();
            }
        }

        class Mockservice : IScoreService
        {
            public Player CreatePlayer(Player player)
            {
                throw new NotImplementedException();
            }

            public Score CreateScore(Score score)  // oo
            {
                throw new NotImplementedException();
            }

            public void DeletePlayer(int id)
            {

            }

            public void DeleteScore(int id) // oo
            {
            }

            public void EditPlayer(int id, Player player)  // oo
            {
            }

            public void EditScore(int id, Score score)
            {
                throw new NotImplementedException();
            }

            public bool GamescoreExists(Player player, string gameName)  // oo
            {
                throw new NotImplementedException();
            }

            public Player GetPlayerById(int id)  // oo
            {
                throw new NotImplementedException();
            }

            public Player GetPlayerByName(string name)  // oo
            {
                throw new NotImplementedException();
            }

            public Player GetPlayerByScore(Score score)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Player> GetPlayers()
            {
                throw new NotImplementedException();
            }

            public Score GetScoreById(int id)
            {
                throw new NotImplementedException();
            }

            public Score GetScoreByPlayer(Player player)  // oo
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Score> GetScores()  // oo
            {
                throw new NotImplementedException();
            }

            public bool PlayerExists(int id)
            {
                throw new NotImplementedException();
            }

            public bool PlayerExists(string name)  // oo
            {
                throw new NotImplementedException();
            }

            public void SaveChanges()
            {
                throw new NotImplementedException();
            }

            public bool ScoreExists(Player player) // oo
            {
                throw new NotImplementedException();
            }
        }
    }
}
