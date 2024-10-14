using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    public class Player : GameContainer
    {
        public Room CurrentRoom { get; set; }

        public Player() { }
    }
}
