using System.IO;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace TextGame.Classes
{
    public static class FileHandler
    {
        private static readonly string _pathGame = @".\Data\game_data.json";
        private static readonly string _pathItems = @".\Data\game_items.json";
        private static readonly string _pathVocabulary = @".\Data\game_vocabulary.json";

        public static Game LoadGame()
        {
            Game game = LoadData<Game>(_pathVocabulary);
            var data = LoadData<Tuple<Player, List<Room>, List<string>>>(_pathGame);
            game.Player = data.Item1;
            game.Player.CurrentRoom = data.Item2.Where(r => r.Name == data.Item3[0]).SingleOrDefault();
            for (int i = 1; i < data.Item3.Count(); i++)
            {
                var exitData = data.Item3[i].Split('*');
                data.Item2.Where(r => r.Name == exitData[0]).SingleOrDefault().Exits[exitData[1]].Room
                    = data.Item2.Where(r => r.Name == exitData[2]).SingleOrDefault();
            }
            data.Item1.Items.ForEach(i => i.Owner = data.Item1);
            foreach (Room r in data.Item2)
                r.Items.ForEach(i => i.Owner = r);
            return game;
        }

        public static GameItem LoadItem(string name)
        {
            if (!File.Exists(_pathItems))
                throw new FileNotFoundException($"The file at \"{_pathItems}\" could not be found.");
            GameItem gameItem;
            using (StreamReader reader = new StreamReader(_pathItems))
            {
                string dataJSON = reader.ReadToEnd();
                gameItem = JsonSerializer.Deserialize<List<GameItem>>(dataJSON).Where(gi => gi.Name == name).SingleOrDefault();
            }
            if (gameItem == null)
                throw new ArgumentException($"No item with the name {name} could be found.");
            return gameItem;
        }

        private static T LoadData<T>(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"The file at \"{path}\" could not be found.");
            T data;
            using (StreamReader reader = new StreamReader(path))
            {
                string dataJSON = reader.ReadToEnd();
                data = JsonSerializer.Deserialize<T>(dataJSON);
            }
            return data;
        }

    }
}
