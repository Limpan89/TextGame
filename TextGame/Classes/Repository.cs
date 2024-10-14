using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace TextGame.Classes
{
    public static class Repository
    {
        private static readonly string _pathGame = @"C:\Users\lbrob\source\repos\TextGame\TextGame\Data\game_data.json";
        private static readonly string _pathItems = @"C:\Users\lbrob\source\repos\TextGame\TextGame\Data\game_items.json";
        private static readonly string _pathVocabulary = @"C:\Users\lbrob\source\repos\TextGame\TextGame\Data\game_vocabulary.json";

        public static Game LoadGame()
        {
            Game game = new Game();

            using (StreamReader reader = new StreamReader(_pathVocabulary))
            {
                string gameJSON = reader.ReadToEnd();
                game = JsonSerializer.Deserialize<Game>(gameJSON);
            }
            var data = LoadGameItems();
            game.Player = data.Item1;
            game.Player.CurrentRoom = data.Item2.Where(r => r.Name == data.Item3[0]).SingleOrDefault();
            for (int i = 2; i < data.Item2.Count(); i++)
            {
                var exitData = data.Item3[i].Split('*');
                data.Item2.Where(r => r.Name == exitData[0]).SingleOrDefault().Exits[exitData[1]].Room
                    = data.Item2.Where(r => r.Name == exitData[2]).SingleOrDefault();
            }
            return game;
        }

        private static Tuple<Player, List<Room>, List<string>> LoadGameItems()
        {
            Tuple<Player, List<Room>, List<string>> data;

            using (StreamReader reader = new StreamReader(_pathGame))
            {
                string gameJSON = reader.ReadToEnd();
                data = JsonSerializer.Deserialize<Tuple<Player, List<Room>, List<string>>>(gameJSON);
            }
            return data;
            
        }

        public static GameItem LoadItem(string name)
        {
            GameItem gameItem;
            using (StreamReader reader = new StreamReader(_pathItems))
            {
                string itemJSON = reader.ReadToEnd();
                gameItem = JsonSerializer.Deserialize<List<GameItem>>(itemJSON).Where(gi => gi.Name == name).SingleOrDefault();
            }
            return gameItem;
        }
    }
}
