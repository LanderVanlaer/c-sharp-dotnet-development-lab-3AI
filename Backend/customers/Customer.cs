using System.Globalization;
using Backend.Utils;

namespace Backend.customers;

public class Customer : Base
{
    private static int _lastId = 1;

    internal Customer(string name, string contactPhoneNumber, DateTime birthDay)
    {
        Name = name;
        ContactPhoneNumber = contactPhoneNumber;
        BirthDay = birthDay;
        FirstSavingsAccount = IsUnderTwelveYears(BirthDay);
    }

    public Customer()
    {
        Console.Write("Please enter your name: ");
        Name = Console.ReadLine() ?? string.Empty;


        Console.Write("Please enter your contact phone number: ");
        ContactPhoneNumber = Console.ReadLine() ?? string.Empty;


        Console.Write("Please enter your birthday: ");
        // https://stackoverflow.com/a/32381146/13165967
        string input = Console.ReadLine() ?? string.Empty;

        DateTime dt;
        while (!DateTime.TryParseExact(
                   input,
                   "dd/MM/yyyy",
                   null,
                   DateTimeStyles.None,
                   out dt))
        {
            Console.Write("Invalid date: ");
            input = Console.ReadLine() ?? string.Empty;
        }

        BirthDay = dt;
        FirstSavingsAccount = IsUnderTwelveYears(BirthDay);
    }

    public bool FirstSavingsAccount { get; set; }
    public int Id { get; } = _lastId++;
    public string Name { get; private set; }
    public string ContactPhoneNumber { get; private set; }
    public DateTime BirthDay { get; }

    private static bool IsUnderTwelveYears(DateTime birthDay)
    {
        DateTime now = DateTime.Now;
        return now.Year - birthDay.Year < 12 ||
               (now.Year - birthDay.Year == 12 &&
                (birthDay.Month > now.Month ||
                 (birthDay.Month == now.Month && birthDay.Day <= now.Day))
               );
    }
}