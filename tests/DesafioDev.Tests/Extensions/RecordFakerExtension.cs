using Bogus;
using System.Runtime.Serialization;

namespace DesafioDev.Tests.Extensions;

public static class RecordFakerExtension
{
    public static Faker<T> WithRecord<T>(this Faker<T> faker) where T : class
    {
        return faker.CustomInstantiator(_ => FormatterServices.GetUninitializedObject(typeof(T)) as T);
    }
}

