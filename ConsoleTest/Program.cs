﻿// See https://aka.ms/new-console-template for more information

using Backend;
using Backend.accounts;
using Backend.cards;
using Backend.customers;
using Backend.Transactions;
using Backend.Transactions.exceptions;
using Backend.Utils;
using Newtonsoft.Json;

Base.DefaultFormatting = Formatting.Indented;

Bank bank = new();

CurrentAccount currentAccount = bank.Accounts.CreateCurrent();
Customer customer = bank.Customers.Create(
    "Test",
    "123",
    new DateTime(2003, 4, 18, 0, 0, 0, DateTimeKind.Utc),
    Gender.Male
);

currentAccount.AddCustomer(customer);

Console.WriteLine("Message:\n\t" + customer.Message("Title", "a message"));
customer.ContactTypes.Add(ContactType.Email);
Console.WriteLine("Message:\n\t" + customer.Message("Title", "a message"));
customer.ContactTypes.Add(ContactType.Phone);
customer.ContactTypes.Add(ContactType.WhatsApp);
Console.WriteLine("Message:\n\t" + customer.Message("Title", "a message"));

BankCard bankCard = currentAccount.Cards.CreateBankCard("2000", customer);
CreditCard creditCard = currentAccount.Cards.CreateCreditCard("2000", customer);

Customer customer2 = bank.Customers.Create(
    "Test",
    "123",
    new DateTime(2003, 4, 18, 0, 0, 0, DateTimeKind.Utc),
    Gender.Female
);


Console.WriteLine("SavingAccount numbers: ");
for (int i = 0; i < 3; i++)
{
    SavingAccount savingAccount = bank.Accounts.CreateSaving();
    Console.WriteLine("\t" + savingAccount.Number);
    bank.Accounts.Remove(savingAccount);
}

Console.WriteLine("\nSavingAccount < 12 years");
SavingAccount savingAccountOlder = bank.Accounts.CreateSaving();
savingAccountOlder.AddCustomer(
    bank.Customers.Create(
        "Younger",
        "+32 400 00 00 00",
        new DateTime(2003, 4, 18, 0, 0, 0, DateTimeKind.Utc),
        Gender.OtherOrRatherNotSay
    )
);

SavingAccount savingAccountYounger = bank.Accounts.CreateSaving();
savingAccountYounger.AddCustomer(
    bank.Customers.Create(
        "Younger",
        "+32 400 00 00 00",
        new DateTime(DateTime.Now.Year - 6, 4, 18, 0, 0, 0, DateTimeKind.Utc),
        Gender.OtherOrRatherNotSay
    )
);

Console.WriteLine("Older:");
Console.WriteLine("\tBalance:\t" + savingAccountOlder.Balance);
Console.WriteLine("\tCustomer.FirstSavingsAccount:\t" + savingAccountOlder.AccessList.First().FirstSavingsAccount);

Console.WriteLine("Younger:");
Console.WriteLine("\tBalance:\t" + savingAccountYounger.Balance);
Console.WriteLine("\tCustomer.FirstSavingsAccount:\t" + savingAccountYounger.AccessList.First().FirstSavingsAccount);


Console.WriteLine("\n############         SORT         ############\n");


for (int i = 0; i < 5; i++)
    bank.Accounts.CreateCurrent();

List<CurrentAccount> currentAccounts = bank.Accounts.AllCurrentAccounts.ToList();
Console.WriteLine("Before Sort:");
foreach (CurrentAccount account in currentAccounts)
    Console.WriteLine("  - " + account.Number);

currentAccounts.Sort();

Console.WriteLine("After Sort:");
foreach (CurrentAccount account in currentAccounts)
    Console.WriteLine("  - " + account.Number);


Console.WriteLine("\n############         JSON         ############\n");


Console.WriteLine("Account: " + currentAccount);
Console.WriteLine("Card: " + currentAccount.Cards.All.First());
Console.WriteLine("Customer: " + currentAccount.AccessList.First());
Base.DefaultFormatting = Formatting.None;
Console.WriteLine("\nFormatting set to None:");
Console.WriteLine("Customer: " + currentAccount.AccessList.First());


Console.WriteLine("\n############     IMMUTABILITY     ############\n");


Console.WriteLine("Length account.AccessList: " + currentAccount.AccessList.Count);
Console.Write("Added item: ");
Console.WriteLine(currentAccount.AccessList.Add(customer2));
Console.WriteLine("Length account.AccessList: " + currentAccount.AccessList.Count);

currentAccount.Cards.Remove(creditCard);


Console.WriteLine("\n############      EXCEPTIONS      ############\n");


try
{
    Console.WriteLine(currentAccount[-1]);
}
catch (Exception e)
{
    Console.WriteLine(e.GetType() + ": " + e.Message);
}

try
{
    currentAccount.RemoveCustomer(customer2);
}
catch (Exception e)
{
    Console.WriteLine(e.GetType() + ": " + e.Message);
}

try
{
    bank.Accounts.Remove(savingAccountYounger.Number);
}
catch (InvalidOperationException e)
{
    Console.WriteLine(e.GetType() + ": " + e.Message);
}

Console.WriteLine("\n############     TRANSACTION      ############\n");

currentAccount.DepositMoney(20);
Transaction transaction = Bank.Transact(currentAccount, creditCard, 15);
Console.WriteLine(transaction);
Console.WriteLine($"currentAccount.Balance: {currentAccount.Balance}");
Console.WriteLine($"creditCard.Balance: {creditCard.Balance}");

try
{
    Bank.Transact(creditCard, currentAccount, 18);
}
catch (InsufficientBalanceException e)
{
    Console.WriteLine(e.GetType() + ": " + e.Message);
}

Console.WriteLine("\n############     INTERACTIVE      ############\n");


Console.WriteLine(new Customer());

Console.WriteLine("\n############         BANK          ############\n");

Base.DefaultFormatting = Formatting.Indented;
Console.WriteLine(bank);