using ITNotion.Storage;

namespace ITNotion.Authorization;

public class LogIn : IAuthorization
{
    private string? Name { get; set; }
    private string? Password { get; set; }
    public void Authorize()
    {
        InputName();
        if (!InputPassword()) return;
        Console.WriteLine($"Добро пожаловать, {Name}");
        Log.LogAuthorization(new AuthorizationUserDto(this), "log in");
    }

    public string? GetName()
    {
        return Name;
    }
    
    private void InputName()
    {
        while (true)
        {
            Console.Write("Введите имя пользователя: ");
            var name = Console.ReadLine();
            if (name == null)
            {
                Console.WriteLine("Имя пользователя не может быть пустым.");
                continue;
            }
            
            if (!Storage.Storage.HasNicknameInStorage(name))
            {
                Console.WriteLine("Польозователь не найден.");
                continue;
            }

            Name = name;
            break;
        }
    }

    private bool InputPassword()
    {
        Console.Write("Введите пароль: ");
        var password = Console.ReadLine();
        var attempt = 3;
        while (!IsCorrectPassword(password!))
        {
            Console.WriteLine($"Неверный пароль. Осталось попыток: {attempt}");
            Log.LogAuthorization(new AuthorizationUserDto(this), "failed login attempt");
            Console.Write("Введите пароль: ");
            password = Console.ReadLine();
            if (attempt-- <= 0) break;
        }

        if (attempt <= 0)
        {
            Console.WriteLine("Слишком много попыток!");
            Log.LogAuthorization(new AuthorizationUserDto(this), "too many login attempts");
            return false;
        }
        Password = password!;
        return true;
    }

    private bool IsCorrectPassword(string password)
    {
        return Storage.Storage.HashPassword(password).Equals(Storage.Storage.UserFromJson(Name!).Result?["User"]["Password"]);
    }
}