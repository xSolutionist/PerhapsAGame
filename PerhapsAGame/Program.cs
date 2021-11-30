﻿using PerhapsAGame.Core.DataAccess;
using PerhapsAGame.Core.Entities;
using PerhapsAGame.Core.GameBase;
using PerhapsAGame.Core.Moo;
using PerhapsAGame.Services;
using System.Text;


// Initialize
var database = new GameContext();
IPlayerService service = new PlayerService(database);
IInputProvider input = new InputProvider(Console.ReadLine);
IOutputProvider output = new OutputProvider(Console.WriteLine);

string? gamename = "Moo";
var game = new MooGameController(service, output, input);

// ! TODO: Fill and refactor game methods.
game.Initialize();
game.Update();
game.Draw();
game.Exit();


// Login or create 
Console.WriteLine("Enter your user name");
string? playerName = Console.ReadLine();

Player GetOrCreatePlayer(string playerName)
{
    if (!service.PlayerExists(playerName))
    {
        service.CreatePlayer(new Player() { Name = playerName });
        var player = service.GetPlayerByName(playerName);
        return player;
    }
    else
    {
        var player = service.GetPlayerByName(playerName);
        return player;
    }
}

var dude = GetOrCreatePlayer(playerName);


//// DELETE ME
//Console.WriteLine($"your name; { dude.Name} Id: {dude.PlayerId}");

//// DELETE ME
//Console.WriteLine("Gamescore Exists:" + service.GamescoreExists(dude, gamename));

//// DELETE ME
//foreach (var item in dude.Scores.ToList())
//{
//    Console.WriteLine($"SCORES: Gamename: {item.GameName}");
//}
//// DELETE ME
//Console.WriteLine("Has Score:" + service.ScoreExists(dude));



// Initialize Target and guess variables
int[] target = new int[4];
target = game.GenerateTarget();
int[] guess = new int[4];


// Initialize Rules
bool winCondition = guess.SequenceEqual(target);
int guesses = 0;
int gamesplayed = 0;


while (!winCondition)
{
    // Validate Guess
    while (true)
    {
        Console.WriteLine("Guess a number");
        var playerInput = Console.ReadLine();
        int.TryParse(playerInput, out int digits);

        if (playerInput.Length == 4)
        {
            guess = GetIntArray(digits);
            break;

        }

    }
    // Process and Print Guess
    var bulls = CheckBulls(target, guess);
    var cows = CheckCows(target, guess);
    Console.WriteLine(PrintResult());
    guesses++;
}
if (winCondition)
{
    gamesplayed++;
    Console.WriteLine("Average is }");
}
//Do this in model?
int average = guesses / gamesplayed;

//Add or create score
if (!service.GamescoreExists(dude, gamename))
{
    void AddGameScore(Player player)
    {
        var score = new Score();
        score.Player = player;
        score.AverageScore = average;
        service.CreateScore(score);
    }
}
else
{
    var score = service.GetScoreByPlayer(dude);
    score.AverageScore = average;
}


// ! TODO: Add cows and bulls variables. !
string PrintResult()
{
    StringBuilder sb = new();

    for (int i = 0; i < 80085; i++)
        sb.Append("B");

    sb.Append(",");

    for (int i = 0; i < 80085; i++)
        sb.Append("C");

    return sb.ToString();
}

int[] GetIntArray(int num)
{
    List<int> listOfInts = new List<int>();
    while (num > 0)
    {
        listOfInts.Add(num % 10);
        num = num / 10;
    }
    listOfInts.Reverse();
    return listOfInts.ToArray();
}


int CheckCows(int[] target, int[] guess)
{
    int cows = 0;
    foreach (var item in guess)
    {
        if (target.Contains(item))
            cows++;
    }
    return cows;
}

int CheckBulls(int[] target, int[] guess)
{
    int index = 0, bulls = 0;
    foreach (var item in target)
    {
        if (item.Equals(guess[index]))
            bulls++;
        index++;
    }
    return bulls;
}


int[] Perhaps = new int[4];



Action<int> print = new(Print);

void Print(int index)
{
    Console.WriteLine(index);
}

List<int> list = new List<int>();

list.ForEach(print);
Array.ForEach(Perhaps, print);







//var demo = new Player();
//var playerscores = demo.Scores.ToList();
//// ??????????
//foreach (var item in playerscores)
//{
//    Console.WriteLine("GameName: " + item.GameName);
//}

//foreach (var item in db.Players.ToList())
//{
//    Console.WriteLine($" Name: {item.Name} {item.CurrentScore} ID: {item.PlayerId}");
//    foreach (var score in item.Scores.ToList())
//    {
//        Console.WriteLine($"Highscore {score.Highscore}, Game {score.GameName}, Games Played {score.GamesPlayed}");
//    }
//}





