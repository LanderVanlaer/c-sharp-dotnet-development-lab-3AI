using System.Collections.Immutable;
using System.Globalization;
using System.Text;
using Backend.Utils;

namespace Backend.customers;

public class Customer : Base
{
    private static int _lastId = 1;
    public readonly ContactTypeHandler ContactTypes;

    internal Customer(string name, string contactPhoneNumber, DateTime birthDay, Gender gender)
    {
        ContactTypes = new ContactTypeHandler(this);

        Name = name;
        ContactPhoneNumber = contactPhoneNumber;
        BirthDay = birthDay;
        FirstSavingsAccount = IsUnderTwelveYears(BirthDay);
        Gender = gender;
    }

    public Customer()
    {
        ContactTypes = new ContactTypeHandler(this);

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

        //https://www.c-sharpcorner.com/UploadFile/d3e4b1/how-to-play-with-enum-in-C-Sharp/
        Console.WriteLine("Please enter your gender:");
        foreach (int value in Enum.GetValues(typeof(Gender)))
            Console.WriteLine("  " + value + ": " + Enum.GetName(typeof(Gender), value));

        do
        {
            input = Console.ReadLine() ?? "1";

            if (!int.TryParse(input, out int value))
            {
                Console.Write("Please enter a number: ");
                continue;
            }

            if (!Enum.IsDefined(typeof(Gender), value))
            {
                Console.Write("Please enter a valid value: ");
                continue;
            }

            Gender = (Gender)value;
            break;
        } while (true);
    }

    public Gender Gender { get; init; }
    public bool FirstSavingsAccount { get; set; }
    public int Id { get; } = _lastId++;
    public string Name { get; }
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

    public string Message(string title, string message)
    {
        string honorific = Gender switch
        {
            Gender.Female => "Madam ",
            Gender.Male => "Mister ",
            Gender.OtherOrRatherNotSay => "",
            _ => throw new Exception(),
        };

        StringBuilder builder = new(22);
        foreach (ContactType contactType in ContactTypes.All)
            //Contact via Whatsapp: Title, Dear Madam Juliette, Message
            builder
                .Append("Contact via ")
                .Append(Enum.GetName(typeof(ContactType), contactType))
                .Append(": ")
                .Append(title)
                .Append(", Dear ")
                .Append(honorific)
                .Append(Name)
                .Append(", ")
                .Append(message)
                .Append("; ");

        return builder.ToString();
    }

    public readonly struct ContactTypeHandler
    {
        private readonly HashSet<ContactType> _contactTypes = new();
        private readonly Customer _customer;

        internal ContactTypeHandler(Customer customer)
        {
            _customer = customer;
        }

        public ImmutableHashSet<ContactType> All => _contactTypes.ToImmutableHashSet();

        public void Remove(ContactType item)
        {
            if (!_contactTypes.Remove(item))
                throw new ArgumentException(
                    $"The given {nameof(ContactType)} wasn't active for this {nameof(Customer)}"
                );
        }

        public void Add(ContactType type)
        {
            if (!_contactTypes.Add(type))
                throw new ArgumentException($"The given {nameof(ContactType)} is already active");
        }
    }
}