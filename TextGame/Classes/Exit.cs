using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    public class Exit : GameObject
    {
        public Room Room { get; set; }
        public bool Locked { get; set; }
        public string Key { get; set; }

        public Exit() { }

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
