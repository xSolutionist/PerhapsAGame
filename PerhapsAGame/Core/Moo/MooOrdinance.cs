using PerhapsAGame.Core.Entities;
using PerhapsAGame.Core.GameBase;
using PerhapsAGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerhapsAGame.Core.Moo
{

    public class MooOrdinance : Game, IMooOrdinance
    {
        public event EventHandler OnGameWonEvent;
        private readonly MooState s;
        private readonly IInputProvider _input;
        private readonly IOutputProvider _output;
        private readonly IScoreService _service;

        public MooOrdinance(IScoreService service, IInputProvider input, IOutputProvider output, MooState state)
        {
            _service = service;
            _input = input;
            _output = output;
            s = state;
        }
        public void CheckWinCondition()
        {
            if (s.Guess.SequenceEqual(s.Target))
            {
                OnGameWonEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        public override void Initialize()
        {

            OnGameWonEvent += _OnGameWonEvent;
            s.Target = GenerateTarget();
            _output.Write("Enter your user name");
            s.PlayerName = _input.Read();
            s.Player = GetOrCreatePlayer(s.PlayerName);
        }

        public override void Draw()
        {
            // Array.ForEach(s.target, Print);
            while (true)
            {
                Update();
            }
        }

        public override void Update()
        {
            GetGuess();
            s.Bulls = CheckBulls(s.Target, s.Guess);
            s.Cows = CheckCows(s.Target, s.Guess);
            _output.Write(GameState());
            ResetGuessState();
            CheckWinCondition();
        }

        private void _OnGameWonEvent(object? sender, EventArgs e)
        {
            UpdateCharts();
            ShowScore();
            _output.Write("Continue? (y)");
            var answer = _input.Read();
            if (answer == "y")
            {
                Console.Clear();
                s.Target = GenerateTarget();
                Draw();
            }
            Exit();
        }

        public void UpdateCharts()
        {
            s.GamesPlayed++;
            GetCurrentAverage();

            if (!_service.GamescoreExists(s.Player, s.GameName))
                AddGameScore(s.Player);
            else
            {
                var score = _service.GetScoreByPlayer(s.Player);
                UpdatePlayersAverage(score);
            }

        }

        private void UpdatePlayersAverage(Score score)
        {
            var newAverage = (score.AverageScore + s.CurrentAverage) / 2;
            score.AverageScore = newAverage;
            score.GamesPlayed++;
            _service.EditScore(score.ScoreId, score);
        }

        private void ResetGuessState()
        {
            s.Bulls = 0;
            s.Cows = 0;
        }

        void AddGameScore(Player player)
        {
            var score = new Score();
            score.Player = player;
            score.GameName = s.GameName;
            score.AverageScore = s.CurrentAverage;
            score.GamesPlayed = 1;
            _service.CreateScore(score);
        }


        public void ShowScore()
        {
            string[] headers = new string[] { nameof(Player.Name), nameof(Score.GamesPlayed), nameof(Score.AverageScore) };
            string header = "";
            foreach (var item in headers)
            {
                header += item.PadLeft(20);
            }

            _output.Write(header);

            var scores = _service.GetScores().OrderByDescending(s => s.AverageScore).ToList();
            foreach (var item in scores)
            {
                var player = _service.GetPlayerById(item.Player.PlayerId);
                Console.WriteLine($"{player.Name,20} {item.GamesPlayed,19} {item.AverageScore,18}");
            }
        }


        private void GetCurrentAverage()
        {
            s.CurrentAverage = s.Guesses / s.GamesPlayed;
            s.GamesPlayed--;
        }

        private void GetGuess()
        {
            while (true)
            {
                Array.ForEach(s.Target, Print);
                Console.WriteLine("Guess a number");
                var input = _input.Read();
                if (input.Length == 4)
                {
                    s.Guess = GetIntArray(input);
                    s.Guesses++;
                    break;
                }
            }
        }

        public int[] GenerateTarget()
        {
            for (int i = 0; i < s.Target.Length; i++)
                s.Target[i] = Random.Shared.Next(0, 9);
            return s.Target;
        }
        string GameState()
        {
            StringBuilder sb = new();

            for (int i = 0; i < s.Bulls; i++)
                sb.Append("B");

            sb.Append(",");

            for (int i = 0; i < s.Cows; i++)
                sb.Append("C");
            return sb.ToString();
        }

        Player GetOrCreatePlayer(string playerName)
        {
            if (!_service.PlayerExists(playerName))
            {
                _service.CreatePlayer(new Player() { Name = playerName });
                var NewPlayer = _service.GetPlayerByName(playerName);
                return NewPlayer;
            }

            var Existingplayer = _service.GetPlayerByName(playerName);
            return Existingplayer;
        }
        void Print(int index)
        {
            _output.Write(index.ToString());
        }


        int[] GetIntArray(string num)
        {
            return num.Select(n => Convert.ToInt32(n) - 48).ToArray();
        }

        int CheckCows(int[] target, int[] guess)
        {
            foreach (var item in guess)
            {
                if (target.Contains(item))
                    s.Cows++;
            }
            return s.Cows - s.Bulls;
        }

        int CheckBulls(int[] target, int[] guess)
        {
            int index = 0;
            foreach (var item in target)
            {
                if (item.Equals(guess[index]))
                    s.Bulls++;
                index++;
            }
            return s.Bulls;
        }
        public override void Exit()
        {
            Environment.Exit(0);
        }
    }
}
