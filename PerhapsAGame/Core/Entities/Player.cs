using System.ComponentModel.DataAnnotations;

namespace PerhapsAGame.Core.Entities
{
    public class Player
    {
        public int PlayerId { get; set; }

        [MaxLength(250)]
        public string? Name { get; set; }
        public ICollection<Score> Scores { get; set; } = new List<Score>();
    }
}
