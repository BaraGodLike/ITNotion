using ITNotion.Notes;
using ITNotion.User;

namespace ITNotion.Storage;

public interface IStorage
{
    Task SaveRegistryUser(UserDto user);
    bool HasNicknameInStorage(string name);
    Task<UserDto?> GetUserFromStorage(string name);
    Task CreateNewNote(Note note);

}