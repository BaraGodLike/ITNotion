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
    private DirectoryNotes? _currentDirectory;
    
    public async Task<ICommandPage> AsyncInit()
    {
        _currentDirectory = User.Sources;
        
        _commandReader = new CommandReader(User);
        _commands = new Dictionary<string, AbstractCommand>
        {
            {"cd", new CdCommand(User, this)},
            {"create", new CreateCommand(User)},
            {"menu", null},
            {"open", null},
            {"delete", null}
        };
        _help = new HelpCommand(_commands);
        var taskHelp = ExecuteCommand(_help);
        var taskNewCommand = ExecuteCommand(await CommandHandler());
        await Task.WhenAll(taskHelp, taskNewCommand);
        Console.WriteLine();
        await PrintCurrentDirectory();
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
            await ExecuteCommand(await CommandHandler());
        }
        
        await command.Execute();
    }

    public async Task PrintCurrentDirectory()
    {
        Console.WriteLine("-: папка, *: файл");
        foreach (var file in _currentDirectory!.Directory!)
        {
            await Console.Out.WriteLineAsync(file.ToString());
        }
    }

    public async Task ToDirectory(string? name)
    {
        
        foreach (var source in _currentDirectory!.Directory.
                     Where(source => source.GetType() == typeof(DirectoryNotes)).
                     Where(source => source.Name.Equals(name)))
        {
            _currentDirectory = (DirectoryNotes) source;
            return;
        }

        await Log.LogWarning(new UserDto(User), "cd unknown repo");
        throw new UnknownDirectoryException();
    }
    
    public async Task<AbstractCommand> CommandHandler()
    {
        var commandString = await _commandReader!.GetCommand();
        if (commandString == "help") return _help!;
        var command = _commands!.GetValueOrDefault(commandString);
        while (command == null) 
        {
            Console.WriteLine("Неизвестная команда. --help для вывода списка доступных команд");
            
            command = _commands!.GetValueOrDefault(await _commandReader.GetCommand());
        }

        return command;
    }
}