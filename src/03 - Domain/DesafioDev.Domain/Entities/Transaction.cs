using DesafioDev.Domain.Enums;

namespace DesafioDev.Domain.Entities;

public class Transaction
{
    public Guid Id { get; private set; }

    public TransactionType Type { get; private set; }

    public DateTime Date { get; private set; }

    public decimal Value { get; private set; }        

    public string Card { get; private set; }

    public TimeSpan Hour { get; private set; }

    public virtual Establishment Establishment { get; private set; }

    public NatureTransactionTypeEnum NatureTransactionType
        => GetNatureTransactionType();

    public Transaction()
    {
    }

    public Transaction(TransactionType type, DateTime date, decimal value, string card, TimeSpan hour)
    {
        Id = Guid.NewGuid();
        SetType(type);
        SetDate(date);
        SetValue(value);
        SetCard(card);
        SetHour(hour);
    }

    public void SetType(TransactionType type)
    {
        Type = type;
    }

    public void SetDate(DateTime date)
    {
        Date = date;
    }

    public void SetValue(decimal value)
    {
        Value = value;
    }

    public void SetCard(string card) 
    {
        Card = card;
    }

    public void SetHour(TimeSpan hour) 
    {
        Hour = hour;
    }

    private NatureTransactionTypeEnum GetNatureTransactionType()
    {
        if (Type == TransactionType.Ticket || Type == TransactionType.Financing || Type == TransactionType.Rent)
            return NatureTransactionTypeEnum.Exit;

        return NatureTransactionTypeEnum.Entry;
    }
}