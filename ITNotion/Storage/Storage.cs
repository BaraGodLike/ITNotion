using System.Text.Json;
using ITNotion.User;
using Microsoft.VisualBasic;

namespace ITNotion.Storage;

public static class Storage
{
    public static string HashPassword(string password)
    {
        return Conversion.Hex(password.Select((t, i) => 
            t * (int) Math.Pow(7, i)).Sum());
    }

    public static bool HasNicknameInStorage(string name)
    {
        return File.Exists($"Storage/Users/{name}.json");
    }

    public static async void SaveRegistryUser(UserDto user)
    {
        if (!Directory.Exists("Storage/Users/"))
        {
            Directory.CreateDirectory("Storage/Users");
        }
        await using var createStream = File.Create($"Storage/Users/{user.User!.Name}.json");
        await JsonSerializer.SerializeAsync(createStream, user);
        
    }

    public static async Task<UserDto?> UserFromJson(string name) 
    {
        await using var openStream = File.OpenRead($"Storage/Users/{name}.json");
        return await JsonSerializer.DeserializeAsync<UserDto>(openStream);
    }
}