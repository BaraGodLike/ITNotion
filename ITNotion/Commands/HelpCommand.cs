using System.Transactions;

namespace ITNotion.Commands;

public class HelpCommand : AbstractCommand
{
    public HelpCommand(Dictionary<string, AbstractCommand> map)
    {
        _map = map;
        Description = "вывод списка доступных команд";
    }
    
    private readonly Dictionary<string, AbstractCommand> _map;

    public override bool Execute()
    {
        Console.WriteLine("Список доступных комманд:");
        foreach (var key in _map)
        {
            Console.WriteLine($"\t--{key.Key}\t{key.Value.Description}");
        }
        
        return false;
    }
}