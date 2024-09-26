using ITNotion.Commands;
using ITNotion.Commands.StartCommands;
using ITNotion.Exceptions;
using ITNotion.Storage;

namespace ITNotion.Pages;

public class Start : ICommandPage
{
    public async Task<ICommandPage> AsyncInit()
    {
        await ExecuteCommand(Help);
        await ExecuteCommand(await CommandHandler());
        return this;
    }
    
    private readonly CommandReader _commandReader = new(null);
    
    private static readonly Dictionary<string, AbstractCommand> Commands = new()
    {
        {"reg", new RegisterCommand()},
        {"login", new LogInCommand()},
        {"exit", new ExitCommand()}
    };

    private static readonly HelpCommand Help = new(Commands);
    
    public async Task ExecuteCommand(AbstractCommand command)
    {
        
        while (command == Help)
        {
            await Help.Execute();
            Console.WriteLine($"\t--help\t{Help.Description}");
            command = await CommandHandler();
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
            try
            {
                throw new UnknownCommandException();
            }
            catch (UnknownCommandException e)
            {
                await Log.LogWarning(e);
                Console.WriteLine(e.Message);
                
                command = Commands.GetValueOrDefault(await _commandReader.GetCommand());
            }
        }

        return command;
    }
}