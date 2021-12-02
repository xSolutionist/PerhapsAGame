using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerhapsAGame.Core.DataAccess;
using PerhapsAGame.Core.Entities;


namespace PerhapsAGame.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly GameContext _context;

        public PlayerService(GameContext context)
        {
            _context = context;
        }
        public IEnumerable<Player> GetPlayers()
        {
            return _context.Players.ToList();
        }

        public Player GetPlayerById(int id)
        {
            var player =_context.Players.Find(id);

            if (player == null)
            {
                new EmptyResult();
            }

            return player;
        }

        public Player GetPlayerByName(string name)
        {
            var requestedplayer =  _context.Players.Where( p => p.Name == name).FirstOrDefault();

            if (requestedplayer == null)
            {
                new EmptyResult();
            }

            return requestedplayer;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void EditPlayer(int id, Player player)
        {
            if (id != player.PlayerId)
            {
                new EmptyResult();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    new EmptyResult();
                }
                else
                {
                    throw;
                }
            }
        }

        public void DeletePlayer(int id)
        {
            var player = _context.Players.Find(id);
            if (player == null)
            {
                new EmptyResult();
            }
            _context.Players.Remove(player);
            _context.SaveChanges();
           
        }

        public bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.PlayerId == id);
        }
        public bool PlayerExists(string name)
        {
            return _context.Players.Any(e => e.Name == name);
        }

        public Score GetScore(Player player)
        {
            return _context.Scores.Where(s => s.Player.PlayerId == player.PlayerId).Single();
        }

        //  SLOWER
        //bool hasMatch = myStrings.Any(x => parameters.Any(y => y.source == x));
        // FASTER
        //bool hasMatch = parameters.Select(x => x.source)
        //                  .Intersect(myStrings)
        //                  .Any();
        public bool ScoreExists(Player player)
        {
            return _context.Players.Any(p => p.Scores.Any(s => s.Player.PlayerId == player.PlayerId));
        }

        public bool GamescoreExists(Player player, string GameName)
        {
          return _context.Scores.Any(p => p.GameName == GameName && p.Player.PlayerId == player.PlayerId);
        }
        public Player CreatePlayer(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChangesAsync();

            return (player);
        }

        public Score CreateScore( Score score)
        {
            _context.Scores.Add(score);
            _context.SaveChanges();
            return (score);
        }

        public IEnumerable<Score> GetScores()
        {
            return  _context.Scores.ToList();
        }

        public void EditScore(int id, Score score)
        {
            if (id != score.ScoreId)
            {
                throw new InvalidOperationException();
            }

            _context.Entry(score).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreExists(id))
                {
                    new EmptyResult();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool ScoreExists(int id)
        {
            return _context.Scores.Any(e => e.ScoreId == id);
        }

        public void DeleteScore(int id)
        {
            var score = _context.Scores.Find(id);
            if (score == null)
            {
                new EmptyResult();
            }

            _context.Scores.Remove(score);
            _context.SaveChanges();
            
        }
        public Score GetScoreById(int id)
        {
            var score = _context.Scores.Find(id);

            if (score == null)
            {
                new EmptyResult();
            }

            return score;
        }

        public Score GetScoreByPlayer(Player player)
        {
            var score = _context.Scores.Find(player.PlayerId);

            if (score == null)
            {
                new EmptyResult();
            }

            return score;
        }
    }


}
