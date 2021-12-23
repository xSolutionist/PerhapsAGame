namespace PerhapsAGame.Core.Moo;
public class MooGameController
{
    private readonly IGameManager _logic;

    public MooGameController(IGameManager logic)
    {
        _logic = logic;
    }

    public void StartMoo()
    {
        _logic.Initialize();
        _logic.Draw();
    }

    public void Exit()
    {
        _logic.Exit();
    }

}
