using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    public class Game
    {
        public List<string> Verbs { get; set; }
        public List<string> Nouns { get; set; }
        public List<string> Prepositions { get; set; }
        public Player Player { get; set; }

        public bool IsVerb(string word)
        {
            return Verbs.Contains(word.ToLower());
        }

        public bool IsNoun(string word)
        {
            return GetVisibleGameObjects(true).Any(go => go.Name.ToLower() == word.ToLower())
                   || Nouns.Contains(word.ToLower());
        }

        public bool IsPreposition(string word)
        {
            return Prepositions.Contains(word.ToLower());
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
