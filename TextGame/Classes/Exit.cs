using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    public class Exit : GameUsable
    {
        public Room Room { get; set; }
        public bool Locked { get; set; }

        public Exit() : base()
        {
            Room = null;
            Locked = false;
        }

        public override string Use(GameObject go)
        {
            if (!Locked)
                return $"The {Name} isn't locked";
            if (go.Name != Key)
                return "Wrong key";
            Locked = false;
            return $"The {Name} has been unlocked with the {Key}.";
        }
    }
}
