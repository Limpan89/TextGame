using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    public class GameItem : GameObject
    {
        public bool Visable { get; set; }
        public bool Movable { get; set; }
        public string Key { get; set; }
        public string NewItem { get; set; }
        public string UseText { get; set; }
        public override string Use(GameObject go)
        {
            if (go.Name != Key)
                return $"You can't use {go.Name} with {Name}.";
            return UseText;
        }

        public getNewItem()
    }
}
