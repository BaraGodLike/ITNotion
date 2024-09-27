namespace ITNotion.Notes;

public static class SourceHandler
{
    public static async Task<AbstractSource> GetFromString(string? name, DirectoryNotes parent)
    {
        if (name == null)
        {
            return new DirectoryNotes("New directory", parent);
        }

        if (name.Length >= 5 && name[^3..].Equals(".txt"))
        {
            return new Note(name, parent);
        }

        return new DirectoryNotes(name, parent);
    }
}