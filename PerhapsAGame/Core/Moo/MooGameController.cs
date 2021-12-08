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
    public class MooGameController
    {
        private readonly IMooOrdinance _ordinance;

        public MooGameController(IMooOrdinance ordinance)
        {
            _ordinance = ordinance;
        }

        public void StartMoo() 
        {
            _ordinance.Initialize();
            _ordinance.Draw();
        }

        public void Exit() 
        {
           _ordinance.Exit();
        }
      
    }
       

}
