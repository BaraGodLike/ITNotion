using ITNotion.Commands;

namespace ITNotion.Pages;

public class Start : ICommands
{
    public Start()
    {
        ExecuteCommand(Help);
        ExecuteCommand(CommandHandler());
    }
    
    private readonly CommandReader _commandReader = new CommandReader(null);
    
    private static readonly Dictionary<string, AbstractCommand> Commands = new Dictionary<string, AbstractCommand>
    {
        {"reg", new RegisterCommand()},
        {"login", new LogInCommand()},
        {"exit", new ExitCommand()}
    };

    private static readonly HelpCommand Help = new HelpCommand(Commands);
    
    public void ExecuteCommand(AbstractCommand command)
    {
        while (command == Help)
        {
            Help.Execute();
            Console.WriteLine($"\t--help\t{Help.Description}");
            ExecuteCommand(CommandHandler());
        }

        command.Execute();
    }

    public AbstractCommand CommandHandler()
    {
        var commandString = _commandReader.GetCommand();
        if (commandString == "help") return Help;
        var command = Commands.GetValueOrDefault(commandString);
        while (command == null) 
        {
            Console.WriteLine("Неизвестная команда. --help для вывода списка доступных команд");
            
            command = Commands.GetValueOrDefault(_commandReader.GetCommand());
        }

        return command;
    }
}