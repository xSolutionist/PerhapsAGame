using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerhapsAGame.Core.GameBase
{
    public class OutputProvider : IOutputProvider
    {
        private readonly Action<string> _outputProvider;
        public OutputProvider(Action<string> outputProvider)
        {
            _outputProvider = outputProvider;
        }
        public void Write(string message)
        {
            _outputProvider(message);
        }
    }
}
