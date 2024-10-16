using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    public class GameItem : GameUsable
    {
        public bool Visible { get; set; }
        public bool Movable { get; set; }
        public string NewItem { get; set; }
        public GameContainer Owner { get; set; }

        public GameItem() : base()
        {
            Visible = true;
            NewItem = null;
        }

        public string Drop()
        {
            if (Owner is Player p)
            {
                p.Items.Remove(this);
                Owner = p.CurrentRoom;
                Owner.Items.Add(this);
                return $"You drop the {Name} on the ground.";
            }
            return $"You aren't holding a {Name}.";
        }

        public string Take(Player player)
        {
            if (Owner.Name == player.Name)
                return $"You are allready holding the {Name}.";
            if (player.CurrentRoom.Name != Owner.Name || !Visible)
                return $"You don't see a {Name}.";
            if (!Movable)
                return $"You can't carry the {Name}.";
            Owner.Items.Remove(this);
            Owner = player;
            Owner.Items.Add(this);
            return $"You take the {Name}";
        }

        public override string Use(GameObject go)
        {
            if (go.Name != Key)
                return $"You can't use {go.Name} with {Name}.";

            Consume();
            if (UseText == null)
                return $"{Name} was lost. {NewItem} has been created.";
            return UseText;
        }

        private void Consume()
        {
            if (NewItem == null)
                throw new Exception($"{Name} can't be consumed if NewItem is null.");

            Owner.Items.Remove(this);
            GameItem item = FileHandler.LoadItem(NewItem);
            item.Owner = Owner;
            Owner.Items.Add(item);
        }
    }
}
