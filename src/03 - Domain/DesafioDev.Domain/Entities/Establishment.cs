using DesafioDev.Domain.Enums;

namespace DesafioDev.Domain.Entities;

public class Establishment
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public virtual Owner Owner { get; private set; }

    public virtual ICollection<Transaction> Transactions { get; private set; }

    public Establishment() { }

    public Establishment(string name, string cpf, string ownerName)
    {
        Id = Guid.NewGuid();
        SetName(name);
        SetOwner(cpf, ownerName);
        Transactions = new List<Transaction>();
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetOwner(string cpf, string name)
    {
        Owner = new Owner(cpf, name);
    }

    public void AddTransaction(TransactionType type, DateTime date, decimal value, string card, TimeSpan hour)
    {
        Transactions.Add(new Transaction(type, date, value, card, hour));
    }

    public decimal CalculateTotalBalance()
    {
        var valueEntry = CalculateTotalEntryValue();
        var valueExit = CalculateTotalExitValue();

        return valueEntry - valueExit;
    }

    public decimal CalculateTotalEntryValue()
    {
        return Transactions.Where(_ => _.NatureTransactionType == NatureTransactionTypeEnum.Entry).Sum(_ => _.Value);
    }

    public decimal CalculateTotalExitValue()
    {
        return Transactions.Where(_ => _.NatureTransactionType == NatureTransactionTypeEnum.Exit).Sum(_ => _.Value);
    }
}
