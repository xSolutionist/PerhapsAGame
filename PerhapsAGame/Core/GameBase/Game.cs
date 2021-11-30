using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerhapsAGame.Core.GameBase
{
    public abstract class Game
    {
        public abstract void Initialize();
        public abstract  void Draw();
        public abstract void Update();
        public abstract  void Exit();
    }
}
