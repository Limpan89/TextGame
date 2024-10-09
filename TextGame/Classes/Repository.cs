using System.Text.Json;

namespace TextGame.Classes
{
    public static class Repository
    {
        private static readonly string _pathGame = @"C:\Users\lbrob\source\repos\TextGame\TextGame\Data\game_data.json";

        public static Game LoadGame()
        {
            Game game = new Game();

            using (StreamReader reader = new StreamReader(_pathGame))
            {
                string gameJSON = reader.ReadToEnd();
                game = JsonSerializer.Deserialize<Game>(gameJSON);
            }
            return game;
        }

        public static void LoadRoomsAndItems()
        {

        }

        public static void LoadExitsGame()
        {

        }

        public static void LoadItem(string name)
        {

        }
    }
}
