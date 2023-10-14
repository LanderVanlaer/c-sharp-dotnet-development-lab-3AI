using Backend.customers;

namespace Backend.cards;

public class BankCard : Card
{
    internal BankCard(string pinCode, Customer holder) : base(pinCode, holder)
    {
    }
}