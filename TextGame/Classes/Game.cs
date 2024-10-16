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
                output = $"I don't understand \"{verb}\"";
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
                output = $"I don't understand \"{noun}\"";
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
                output = $"I don't understand \"{verb}\"";
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
            Player.CurrentRoom = Player.CurrentRoom.Go(direction, out string output);
            return output;
        }

        private string Take(GameItem item)
        {
            return item.Take(Player);
        }

        private string Drop(GameItem item)
        {
            return item.Drop();
        }

        private string Use(GameItem item, GameObject go)
        {
            return go.Use(item);
        }

        private List<GameObject> GetVisibleItems()
        {
            var visible = new List<GameObject>();
            visible.AddRange(Player.Items.Where(go => go.Visible));
            visible.AddRange(Player.CurrentRoom.Items.Where(go => go.Visible));
            visible.AddRange(Player.CurrentRoom.Exits.Values.Where(v => v.Locked));
            return visible;
        }
    }
}
