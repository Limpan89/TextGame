using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    [JsonDerivedType(typeof(GameObject), typeDiscriminator: "base")]
    [JsonDerivedType(typeof(Room), typeDiscriminator: "room")]
    [JsonDerivedType(typeof(Player), typeDiscriminator: "player")]
    [JsonDerivedType(typeof(Exit), typeDiscriminator: "exit")]
    public abstract class GameObject
    {
        public Dictionary<string, Func<Game, string>> Funcs { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public GameObject()
        {
            Funcs = new Dictionary<string, Func<Game, string>>();
            Func<Game, string> func = (Game game) => { return ""; };
        }

        public virtual string Examine()
        {
            return Description;
        }

        public virtual string Use(GameObject game)
        {
            return "Nothing happens";
        }
    }
}
