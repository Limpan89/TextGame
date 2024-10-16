using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Classes
{
    public class IOHandler
    {
        public Game TextGame { get; set; }

        public IOHandler()
        {
            TextGame = FileHandler.LoadGame();
        }

        public void Run()
        {
            bool run = true;
            Console.WriteLine(TextGame.Intro); // Displays intro
            Console.WriteLine(TextGame.Player.CurrentRoom.Enter());
            while (run)
            {
                if (ParseCommand(ReadCommand("Command"), out List<string> commands))
                    run = ExecuteCommand(commands);
                else
                {
                    if (commands.Count == 0)
                        Console.WriteLine("I don't understand what you want to do.");
                    else if (commands[0].ToLower() == "go")
                        Console.WriteLine("I dont understand where you want to go.");
                    else if (TextGame.IsVerb(commands[0]))
                        Console.WriteLine($"I don't understand what you want to {commands[0]}.");
                    else
                        Console.WriteLine($"I don't understand \"{commands[0]}\"");
                }
            }
            Console.WriteLine(TextGame.Outro); // Displays outro
        }

        private bool ExecuteCommand(List<string> commands)
        {
            bool run;
            string output;
            if (commands.Count == 1)
                run = TextGame.ExecuteCommands(commands[0], out output);
            else if (commands.Count == 2)
                run = TextGame.ExecuteCommands(commands[0], commands[1], out output);
            else
                run = TextGame.ExecuteCommands(commands[0], commands[1], commands[2], out output);
            Console.WriteLine(output);
            return run;
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

            if (words.Length == 0) // No user input
                return false;

            commands.Add(words[0]);
            if (words.Length == 1)
                return TextGame.IsShorthand(words[0]);

            if (!TextGame.IsVerb(commands[0])) // First word not a valid verb
                return false;

            string word = "";
            for (int i = 1; i < words.Length; i++)
            {
                if (TextGame.IsPreposition(words[i])) // Breaks up into multiple Nauns
                {
                    if (TextGame.IsNoun(word))
                    {
                        commands.Add(word);
                        word = "";
                        continue;
                    }
                    else if (TextGame.IsPartialUniqueItemName(word, out string output))
                    {
                        commands.Add(output);
                        word = "";
                        continue;
                    }
                    else
                    {
                        commands = new List<string>() { word };
                        return false;
                    }
                }
                if (word.Length == 0)
                    word = words[i];
                else
                    word = $"{word} {words[i]}";
            }
            if (TextGame.IsNoun(word))
                commands.Add(word);
            else if (TextGame.IsPartialUniqueItemName(word, out string output))
                commands.Add(output);
            else
            {
                commands = new List<string>() { word };
                return false;
            }
            if (commands.Count > 3) // if more then 1 Verb + 2 Nouns
            {
                commands = new List<string>();
                return false;
            }
            return true;
        }
    }
}
