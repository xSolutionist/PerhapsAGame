using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerhapsAGame.Core.GameBase
{
    public class InputProvider : IInputProvider
    {
        private readonly Func<string?> _inputProvider;
        public InputProvider(Func<string?> inputProvider)
        {
            _inputProvider = inputProvider;
        }
        public string Read()
        {
            return _inputProvider();
        }

    }
}
