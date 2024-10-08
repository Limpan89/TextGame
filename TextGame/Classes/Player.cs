using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    public class Player : GameObject
    {
        public Room CurrentRoom { get; set; }

        public Player(string name, Room startingRoom) : base(name, true, true) 
        {
            CurrentRoom = startingRoom;
        }
    }
}
