using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PerhapsAGame.Core.DataAccess;
using PerhapsAGame.Core.Entities;
using PerhapsAGame.Core.Moo;
using PerhapsAGame.Services;
using System;

namespace Tests.Moo
{
    [TestClass]
    public class MooTests
    {

        //private readonly InMemSQLiteContext context;
    
        [TestInitialize]
        public void Init()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder()
            .UseInMemoryDatabase
           (
                Guid.NewGuid().ToString()
           );

            var db = new SQLiteContext(dbOptions.Options);
            Player player = new();
            db.Add(player);


            //    var db = new InMemSQLiteContext();

            //    var service = new ScoreService(db);
            //    var ordinance = new MooOrdinance(service);
            //    var game = new MooGameController(ordinance);

            //}


            //    [TestMethod]
            //public void TestMethod1()
            //{
            //}


        }
    }
}
