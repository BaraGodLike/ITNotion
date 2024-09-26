using ITNotion.Notes;
using ITNotion.User;
using Microsoft.VisualBasic;

namespace ITNotion.Storage;

public class Storage(IStorage repo) : IStorage
{
    public static string HashPassword(string password)
    {
        return Conversion.Hex(password.Select((t, i) => 
            t * (long) Math.Pow(7, i)).Sum());
    }

    public bool HasNicknameInStorage(string name)
    {
        return repo.HasNicknameInStorage(name);
    }

    public async Task SaveRegistryUser(UserDto user)
    {
        await repo.SaveRegistryUser(user);
    }

    public async Task<UserDto?> GetUserFromStorage(string name)
    {
        return await repo.GetUserFromStorage(name);
    }

    public async Task CreateNewNote(AbstractSource source)
    {
        await repo.CreateNewNote(source);
    }
}