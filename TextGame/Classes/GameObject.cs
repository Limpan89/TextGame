using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    [JsonDerivedType(typeof(GameObject), typeDiscriminator: "base")]
    [JsonDerivedType(typeof(Room), typeDiscriminator: "room")]
    [JsonDerivedType(typeof(Player), typeDiscriminator: "player")]
    [JsonDerivedType(typeof(Exit), typeDiscriminator: "exit")]
    public class GameObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<GameObject> Items { get; set; }
        public bool Visable { get; set; }
        public bool Movable { get; set; }

        public GameObject() { }
    }
}
