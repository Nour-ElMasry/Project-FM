using Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.Entities;

public class User : Person
{
    public User(string username, string password, string name, string birthDate, string nationality, string photo) : base(name, birthDate, nationality, photo)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Password = IsPassSecure(password);
    }

    private static string IsPassSecure(string pass)
    {
        if (pass == null)
            throw new ArgumentNullException(nameof(pass));

        if (pass.Length < 8)
            throw new PasswordNotSecureException("Password is too short! please have more than 8 characters in password!");


        string regex = "^(?=.*[a-z])(?=."
                    + "*[A-Z])(?=.*\\d)"
                    + "(?=.*[-+_!@#$%^&*.,?]).+$";

        Regex p = new Regex(regex);

        if (!p.Match(pass).Success)
            throw new PasswordNotSecureException($"Password must contain at least one ->" +
                $"\n - Capital Letter" +
                $"\n - Digit" +
                $"\n - Special symbol such as: (|!#$%&/()=?»«@£€.-;'<>_,)");

        return pass;
    }

    public string Username { get; set; }
    public string Password { get; set; }

    public override string? ToString()
    {
        return "User {" +
            "\nUsername: " + Username +
            "\nPassword: " + Password +
            "\nPerson: {" +
            "\n Name: " + Name +
            "\n Birthdate: " + BirthDate.ToString() +
            "\n Nationality: " + Nationality +
            "\n Photo: " + Photo +
            "\n}\n}";
    }
}

