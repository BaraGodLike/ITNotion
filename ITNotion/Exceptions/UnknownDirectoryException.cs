namespace ITNotion.Exceptions;

public class UnknownDirectoryException() : Exception("Репозиторий не найден")
{
    public override string ToString()
    {
        return "go to unknown repo";
    }
}