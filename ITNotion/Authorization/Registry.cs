using System.Text.RegularExpressions;
using ITNotion.Storage;
using ITNotion.User;

namespace ITNotion.Authorization;

public partial class Registry : IAuthorization
{
    private string? Name { get; set; }
    private string? Password { get; set; }
    private User.User? _user;
    // private UserDto? _userDto;
    
    public User.User? Authorize()
    {
        InputName();
        InputPassword();
        
        _user = new User.User(Name!, Password!);
        // _userDto = new UserDto(_user);
        
        Storage.Storage.SaveRegistryUser(new UserDto(_user));
        
        Console.WriteLine("Вы успешно зарегестрировались!");
        return _user;
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

            if (Storage.Storage.HasNicknameInStorage(name))
            {
                Console.WriteLine("Данное имя пользователя уже занято.");
                continue;
            }

            Name = name;
            break;
        }
    }

    private void InputPassword()
    {
        while (true)
        {
            Console.Write("Введите пароль длиной 3-20 символов (a-Z, 0-9, _): ");
            var password = Console.ReadLine();
            if (password == null || password.Length is < 3 or > 20)
            {
                Console.WriteLine("Пароль должен содержать от 3 до 20 символов.");
                continue;
            }

            if (!MyRegex().IsMatch(password))
            {
                Console.WriteLine("Пароль может содержать только латинские буквы, цифры и _");
                continue;
            }

            Console.Write("Повторите пароль: ");
            var passwordRepeat = Console.ReadLine();
            while (!password.Equals(passwordRepeat))
            {
                Console.WriteLine("Пароли не совпадают.");
                Console.Write("Повторите пароль: ");
                passwordRepeat = Console.ReadLine();
            }
            Password = Storage.Storage.HashPassword(password);
            break;
        }
    }

    [GeneratedRegex(@"^[a-zA-Z0-9_]+$")]
    private static partial Regex MyRegex();
}