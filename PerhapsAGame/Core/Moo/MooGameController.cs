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
        private readonly IInputProvider _input;
        private readonly IOutputProvider _output;

        public MooGameController(IPlayerService service,
            IOutputProvider output, IInputProvider input)
        {
            _input = input;
            _output = output;
            _service = service;

        }

        public override void Initialize()
        {


        }
        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }

        public int[] GenerateTarget()
        {
            int[] target = new int[4];

            for (int i = 0; i < target.Length; i++) 
                target[i] = Random.Shared.Next(0, 9);
            return target;
        }


    }



}
