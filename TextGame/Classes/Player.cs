using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    public class Player : GameContainer
    {
        public Room CurrentRoom { get; set; }

        public Player() { }

        public override string Examine()
        {
            StringBuilder sb = new StringBuilder($"{Description}\n\n");
            if (Items.Count > 0)
                sb.Append("Inventory: ");
            else
                sb.Append("Your inventory is empty.");
            for (int i = 0; i < Items.Count; i++)
            {
                sb.Append(Items[i].Name);
                if (i + 1 == Items.Count)
                    sb.Append(".\n\n");
                else if (i + 2 == Items.Count)
                    sb.Append(" and ");
                else
                    sb.Append(", ");
            }
            return sb.ToString();
        }
    }
}
