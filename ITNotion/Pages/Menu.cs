using ITNotion.Commands;
using ITNotion.Commands.MenuCommands;
using ITNotion.Exceptions;
using ITNotion.Storage;
using ITNotion.User;

namespace ITNotion.Pages;

public class Menu(User.User user) : ICommandPage
{
    private User.User User { get; } = user;
    private CommandReader? _commandReader;

    public async Task<ICommandPage> AsyncInit()
    {
        _commandReader = new CommandReader(User);
        _commands = new Dictionary<string, AbstractCommand>
        {
            {"list", new ListCommand(User)},
            {"logout", new LogOutCommand(User)}
        };
        _help = new HelpCommand(_commands);
        var taskHelp = ExecuteCommand(_help);
        var taskNewCommand = ExecuteCommand(await CommandHandler());
        await Task.WhenAll(taskHelp, taskNewCommand);
        return this;
    }

    private Dictionary<string, AbstractCommand>? _commands;

    private HelpCommand? _help;
    
    public async Task ExecuteCommand(AbstractCommand command)
    {
        
        while (command == _help)
        {
            await _help.Execute();
            Console.WriteLine($"\t--help\t{_help.Description}");
            command = await CommandHandler();
        }
        
        var execute = command.Execute();
        var log = Log.LogInformation(new UserDto(User), command);
        await Task.WhenAll(execute, log);
    }

    public async Task<AbstractCommand> CommandHandler()
    {
        var commandString = await _commandReader!.GetCommand();
        if (commandString == "help") return _help!;
        var command = _commands!.GetValueOrDefault(commandString);
        while (command == null) 
        {
            try
            {
                throw new UnknownCommandException();
            }
            catch (UnknownCommandException e)
            {
                await Log.LogWarning(e, new UserDto(User));
                Console.WriteLine(e.Message);

                command = _commands!.GetValueOrDefault(await _commandReader.GetCommand());
            }
        }

        return command;
    }
}