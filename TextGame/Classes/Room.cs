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

        public Room() : base()
        {
            Exits = new Dictionary<string, Exit>();
            EndPoint = false;
        }

        public Room Go(string direction, out string output)
        {
            if (!Exits.TryGetValue(direction, out Exit exit))
            {
                output = $"You can't travel {direction}.";
                return this;
            }
            if (exit.Locked)
            {
                output = $"The way {direction} is blocked by a {exit.Name}";
                return this;
            }
            output = $"You travel {direction}.\n\n{exit.Room.Enter()}";
            return exit.Room;
        }

        public string Enter()
        {
            string lines = "==============================================================================";
            return $"{lines}\n{Name}\n{lines}\n\n{Examine()}";
        }

        public override string Examine()
        {
            StringBuilder sb = new StringBuilder($"{Description}\n\n");
            if (Items.Count > 0)
                sb.Append("You can see ");
            for (int i = 0; i < Items.Count; i++)
            {
                sb.Append($"a {Items[i].Name}");
                if (i + 1 == Items.Count)
                    sb.Append(".\n\n");
                else if (i + 2 == Items.Count)
                    sb.Append(" and ");
                else
                    sb.Append(", ");
            }
            if (Exits.Values.Count > 0)
                sb.Append("You can see ");
            int n = 0;
            foreach (var kvp in Exits)
            {
                if (kvp.Value.Locked)
                    sb.Append($"a {kvp.Value.Name} blocks the way {kvp.Key}");
                else
                    sb.Append($"a way {kvp.Key}");
                if (n + 1 == Exits.Count)
                    sb.Append(".");
                else if (n + 2 == Exits.Count)
                    sb.Append(" and ");
                else
                    sb.Append(", ");
                n++;
            }
            return sb.ToString();
        }
    }
}
