using Bogus;
using Bogus.Extensions.Brazil;
using DesafioDev.Domain.Entities;
using DesafioDev.Domain.Enums;
using DesafioDev.Tests.Extensions;
using FluentAssertions;

namespace DesafioDev.Tests.Domain.Models
{
    public class EstablishmentTests
    {
        [Fact]
        public void Establishment_Add_Transaction()
        {
            var establishment = CreateEstablishment().Generate();

            establishment.AddTransaction(TransactionType.Financing, DateTime.Now, 2000, "Name", TimeSpan.MinValue);

            establishment.Transactions
                         .Should().NotBeNullOrEmpty()
                         .And.HaveCount(5);
        }

        [Fact]
        public void Establishment_Calculate_Total_Entry()
        {
            var establishment = CreateEstablishment().Generate();

            var result = establishment.CalculateTotalEntryValue();

            result.Should().Be(5000);
        }

        [Fact]
        public void Establishment_Calculate_Total_Exit()
        {
            var establishment = CreateEstablishment().Generate();

            var result = establishment.CalculateTotalExitValue();

            result.Should().Be(2100);
        }

        [Fact]
        public void Establishment_Calculate_Total_Balance()
        {
            var establishment = CreateEstablishment().Generate();

            var result = establishment.CalculateTotalBalance();

            result.Should().Be(2900);
        }

        private static Faker<Establishment> CreateEstablishment()
        {
            return new Faker<Establishment>()
                       .WithRecord()
                       .RuleFor(_ => _.Name, _ => _.Company.CompanyName())
                       .RuleFor(_ => _.Owner, _ => new Owner(_.Person.Cpf(), _.Person.FullName))
                       .RuleFor(_ => _.Transactions, _ => new List<Transaction>
                       {
                           new Transaction(TransactionType.Debit, _.Date.Past(5), 2000, _.Name.Random.ToString(), _.Date.Timespan()),
                           new Transaction(TransactionType.Debit, _.Date.Past(5), 3000, _.Name.Random.ToString(), _.Date.Timespan()),
                           new Transaction(TransactionType.Financing, _.Date.Past(5), 1000, _.Name.Random.ToString(), _.Date.Timespan()),
                           new Transaction(TransactionType.Financing, _.Date.Past(5), 1100, _.Name.Random.ToString(), _.Date.Timespan())
                       });
        }
    }
}
