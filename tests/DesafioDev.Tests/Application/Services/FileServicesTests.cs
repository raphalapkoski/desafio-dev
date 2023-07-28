using DesafioDev.Application.Services;
using DesafioDev.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Collections.ObjectModel;

namespace DesafioDev.Tests.Application.Services
{
    public class FileServicesTests
    {
        readonly FileServices _fileServices;

        public FileServicesTests()
        {
            _fileServices = new FileServices();
        }

        [Fact]
        public void DesserializeValuesForEstablishment_Return_Collection_With_Success()
        {
            var formFile = CreateFormFile(true);

            var result = _fileServices.DesserializeValuesForEstablishment(formFile);

            result.Should().NotBeNullOrEmpty()
                           .And.HaveCount(1)
                           .And.BeOfType<Collection<Establishment>>();
        }

        [Fact]
        public void DesserializeValuesForEstablishment_Return_Collection_With_Not_Success()
        {
            var formFile = CreateFormFile(false);

            var result = _fileServices.DesserializeValuesForEstablishment(formFile);
            
            result.Should().BeNullOrEmpty()
                           .And.HaveCount(0)
                           .And.BeOfType<Collection<Establishment>>();
        }

        private static FormFile CreateFormFile(bool valid)
        {
            var content = valid
                ? "0201903010000000000000000000000000****000000000000000000000000000000000000000000"
                : "0201903010000000000000000000000000****00000000000000000000000000000000000000000";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();

            return new FormFile(stream, 0, stream.Length, null, "test.txt");
        }
    }
}
