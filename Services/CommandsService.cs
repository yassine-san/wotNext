using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace wotNext.Services
{
    public class CommandsService
    {
        private readonly JsonDocument _cliConfig ;
        private string[] args;

        public CommandsService(string[] args)
        {
            if (args.Length == 0)
            {
                NoArgumentProvided();
            }
            this.args = args;
            var json = File.ReadAllText(Directory.GetCurrentDirectory()+"\\commands.json");
            _cliConfig = JsonDocument.Parse(json);
        }

        public void ManageArgs()
        {
            JsonElement commands = _cliConfig.RootElement.GetProperty("commands");
            
            string providedCommand = args[0];
            if (commands.TryGetProperty(providedCommand, out JsonElement command))
            {
                switch (providedCommand)
                {
                    case "--login": LoginIn(command); break;
                    case "--help": DisplayHelp(command); break;
                    default: Console.WriteLine("nOnE"); break;
                }
            }
            else
            {
                Console.WriteLine("Command not found. Use 'wotNext.exe --help' for a list of commands.");
            }
        }
        
        private void LoginIn(JsonElement loginElement)
        {
            ArgsValidation("login", loginElement);
            Console.WriteLine($"Enter password for the user {args[1]}");
            string password = Console.ReadLine();

            Console.WriteLine($"your password is {password}");
            
            Console.WriteLine("gonna login");
        }

        private void DisplayHelp(JsonElement helpElement)
        {
            ArgsValidation("help", helpElement);
            var element = _cliConfig.RootElement.GetProperty("commands");
            Console.WriteLine(element);
        }

        private void NoArgumentProvided()
        {
            Console.WriteLine("No Command was provided. Use 'wotNext.exe --help' for a list of commands.");
            Environment.Exit(0);
        }

        private void InvalidNbArgs(string command,string usage, int needed, int provided)
        {
            Console.WriteLine($"invalid number of arguments for the command '{command}', needed {needed}, provided {provided}");
            Console.WriteLine($"usage : {usage}");
            Environment.Exit(0);
        }

        private void ArgsValidation(string commandName, JsonElement commandElement )
        {
            string usage = commandElement.GetProperty("usage").GetString();
            int nbArguments = commandElement.GetProperty("nbArguments").GetInt32();
            
            if (args.Length-1 != nbArguments)
            {
                InvalidNbArgs(commandName,usage, nbArguments, args.Length - 1);
            }
        }
        
    }
}