namespace Backend.cards.exceptions;

public class InsufficientCardBalanceException : Exception
{
    public InsufficientCardBalanceException(decimal balance) : base(
        $"Insufficient Balance, you only have ${balance} left on your card")
    {
    }
}