using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    public abstract class GameObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }

        public GameObject()
        {
            Name = null;
            Description = null;
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
