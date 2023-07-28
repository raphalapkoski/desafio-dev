using DesafioDev.Application.Interfaces;
using DesafioDev.Domain.Entities;
using DesafioDev.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.Collections.ObjectModel;
using System.Globalization;

namespace DesafioDev.Application.Services;

internal class FileServices : IFileServices
{
    public ICollection<Establishment> DesserializeValuesForEstablishment(IFormFile formFile)
    {
        var lines = ReadAndConvertInListString(formFile);

        ICollection<Establishment> establishments = new Collection<Establishment>();

        foreach (var line in lines)
        {
            bool validateLines = GetInvalidLine(line);
            if (!validateLines)
            {
                establishments.Clear();
                break;
            }

            var type = GetTransactionType(line);
            var date = GetDate(line);
            var value = GetValue(line);
            var cpf = GetCpf(line);
            var card = GetCard(line);
            var hour = GetHour(line);
            var ownerName = GetOwnerName(line);
            var establishmentName = GetEstablishmentName(line);

            Establishment establishment = establishments.FirstOrDefault(_ => _.Name == establishmentName);
            if (establishment is not null)
            {
                establishments.Remove(establishment);
            }

            establishment ??= new Establishment(establishmentName, cpf, ownerName);

            establishment.AddTransaction(type, date, value, card, hour);
            establishments.Add(establishment);
        }

        return establishments;
    }

    private static List<string> ReadAndConvertInListString(IFormFile formFile)
    {
        string line;
        List<string> lines = new();

        if (formFile.ContentType == "text/plain")
        {

            using var streamReader = new StreamReader(formFile.OpenReadStream());
            while ((line = streamReader.ReadLine()) is not null)
            {
                lines.Add(line);
            }
        }

        return lines;
    }

    private static bool GetInvalidLine(string line)
    {
        return line.Length == 80;
    }

    private static TransactionType GetTransactionType(string line)
    {
        var type = line[0..1];
        var convertedInInt = Convert.ToInt16(type);
        return (TransactionType)convertedInInt;
    }

    private static DateTime GetDate(string line)
    {
        var stringDate = line[1..9];
        return DateTime.ParseExact(stringDate, "yyyyMMdd", new CultureInfo("pt-BR"));
    }

    private static decimal GetValue(string line)
    {
        var value = line[9..19];
        var convertedInDecimal = Convert.ToDecimal(value);
        return convertedInDecimal / 100;
    }

    private static string GetCpf(string line)
    {
        var cpf = line[19..30];
        return cpf;
    }

    private static string GetCard(string line)
    {
        var card = line[30..42];
        return card;
    }

    private static TimeSpan GetHour(string line)
    {
        var hourString = line[42..48];
        var hour = TimeSpan.ParseExact(hourString, "hhmmss", new CultureInfo("pt-BR"));
        return hour;
    }

    private static string GetOwnerName(string line)
    {
        var ownerName = line[48..62];
        return ownerName.TrimEnd();
    }

    private static string GetEstablishmentName(string line)
    {
        var establishmentName = line[62..80];
        return establishmentName.TrimEnd();
    }
}
