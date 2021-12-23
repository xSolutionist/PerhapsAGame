using PerhapsAGame.Core.DataAccess;
using PerhapsAGame.Core.Moo;
using PerhapsAGame.Services;
using PerhapsAGame.Core.GameBase;



GameMenu();


void GameMenu()
{
    Dictionary<string, Action> gameLibrary = new();
    gameLibrary.Add("Moo", BuildAndRunConsoleMoo);
    gameLibrary.Add("Something", BuildAndRunRhymeMaze);


    foreach (var item in gameLibrary.Keys)
        Console.WriteLine(item);
    var command = Console.ReadLine();

    if (gameLibrary.ContainsKey(command))
        gameLibrary[command]();

}
void BuildAndRunRhymeMaze() { }
void BuildAndRunConsoleMoo()
{

    var database = new SQLiteContext();
    IScoreService service = new ScoreService(database);
    MooState state = new();

    IInputProvider input = new InputProvider(Console.ReadLine);
    IOutputProvider output = new OutputProvider(Console.WriteLine);

    IGameManager logic = new GameManager(service, input, output, state);
    var game = new MooGameController(logic);
    game.StartMoo();
}

