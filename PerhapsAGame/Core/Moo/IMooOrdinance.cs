﻿
namespace PerhapsAGame.Core.Moo
{
    public interface IMooOrdinance
    {
        event EventHandler OnGameWonEvent;

        void Initialize();
        void Draw();
        void Update();
        void Exit();
        void CheckWinCondition();
        int[] GenerateTarget();
        void ShowScore();
        void UpdateCharts();
    }
}