using System.ComponentModel.DataAnnotations;

namespace PerhapsAGame.Core.Entities
{
    public class Score
    {
        public int ScoreId { get; set; }
        public Player Player { get; set; }
        
        [MaxLength(250)]
        public string? GameName { get; set; }

        public int GamesPlayed { get; set; }
        public double AverageScore { get; set; }
        public int Highscore { get; set; }
    }
}