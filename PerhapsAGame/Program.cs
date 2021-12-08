using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using PerhapsAGame.Core.DataAccess;
using PerhapsAGame.Core.Moo;
using PerhapsAGame.Benchies;
using PerhapsAGame.Services;
using System.Security.Cryptography;
using PerhapsAGame.Core.Entities;
using PerhapsAGame.Core.GameBase;



//                       BENCHMARK TESTS
//var summary = BenchmarkRunner.Run<Benchmark>();
//public class Benchmark
//{

//    int randomnumber1 = new();
//    int randomnumber2 = new();

//    [Benchmark]
//    public void CalculateGoonger() 
//    {
//        randomnumber1 = Random.Shared.Next(0, 10000);
//        randomnumber2 = Random.Shared.Next(0, 1000);

//        var result = randomnumber1 * randomnumber2;
//    }
//    [Benchmark]
//    public void CalculatePlus()
//    {
//        randomnumber1 = Random.Shared.Next(0, 10000);
//        randomnumber2 = Random.Shared.Next(0, 1000);

//        var result = randomnumber1 + randomnumber2;
//    }

//}


//                   SCORE PLAYER TESTS.
var databases = new SQLiteContext();
IScoreService service = new ScoreService(databases);

var scores = service.GetScores().OrderByDescending(s => s.AverageScore);
foreach (var score in scores)
{
    Console.WriteLine($"{score.Player.Name,20} {score.GamesPlayed,19} {score.AverageScore,18}");
}

//{
//    Console.WriteLine("From score to players:?");

//    Console.WriteLine($"Gamename:{ item.GameName } ScoreAverage {item.AverageScore} Playername; {item.Player.PlayerId} ");
//}


//var players = service.GetPlayers().ToList();

//string headerss = null;


//string[] headers = new string[] { "Name", "GamesPlayed", "Average" };

//foreach (var item in headers)
//{
//   headerss += item.PadLeft(20);
//}
//Console.WriteLine(headerss);







//    Console.WriteLine("should be isac, Isac, jurgen.");
//var sortedlist = new List<Score>();

//foreach (var item in players)
//{
//    var score = service.GetScoreByPlayer(item);
//    Console.WriteLine($"PlayerName: {item.Name}");
//}

//var players1 = service.GetPlayers().ToList();



//foreach (var item in players)
//{
//    Console.WriteLine(item.Name + "Name and ID:" + item.PlayerId);
//    var scoree = service.GetScoreByPlayer(item);
//    Console.WriteLine("Average + Gamesplayer" + scoree.AverageScore + scoree.GamesPlayed);
//    Console.WriteLine("From score to playername:" + scoree.Player.Name + scoree.Player.Scores.FirstOrDefault().GameName);
//}

//var player = service.GetPlayerByName("Isac");
//Console.WriteLine(player.Name + "Name and ID:" + player.PlayerId);
//var score = service.GetScoreByPlayer(player);
//Console.WriteLine(score.AverageScore);



Dictionary<string, Action> gameLibrary = new();
gameLibrary.Add("Moo", BuildAndStartMoo);
gameLibrary.Add("Something", StartSomething);

foreach (var item in gameLibrary.Keys)
{
    Console.WriteLine(item);
}

var command = Console.ReadLine();

if (gameLibrary.ContainsKey(command))
{
    gameLibrary[command]();
}


void StartSomething()
{
    Console.WriteLine("oo");
}

void BuildAndStartMoo()
{
    var database = new SQLiteContext();
    IScoreService service = new ScoreService(database);
    IInputProvider input = new InputProvider(Console.ReadLine);
    IOutputProvider output = new OutputProvider(Console.WriteLine);
    MooState state = new();
    IMooOrdinance ordinance = new MooOrdinance(service, input, output, state);
    var game = new MooGameController(ordinance);
    game.StartMoo();
}


// /////////////////////////////////////////////////////// TEST

//private const int N = 10000;
//private readonly byte[] data;

//private readonly SHA256 sha256 = SHA256.Create();
//private readonly MD5 md5 = MD5.Create();


//public Benchmark()
//{
//    data = new byte[N];
//    new Random(42).NextBytes(data);
//}


//[Benchmark]
//public byte[] Sha256() => sha256.ComputeHash(data);

//[Benchmark]
//public byte[] Md5() => md5.ComputeHash(data);



// ---------------------- INITIAL -----------------------
//// Login or create 
//Console.WriteLine("Enter your user name");
//string? playerName = Console.ReadLine();

//Player GetOrCreatePlayer(string playerName)
//{
//    if (!service.PlayerExists(playerName))
//    {
//        service.CreatePlayer(new Player() { Name = playerName });
//        var player = service.GetPlayerByName(playerName);
//        return player;
//    }
//    else
//    {
//        var player = service.GetPlayerByName(playerName);
//        return player;
//    }
//}

//var dude = GetOrCreatePlayer(playerName);


////// DELETE ME
////Console.WriteLine($"your name; { dude.Name} Id: {dude.PlayerId}");

////// DELETE ME
////Console.WriteLine("Gamescore Exists:" + service.GamescoreExists(dude, gamename));

////// DELETE ME
////foreach (var item in dude.Scores.ToList())
////{
////    Console.WriteLine($"SCORES: Gamename: {item.GameName}");
////}
////// DELETE ME
////Console.WriteLine("Has Score:" + service.ScoreExists(dude));



//// Initialize Target and guess variables
//int[] target = new int[4];
//target = game.GenerateTarget();
//int[] guess = new int[4];
//int bulls = 0, cows = 0;

//Array.ForEach(target, Print);
//// Initialize Rules
//int guesses = 0;
//int gamesplayed = 0;

//while (true)
//{

//    cows = 0;
//    bulls = 0;
//    // Validate Guess
//    while (true)
//    {
//        Console.WriteLine("Guess a number");
//        var playerInput = Console.ReadLine();
//        int.TryParse(playerInput, out int digits);

//        if (playerInput.Length == 4)
//        {
//            guess = GetIntArray(digits);
//            break;

//        }

//    }

//    // Process and Print Guess
//    bulls = CheckBulls(target, guess);
//    cows = CheckCows(target, guess);
//    Console.WriteLine(PrintResult());
//    guesses++;
//    if (guess.SequenceEqual(target))
//        break;
//}
//  // GET STATS
//    gamesplayed++;
//    Console.WriteLine("Average is }");
////Do this in model?
//int average = guesses / gamesplayed;

////Add or create score
//if (!service.GamescoreExists(dude, gamename))
//{
//    void AddGameScore(Player player)
//    {
//        var score = new Score();
//        score.Player = player;
//        score.AverageScore = average;
//        service.CreateScore(score);
//    }
//}
//else
//{
//    var score = service.GetScoreByPlayer(dude);
//    score.AverageScore = average;
//}


//string PrintResult()
//{
//    StringBuilder sb = new();

//    for (int i = 0; i < bulls; i++)
//        sb.Append("B");

//    sb.Append(",");

//    for (int i = 0; i < cows; i++)
//        sb.Append("C");

//    return sb.ToString();
//}
//// ! TODO: Add cows and bulls variables. !

//int[] GetIntArray(int num)
//{
//    List<int> listOfInts = new List<int>();
//    while (num > 0)
//    {
//        listOfInts.Add(num % 10);
//        num = num / 10;
//    }
//    listOfInts.Reverse();
//    return listOfInts.ToArray();
//}


//int CheckCows(int[] target, int[] guess)
//{
//    int cows = 0;
//    foreach (var item in guess)
//    {
//        if (target.Contains(item))
//            cows++;
//    }
//    return cows-bulls;
//}

//int CheckBulls(int[] target, int[] guess)
//{
//    int index = 0, bulls = 0;
//    foreach (var item in target)
//    {
//        if (item.Equals(guess[index]))
//            bulls++;
//        index++;
//    }
//    return bulls;
//}

//Action<int> print = new(Print);

//Array.ForEach(target, GetBulls);

//void GetBulls(int item)
//{
//    int index = 0;
//    int bulls = 0;
//    if (item.Equals(guess[index]))
//        bulls++;
//    index++;
//}



//void Print(int index)
//{
//    Console.WriteLine(index);
//}

//Array.ForEach(target, GetBulls);
//List<int> list = new List<int>();

//list.ForEach(GetBulls);

//list.ForEach(Print);

//Array.ForEach(target, print);

// ---------------------- INITIAL ------------------------







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





