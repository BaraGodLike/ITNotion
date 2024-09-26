namespace ITNotion.Commands;

public class CommandHistory
{
    private readonly List<AbstractCommand> _history = new List<AbstractCommand>(10);
    

    public void Put(AbstractCommand command)
    {
        _history.Add(command);
    }

    public AbstractCommand Pop()
    {
        var last = _history.Last();
        _history.Remove(last);
        return last;
    }
}