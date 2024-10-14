using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    public class Game
    {
        public Dictionary<string, Tuple<string, string>> Shorthands { get; set; }
        public Dictionary<string, string> Verbs { get; set; }
        public Dictionary<string, string> Nouns { get; set; }
        public List<string> Prepositions { get; set; }
        public Player Player { get; set; }

        public Game() { }

        public bool IsShorthand(string word)
        {
            return Shorthands.ContainsKey(word.ToLower());
        }

        public bool IsVerb(string word)
        {
            return Verbs.ContainsKey(word.ToLower());
        }

        public bool IsNoun(string word)
        {
            return GetVisibleItems().Any(go => go.Name.ToLower() == word.ToLower())
                   || Nouns.ContainsKey(word.ToLower());
        }

        public bool IsPreposition(string word)
        {
            return Prepositions.Contains(word.ToLower());
        }

        public bool ExecuteCommands(string shorthand, out string output)
        {
            bool run = ExecuteCommands(Shorthands[shorthand].Item1, Shorthands[shorthand].Item2, out string text);
            output = text;
            return run;
        }

        public bool ExecuteCommands(string verb, string noun, out string output)
        {
            if 
            return true;
        }

        public bool ExecuteCommands(string verb, string noun1, string noun2, out string output)
        {
            return true;
        }

        private string Examine(GameObject go)
        {
            return go.Description;
        }

        private string Go(string direction)
        {
            if (!Player.CurrentRoom.Exits.TryGetValue(direction, out Exit exit))
                return $"You can't travel {direction}.";

            if (exit.Locked)
                return $"The way {direction} is blocked by a {exit.Name}, you need to find a key.";

            Player.CurrentRoom = exit.Room;
            return $"You travel {direction}.\n\n{Player.CurrentRoom.Name}\n\n{Player.CurrentRoom.Description}";
        }

        private string Take(GameItem gameItem)
        {
            if (Player.Items.Contains(gameItem))
                return $"You are allready holding the {gameItem.Name}";
            if (!gameItem.Movable)
                return $"You can't move the {gameItem.Name}.";

            if (Player.CurrentRoom.Items.Contains(gameItem) && Player.CurrentRoom.Items.Remove(gameItem))
                Player.Items.Add(gameItem);
            else
                return $"You can't find {gameItem.Name}";
            return $"You take the {gameItem.Name} and place it in your inventory.";
        }

        private string Drop(GameItem gameItem)
        {
            if (!Player.Items.Contains(gameItem))
                return $"You aren't holding the {gameItem.Name}";
            if (!gameItem.Movable)
                return $"You can't drop the {gameItem.Name}.";

            if (Player.Items.Contains(gameItem) && Player.Items.Remove(gameItem))
                Player.CurrentRoom.Items.Add(gameItem);
            else
                return $"You can't find {gameItem.Name}";
            return $"You take the {gameItem.Name} and place it in your inventory.";
        }

        private string Use(GameItem gameItem)
        {

        }

        private string Use(GameItem gameItem, GameObject go)
        {

        }

        private List<GameObject> GetVisibleItems()
        {
            var visible = new List<GameObject>();
            visible.AddRange(Player.Items.Where(go => go.Visable));
            visible.AddRange(Player.CurrentRoom.Items.Where(go => go.Visable));
            visible.AddRange(Player.CurrentRoom.Exits.Values.Where(v => v.Locked));
            return visible;
        }
    }
}
