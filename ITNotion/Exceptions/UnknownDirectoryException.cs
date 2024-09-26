namespace ITNotion.Exceptions;

public class UnknownDirectoryException : Exception
{
    public UnknownDirectoryException()
    {
        Console.WriteLine("Репозиторий не найден");
    }
}