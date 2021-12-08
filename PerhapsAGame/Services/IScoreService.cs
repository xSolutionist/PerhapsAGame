using Microsoft.AspNetCore.Mvc;
using PerhapsAGame.Core.Entities;

namespace PerhapsAGame.Services
{
    public interface IScoreService
    {
        Player CreatePlayer(Player player);
        Score CreateScore(Score score);
        IEnumerable<Player> GetPlayers();
        IEnumerable<Score> GetScores();
        Score GetScoreById(int id);
        Score GetScoreByPlayer(Player player);
        Player GetPlayerById(int id);
        Player GetPlayerByScore(Score score);
        Player GetPlayerByName(string name);
        void EditPlayer(int id, Player player);
        void EditScore(int id, Score score);
        void DeletePlayer(int id);
        void DeleteScore(int id);
        bool PlayerExists (int id);
        bool PlayerExists(string name);
        bool ScoreExists(Player player);
        bool GamescoreExists(Player player, string gameName);
        void SaveChanges();
    }
}                                                    