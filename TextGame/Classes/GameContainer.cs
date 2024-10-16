using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    public abstract class GameContainer : GameObject
    {
        public List<GameItem> Items { get; set; }

        public GameContainer() : base()
        {
            Items = new List<GameItem>();
        }
    }
}
