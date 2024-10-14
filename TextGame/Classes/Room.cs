using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    public class Room : GameContainer
    {
        public Dictionary<string, Exit> Exits { get; set; }
        public bool EndPoint { get; set; }

        public Room() { }
    }
}
