using ITNotion.Commands;
using ITNotion.Commands.ListPageCommands;
using ITNotion.Exceptions;
using ITNotion.Notes;
using ITNotion.Storage;
using ITNotion.User;

namespace ITNotion.Pages;

public class ListPage(User.User user) : ICommandPage
{
    private User.User User { get; } = user;
    private CommandReader? _commandReader;
    public DirectoryNotes? CurrentDirectory { get; private set; }
    
    public async Task<ICommandPage> AsyncInit()
    {
        CurrentDirectory = User.Sources;
        
        _commandReader = new CommandReader(User);
        _commands = new Dictionary<string, AbstractCommand>
        {
            {"cd", new CdCommand(User, this)},
            {"create", new CreateCommand(User, this)},
            {"menu", new MenuCommand(User)},
            {"open", null},
            {"delete", null}
        };
        await PrintCurrentDirectory();
        Console.WriteLine();
        _help = new HelpCommand(_commands);
        var taskHelp = ExecuteCommand(_help);
        var taskNewCommand = ExecuteCommand(await CommandHandler());
        await Task.WhenAll(taskHelp, taskNewCommand);
        Console.WriteLine();
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
        
        await command.Execute();
    }

    public async Task PrintCurrentDirectory()
    {
        Console.WriteLine("-: папка, *: файл");
        foreach (var file in CurrentDirectory!.Directory)
        {
            await Console.Out.WriteLineAsync(file.ToString());
        }
    }

    public async Task ToDirectory(string? name)
    {

        if ((bool)name?.Equals("-"))
        {
            if (CurrentDirectory!.Parent == null) return;
            CurrentDirectory = CurrentDirectory!.Parent;
            return;

        }
        
        try
        {
            foreach (var source in CurrentDirectory!.Directory
                         .Where(source => source.GetType() == typeof(DirectoryNotes))
                         .Where(source => source.Name.Equals(name)))
            {
                CurrentDirectory = (DirectoryNotes)source;
                return;
            }
            
            throw new UnknownDirectoryException();
        }
        catch (UnknownDirectoryException e)
        {
            await Log.LogWarning(e, new UserDto(User));
            Console.WriteLine(e.Message);
        }
    }
    
    public async Task<AbstractCommand> CommandHandler()
    {   
        var commandString = await _commandReader!.GetCommand();
        if (commandString == "help") return _help!;
        var command = _commands!.GetValueOrDefault(commandString[..commandString.IndexOf(' ')]);
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
