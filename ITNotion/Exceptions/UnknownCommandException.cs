namespace ITNotion.Exceptions;

public class UnknownCommandException() : Exception("Неизвестная команда. --help для вывода списка доступных команд")
{
    public override string ToString()
    {
        return "used unknown command";
    }
}