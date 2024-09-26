using ITNotion.Commands;
using ITNotion.Commands.StartCommands;

namespace ITNotion.Pages;

public class Start : ICommandPage
{
    public async Task<ICommandPage> AsyncInit()
    {
        await ExecuteCommand(Help);
        await ExecuteCommand(await CommandHandler());
        return this;
    }
    
    private readonly CommandReader _commandReader = new CommandReader(null);
    
    private static readonly Dictionary<string, AbstractCommand> Commands = new Dictionary<string, AbstractCommand>
    {
        {"reg", new RegisterCommand()},
        {"login", new LogInCommand()},
        {"exit", new ExitCommand()}
    };

    private static readonly HelpCommand Help = new HelpCommand(Commands);
    
    public async Task ExecuteCommand(AbstractCommand command)
    {
        while (command == Help)
        {
            await Help.Execute();
            Console.WriteLine($"\t--help\t{Help.Description}");
            await ExecuteCommand(await CommandHandler());
        }

        await command.Execute();
    }

    public async Task<AbstractCommand> CommandHandler()
    {
        var commandString =  await _commandReader.GetCommand();
        if (commandString == "help") return Help;
        var command = Commands.GetValueOrDefault(commandString);
        while (command == null) 
        {
            Console.WriteLine("Неизвестная команда. --help для вывода списка доступных команд");
            
            command = Commands.GetValueOrDefault(await _commandReader.GetCommand());
        }

        return command;
    }
}