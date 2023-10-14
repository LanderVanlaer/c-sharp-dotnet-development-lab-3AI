using Backend.customers;
using Backend.Utils;

namespace Backend.cards;

public abstract class Card : Base
{
    public const int NumberLength = 8;
    private readonly string _number;
    public Customer Holder;
    private string PinCode;

    protected Card(string pinCode, Customer holder)
    {
        _number = GenerateCardNumber();
        PinCode = pinCode;
        Holder = holder;
    }

    public string Number
    {
        get => _number;
        init
        {
            if (!IsValidCardNumber(value))
                throw new ArgumentException($"{nameof(Card)} {nameof(Number)} invalid");

            _number = value;
        }
    }

    /// <seealso cref="NumberLength" />
    /// <seealso cref="char.IsDigit(char)" />
    private static bool IsValidCardNumber(string number)
    {
        return number.Length == NumberLength && number.All(Char.IsDigit);
    }

    protected static string GenerateCardNumber()
    {
        Random random = new();
        char[] number = new char[NumberLength];

        for (int i = 0; i < NumberLength; i++)
            number[i] = (char)((int)Math.Floor(random.NextDouble() * 10d) + 48);

        return new string(number);
    }
}