using ITNotion.ConsoleCommand;

namespace ITNotion.Authorization;

public class Start : ICommandHandler
{
    private Escape? _escape;
    private ConsoleCommand.ConsoleCommand? _consoleCommand;
    
    public async Task StartProgram()
    {
        _escape = new Escape();
        _mapOfAuthorizations = new Dictionary<string, Func<Task>>
        {
            {"help", Help},
            {"reg", () => new Registry(_escape).Authorize()},
            {"log", () => new LogIn(_escape).Authorize()},
            {"exit", Exit}
        };
        await Help();
        _consoleCommand = new ConsoleCommand.ConsoleCommand(_escape, this);
        var escapeThread = _escape.CheckPressEscape();
        var commandThread = _consoleCommand.GetCommandAsync();
        var mainThread = CommandHandler(null);
        await Task.WhenAll(escapeThread, commandThread, mainThread);
    }

    private Dictionary<string, Func<Task>>? _mapOfAuthorizations;
    
    public async Task CommandHandler(string? command)
    {
        if (command == null || command.Length < 2 || !command[..2].Equals("--"))
        {
            Console.WriteLine("Неизвестная команда. --help для списка комманд");
            _consoleCommand!.GetCommandAsync();
        }

        if (command!.Equals("--exit")) await Exit();
        if (command!.Equals("--help")) await Help();
        if (_mapOfAuthorizations!.TryGetValue(command[2..], out var value))
        {
            value();
        }
    }

    public async Task Exit()
    {
        Environment.Exit(0);
    }
    
    public async Task Help()
    {
        Console.WriteLine("Список доступных команд: ");
        foreach (var key in _mapOfAuthorizations!)
        {
            Console.WriteLine($"\t--{key.Key}");
        }
        Console.WriteLine("");
    }
}