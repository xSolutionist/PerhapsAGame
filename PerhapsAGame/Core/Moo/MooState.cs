using PerhapsAGame.Core.Entities;
namespace PerhapsAGame.Core.Moo;
public class MooState
{
    public string GameName { get; init; } = "Moo";
    public int[]? Target { get; set; } = new int[4];
    public int[]? Guess { get; set; } = new int[4];
    public Player? Player { get; set; }
    public string? PlayerName { get; set; }
    public int Cows { get; set; } = 0;
    public int Bulls { get; set; } = 0;
    public int Guesses { get; set; } = 0;
    public double GamesPlayed { get; set; } = 0;
    public double CurrentAverage { get; set; } = 0;

}
