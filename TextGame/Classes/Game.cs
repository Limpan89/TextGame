using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    public class Game
    {
        public string Intro { get; set; }
        public string Outro { get; set; }
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
            return ExecuteCommands(Shorthands[shorthand].Item1, Shorthands[shorthand].Item2, out output);
        }

        public bool ExecuteCommands(string verb, string noun, out string output)
        {
            if (!Verbs.TryGetValue(verb.ToLower(), out string cmdVerb))
            {
                output = $"I don't know the word {verb}";
                return !Player.CurrentRoom.EndPoint;
            }
            GameObject go = null;
            string cmdNoun = null; ;
            if (Nouns.TryGetValue(noun.ToLower(), out cmdNoun))
            {
                if (cmdNoun[0] == '*')
                {
                    switch(cmdNoun)
                    {
                        case "*player":
                            go = Player;
                            break;
                        case "*room":
                            go = Player.CurrentRoom;
                            break;
                    }
                }
            }
            if (cmdNoun == null)
                go = GetVisibleItems().Where(go => go.Name.ToLower() == noun.ToLower()).SingleOrDefault();

            if (go == null && cmdNoun == null)
            {
                output = $"I don't know the word {noun}";
                return !Player.CurrentRoom.EndPoint;
            }


            output = "You can't do that.";
            switch (cmdVerb)
            {
                case "take":
                    if (go != null && go is GameItem take)
                        output = Take(take);
                    break;
                case "drop":
                    if (go != null && go is GameItem drop)
                        output = Drop(drop);
                    break;
                case "examine":
                    if (go != null)
                        output = Examine(go);
                    break;
                case "go":
                    if (cmdNoun != null)
                        output = Go(cmdNoun);
                    break;
                case "use":
                    output = $"I don't understand what you want to use {noun} with?";
                    break;
            }
            return !Player.CurrentRoom.EndPoint;
        }

        public bool ExecuteCommands(string verb, string noun1, string noun2, out string output)
        {
            if (!Verbs.TryGetValue(verb.ToLower(), out string cmdVerb))
            {
                output = $"I don't know the word {verb}";
                return !Player.CurrentRoom.EndPoint;
            }
            if (cmdVerb != "use")
            {
                output = "You can't do that";
                return !Player.CurrentRoom.EndPoint;
            }
            output = "You can't do that";
            GameObject go = GetVisibleItems().Where(go => go.Name.ToLower() == noun1.ToLower()).SingleOrDefault();
            if (go != null && go is GameItem gi)
            {
                go = GetVisibleItems().Where(go => go.Name.ToLower() == noun2.ToLower()).SingleOrDefault();
                if (go != null)
                    output = Use(gi, go);
            }
            return !Player.CurrentRoom.EndPoint;
        }

        private string Examine(GameObject go)
        {
            return go.Examine();
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

        private string Use(GameItem gameItem, GameObject go)
        {
            string output = go.Use(gameItem);
            if (go is GameItem gi && gi.Key != null && gi.Key == gameItem.Name)
            {
                if (!Player.Items.Remove(gi))
                    Player.CurrentRoom.Items.Remove(gi);
                Player.Items.Add(FileHandler.LoadItem(gi.NewItem));
            }
            return output ;
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
