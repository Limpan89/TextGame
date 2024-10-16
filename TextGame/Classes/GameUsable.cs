using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    public abstract class GameUsable : GameObject
    {
        public string Key { get; set; }
        public string UseText { get; set; }

        public GameUsable() : base()
        {
            Key = null;
            UseText = null;
        }
    }
}
