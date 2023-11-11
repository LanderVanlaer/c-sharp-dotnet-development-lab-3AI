// Write a program for Online Attendance. The conditions are as follow:
// -    Users provide their name as Input and then application show message to “Welcome Name”.
// -    Jack, Steven, and Mathew are banned for the organization. So, when any user enters Jack,
//      Steven, or Mathew as the username, the application will raise an event that will sound
//      the alarm (invoke method that writes “FIRE ALARM!!” to the console) in the Alarm object
//      as well as sends an email to the administration (invoke method with the email address as
//      a parameter that writes “email to ‘email’” to the console) in an Email object.

using events_online_attendance;

events_online_attendance.Program program = new();

while (true)
{
    User user;
    while (true)
    {
        Console.Write("username: ");
        string input = (Console.ReadLine() ?? string.Empty).Trim();

        try
        {
            user = program.GetUserByName(input);
            break;
        }
        catch (UserNotFoundException)
        {
            Console.WriteLine("User does not exist, please try again");
        }
        catch (BlockedUserException)
        {
            Console.WriteLine("You are banned!");
        }
    }

    Console.WriteLine($"Welcome {user.UserName}\n");
}


namespace events_online_attendance
{
    internal class Program
    {
        private readonly List<User?> _users = new()
        {
            new User("John"),
            new User("Jane"),
            new User("Jack", true),
            new User("Steven", true),
            new User("Mathew", true),
        };

        public User GetUserByName(string name)
        {
            User? user = _users.Find(u => string.Equals(u.UserName, name, StringComparison.OrdinalIgnoreCase));

            if (user == null)
                throw new UserNotFoundException();

            if (user.IsBlocked)
                throw new BlockedUserException();

            return user;
        }
    }

    internal class User
    {
        public readonly bool IsBlocked;
        public readonly string UserName;

        public User(string userName, bool isBlocked = false)
        {
            UserName = userName;
            IsBlocked = isBlocked;
        }
    }

    public class BlockedUserException : Exception
    {
        private const string AdministratorEmail = "admin@local.com";

        public BlockedUserException()
        {
            Console.WriteLine("FIRE ALARM!!");
            Console.WriteLine($"email to {AdministratorEmail}");
        }
    }

    public class UserNotFoundException : KeyNotFoundException
    {
    }
}