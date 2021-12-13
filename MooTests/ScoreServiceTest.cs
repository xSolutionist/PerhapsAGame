using Microsoft.EntityFrameworkCore;
using PerhapsAGame.Core.DataAccess;
using PerhapsAGame.Core.Entities;
using PerhapsAGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MooTests
{
    public abstract class ScoreServiceTest
    {
        #region Seeding
        protected ScoreServiceTest(DbContextOptions<SQLiteContext> contextOptions)
        {

            ContextOptions = contextOptions;
            Seed();

        }
        protected DbContextOptions<SQLiteContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new SQLiteContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var one = new Player();
                one.Name = "PlayerOne";

                var two = new Player();
                two.Name = "PlayerThree";

                var three = new Player();
                three.Name = "PlayerTwo";

                context.AddRange(one, two, three);

                context.SaveChanges();
            }
        }
        #endregion

        #region CanGetItems
        [Fact]
        public void Can_get_players()
        {
            using (var context = new SQLiteContext(ContextOptions))
            {
                var controller = new ScoreService(context);

                var players = controller.GetPlayers().ToList();

                Assert.Equal(3, players.Count);
                Assert.Equal("PlayerOne", players[0].Name);
                Assert.Equal("PlayerThree", players[1].Name);
                Assert.Equal("PlayerTwo", players[2].Name);
            }
        }
        #endregion

        [Fact]
        public void Can_get_player()
        {
            using (var context = new SQLiteContext(ContextOptions))
            {
                var controller = new ScoreService(context);

                var item = controller.GetPlayerByName("PlayerTwo");

                Assert.Equal("PlayerTwo", item.Name);
            }
        }

        #region CanAddItem
        [Fact]
        public void Can_add_player()
        {
            using (var context = new SQLiteContext(ContextOptions))
            {
                var controller = new ScoreService(context);

                var player = new Player() { Name = "PlayerFour" };
                var item = controller.CreatePlayer(player);
            }

            using (var context = new SQLiteContext(ContextOptions))
            {
                var item = context.Set<Player>().Single(e => e.Name == "PlayerFour");

                Assert.Equal("PlayerFour", item.Name);
                Assert.Equal(0, item.Scores.Count());
            }
        }
        #endregion

        #region CanAddItemCaseInsensitive
        [Fact]
        public void Can_add_item_differing_only_by_case()
        {
            using (var context = new SQLiteContext(ContextOptions))
            {
                var controller = new ScoreService(context);

                var player = new Player() { Name = "PlayerFive" };
                var item = controller.CreatePlayer(player);

                Assert.Equal("PlayerFive", item.Name);
            }

            using (var context = new SQLiteContext(ContextOptions))
            {
                var item = context.Set<Player>().Single(e => e.Name == "PlayerFive");

                Assert.Equal("PlayerFive", item.Name);
            }
        }
        #endregion

        #region CanAddTag
        [Fact]
        public void Can_add_score()
        {
            using (var context = new SQLiteContext(ContextOptions))
            {
                var controller = new ScoreService(context);
                var player = new Player() { Name = "PlayerSix" };
                var score = new Score() { AverageScore = 50, Player = player };
                controller.CreateScore(score);
                context.SaveChanges();

                Assert.Equal(50, score.AverageScore);
            }

            using (var context = new SQLiteContext(ContextOptions))
            {
                var item = context.Set<Player>().Include(e => e.Scores).Single(e => e.Name == "PlayerSix");
                var score = context.Scores.Where(x => x.Player == item).Single();
                Assert.Equal(1, item.Scores.Count);
                Assert.Equal(50, score.AverageScore);
            }
        }
        #endregion

        #region CanUpTagCount
        [Fact]
        public void Can_add_score_when_already_existing_score()
        {
            using (var context = new SQLiteContext(ContextOptions))
            {
                var controller = new ScoreService(context);
                var player = context.Players.Single(x => x.Name == "PlayerThree");
                var score = new Score() { AverageScore = 25 };
                var score2 = new Score() { AverageScore = 30 };
                player.Scores.Add(score);
                player.Scores.Add(score2);
                context.SaveChanges();
            }

            using (var context = new SQLiteContext(ContextOptions))
            {
                var controller = new ScoreService(context);
                var player = context.Players.Include(e => e.Scores).Single(e => e.Name == "PlayerThree");
                Assert.Equal(2, player.Scores.Count);
            }
        }
        #endregion

        #region DeleteItem
        [Fact]
        public void Can_remove_item_and_all_associated_tags()
        {
            using (var context = new SQLiteContext(ContextOptions))
            {
                var controller = new ScoreService(context);

                var player = context.Players.Single(x => x.Name == "PlayerThree");
                Assert.Equal("PlayerThree", player.Name);
                controller.DeletePlayer(player.PlayerId);
                Assert.False(context.Players.Any(e => e.Name == "PlayerThree"));
                Assert.False(context.Scores.Any(e => e.AverageScore == 30));
            }

        }
        #endregion
    }


}

