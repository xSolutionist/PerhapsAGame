using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using PerhapsAGame.Core.DataAccess;
using PerhapsAGame.Core.Moo;
using PerhapsAGame.Services;
using System.Security.Cryptography;
using PerhapsAGame.Core.GameBase;
using Microsoft.EntityFrameworkCore;

GameMenu();

void GameMenu()
{
    Dictionary<string, Action> gameLibrary = new();
    gameLibrary.Add("Moo", BuildAndRunConsoleMoo);
    gameLibrary.Add("Something", BuildAndRunRhymeMaze);


    foreach (var item in gameLibrary.Keys)
    {
        Console.WriteLine(item);
    }

    string[] choices = gameLibrary.Keys.ToArray();
    var command = Console.ReadLine();
    if (gameLibrary.ContainsKey(command))
    {
        gameLibrary[command]();
    }

}
void BuildAndRunRhymeMaze() { }
void BuildAndRunConsoleMoo()
{

    var database = new SQLiteContext();
    IScoreService service = new ScoreService(database);
    MooState state = new();

    IInputProvider input = new InputProvider(Console.ReadLine);
    IOutputProvider output = new OutputProvider(Console.WriteLine);

    IMooOrdinance ordinance = new MooOrdinance(service, input, output, state);
    var game = new MooGameController(ordinance);
    game.StartMoo();
}

var summary = BenchmarkRunner.Run<Benchmark>();
public class Benchmark
{
    int randomnumber1 = new();
    int randomnumber2 = new();

    [Benchmark]
    public void CalculateGoonger()
    {
        randomnumber1 = Random.Shared.Next(0, 10000);
        randomnumber2 = Random.Shared.Next(0, 1000);

        var result = randomnumber1 * randomnumber2;
    }
    [Benchmark]
    public void CalculatePlus()
    {
        randomnumber1 = Random.Shared.Next(0, 10000);
        randomnumber2 = Random.Shared.Next(0, 1000);

        var result = randomnumber1 + randomnumber2;
    }

    private const int N = 10000;
    private readonly byte[] data;

    private readonly SHA256 sha256 = SHA256.Create();
    private readonly MD5 md5 = MD5.Create();


    public Benchmark()
    {
        data = new byte[N];
        new Random(42).NextBytes(data);
    }


    [Benchmark]
    public byte[] Sha256() => sha256.ComputeHash(data);

    [Benchmark]
    public byte[] Md5() => md5.ComputeHash(data);
}






