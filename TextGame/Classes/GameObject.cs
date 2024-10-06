using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    public class GameObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<GameObject> Items { get; set; }
        public bool Visable { get; set; }
        public bool Movable { get; set; }

        public GameObject(string name, bool visable, bool movable)
        {
            Name = name;
            Visable = visable;
            Movable = movable;
            Items = new List<GameObject>();
        }
    }
}
