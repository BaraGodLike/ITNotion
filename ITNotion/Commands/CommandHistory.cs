namespace ITNotion.Commands;

public class CommandHistory
{
    private readonly AbstractCommand[] _history = new AbstractCommand[10];
    private int _last = -1;

    public void Put(AbstractCommand command)
    {
        _history[++_last] = command;
    }

    public AbstractCommand Pop()
    {
        return _history[_last--];
    }
}