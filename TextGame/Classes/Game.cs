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
            return GetVisibleGameObjects(true).Any(go => go.Name.ToLower() == word.ToLower())
                   || Nouns.ContainsKey(word.ToLower());
        }

        public bool IsPreposition(string word)
        {
            return Prepositions.Contains(word.ToLower());
        }

        public void ExecuteCommands(string shorthand)
        {
            Console.WriteLine($"Shorthand Command: {shorthand}");
        }

        public void ExecuteCommands(string verb, string noun)
        {
            Console.WriteLine($"Normal Command: {verb} and {noun}");
        }

        public void ExecuteCommands(string verb, string noun1, string noun2)
        {
            Console.WriteLine($"Double Command: {verb} and {noun1} with {noun2}");
        }

        private List<GameObject> GetVisibleGameObjects(bool includePlayer)
        {
            var visible = new List<GameObject>();
            visible.AddRange(Player.Items.Where(go => go.Visable));
            visible.AddRange(Player.CurrentRoom.Items.Where(go => go.Visable));
            foreach (GameObject go in visible)
            {
                if (go.Items.Count() > 0)
                    visible.AddRange(go.Items.Where(go => go.Visable));
            }
            visible.Add(Player.CurrentRoom);
            if (includePlayer)
                visible.Add(Player);
            return visible;
        }
    }
}
