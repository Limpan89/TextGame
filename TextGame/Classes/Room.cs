using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    public class Room : GameObject
    {
        public List<GameObject> Exits { get; set; }

        public Room(string name) : base(name, true, false)
        {
            Exits = new List<GameObject>();
        }
    }
}
