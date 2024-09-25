using System.Data;
using System.Text.Json;
using ITNotion.Authorization;
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

    public static async Task SaveRegistryUser(AuthorizationUserDto user)
    {
        if (!Directory.Exists("Storage/Users/"))
        {
            Directory.CreateDirectory("Storage/Users");
        }
        await using var createStream = File.Create($"Storage/Users/{user.User.GetName()}.json");
        await JsonSerializer.SerializeAsync(createStream, user);
        
    }

    public static async Task<Dictionary<string, Dictionary<string, string>>?> UserFromJson(string name) 
    {
        await using var openStream = File.OpenRead($"Storage/Users/{name}.json");
        return await JsonSerializer.DeserializeAsync<Dictionary<string, Dictionary<string, string>>>(openStream);
    }
}