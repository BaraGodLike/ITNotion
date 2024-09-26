using System.Text.Json;
using ITNotion.Notes;
using ITNotion.User;

namespace ITNotion.Storage;

public class LocalRepository : IStorage
{
    public async Task SaveRegistryUser(UserDto user)
    {
        if (!Directory.Exists("Storage/Users/"))
        {
            Directory.CreateDirectory("Storage/Users");
        }
        await using var createStream = File.Create($"Storage/Users/{user.User!.Name}.json");
        await JsonSerializer.SerializeAsync(createStream, user);
    }
    
    
    public bool HasNicknameInStorage(string name)
    {
        return File.Exists($"Storage/Users/{name}.json");
    }

    public async Task<UserDto?> GetUserFromStorage(string name)
    {
        await using var openStream = File.OpenRead($"Storage/Users/{name}.json");
        return await JsonSerializer.DeserializeAsync<UserDto>(openStream);
    }

    public async Task CreateNewNote(Note note)
    {
        if (!Directory.Exists(note.Path))
        {
            Directory.CreateDirectory(note.Path);
        }
        await using var createStream = File.Create(note.Path);
    }
}