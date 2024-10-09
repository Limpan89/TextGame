using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    public class GameCLI
    {
        public Game TextGame { get; set; }

        public GameCLI()
        {
            TextGame = Repository.LoadGame();
        }

        public void Run()
        {
            Repository.LoadGame();
            while (true)
            {
                if (ParseCommand(ReadCommand("Command"), out List<string> commands))
                    ExecuteCommand(commands);
                else
                {
                    if (commands.Count == 0)
                        Console.WriteLine("I don't understand what you want to do?");
                    else
                        Console.WriteLine($"I don't understand \"{commands[0]}\"");
                }
            }
        }

        private void ExecuteCommand(List<string> commands)
        {
            if (commands.Count == 1)
                TextGame.ExecuteCommands(commands[0]);
            else if (commands.Count == 2)
                TextGame.ExecuteCommands(commands[0], commands[1]);
            else
                TextGame.ExecuteCommands(commands[0], commands[1], commands[2]);
        }

        private string ReadCommand(string message)
        {
            Console.Write($"{message}: ");
            return Console.ReadLine();
        }

        private bool ParseCommand(string cmd, out List<string> commands)
        {
            string[] words = cmd.Split(' ');
            commands = new List<string>();

            if (words.Length == 0)
                return false;

            commands.Add(words[0]);
            if (words.Length == 1)
                return TextGame.IsShorthand(words[0]);

            if (!TextGame.IsVerb(commands[0]))
                return false;

            string word = "";
            for (int i = 1; i < words.Length; i++)
            {
                if (TextGame.IsPreposition(words[i]))
                {
                    if (TextGame.IsNoun(word))
                    {
                        commands.Add(word);
                        word = "";
                    }
                    else
                    {
                        commands = new List<string>() { word };
                        return false;
                    }
                }
                word += words[i];
            }
            if (TextGame.IsNoun(word))
                commands.Add(word);
            else
            {
                commands = new List<string>() { word };
                return false;
            }
            return true;
        }
    }
}
