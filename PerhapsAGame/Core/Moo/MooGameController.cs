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
    public class MooGameController : Game
    {
        private readonly IPlayerService _service;
        private readonly IInputProvider input = new InputProvider(Console.ReadLine);
        private readonly IOutputProvider output = new OutputProvider(Console.WriteLine);
        private MooState s = new();
        
        public MooGameController(IPlayerService service)
        {
            _service = service;
        }

        public override void Initialize()
        {
            s.target = GenerateTarget();
            output.Write("Enter your user name");
            s.playerName = input.Read();
            s.player = GetOrCreatePlayer(s.playerName);
        }

        public override void Draw()
        {

            Array.ForEach(s.target, Print);

            while (true)
            {

                GetGuess();
                s.guesses++;
                s.bulls = CheckBulls(s.target, s.guess);
                s.cows = CheckCows(s.target, s.guess);
                output.Write(GameState());
                ResetGuessState();

                if (s.guess.SequenceEqual(s.target))
                    break;
            }

            s.gamesPlayed++;
            GetCurrentAverage();

            if (!_service.GamescoreExists(s.player, s.gameName))
                AddGameScore(s.player);
            else
            {
                var score = _service.GetScoreByPlayer(s.player);

                UpdatePlayersAverage(score);

                _service.EditScore(score.ScoreId, score);
            }
        }

        private void UpdatePlayersAverage(Score score)
        {
            var newAverage = (score.AverageScore + s.currentAverage) / 2;
            score.AverageScore = newAverage;
        }

        private void ResetGuessState()
        {
            s.bulls = 0;
            s.cows = 0;
        }

        void AddGameScore(Player player)
       {
           var score = new Score();
           score.Player = player;
           score.GameName = s.gameName;
           score.AverageScore = s.currentAverage;
           _service.CreateScore(score);
       }

        public override void Update()
        {

        }

        public override void Exit()
        {

        }
        private void GetCurrentAverage()
        {
            s.currentAverage = s.guesses / s.gamesPlayed;
        }

        private void GetGuess()
        {
            while (true)
            {
                Console.WriteLine("Guess a number");
                var playerInput = input.Read();
                if (playerInput.Length == 4)
                {
                    s.guess = GetIntArray(playerInput);
                    break;
                }
            }
        }

        public int[] GenerateTarget()
        {
            int[] target = new int[4];
            for (int i = 0; i < target.Length; i++)
                target[i] = Random.Shared.Next(0, 9);
            return target;
        }
        string GameState()
        {
            StringBuilder sb = new();

            for (int i = 0; i < s.bulls; i++)
                sb.Append("B");

            sb.Append(",");

            for (int i = 0; i < s.cows; i++)
                sb.Append("C");
            return sb.ToString();
        }

        Player GetOrCreatePlayer(string playerName)
        {
            if (!_service.PlayerExists(playerName))
            {
                _service.CreatePlayer(new Player() { Name = playerName });
                var player = _service.GetPlayerByName(playerName);
                return player;
            }
            else
            {
                var player = _service.GetPlayerByName(playerName);
                return player;
            }
        }
        void Print(int index)
        {
            output.Write(index.ToString());
        }

        int[] GetIntArray(string num)
        {
            return num.Select(n => Convert.ToInt32(n) -48).ToArray();
        }

        int CheckCows(int[] target, int[] guess)
        {
            foreach (var item in guess)
            {
                if (target.Contains(item))
                    s.cows++;
            }
            return s.cows - s.bulls;
        }

        int CheckBulls(int[] target, int[] guess)
        {
            int index = 0;
            foreach (var item in target)
            {
                if (item.Equals(guess[index]))
                    s.bulls++;
                index++;
            }
            return s.bulls;
        }
    }




}
