using PerhapsAGame.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerhapsAGame.Core.Moo
{
    public class MooState
    {
        public string gameName { get; init; } = "Moo";
        public int[]? target { get; set; } = new int[4];
        public int[]? guess { get; set; } = new int[4];
        public Player? player { get; set; }
        public string? playerName { get ; set; }
        public int cows { get; set; }  = 0;
        public int bulls { get; set; } = 0;
        public int guesses { get; set; } = 0;
        public double gamesPlayed { get; set; } = 0; 
        public double currentAverage { get; set; } = 0;

    }
}
