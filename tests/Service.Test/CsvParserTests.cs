using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mortoff.Application.Interfaces;
using Mortoff.Domain.Import;
using Mortoff.Service.Import;
using NSubstitute;
using System.Text;

namespace Mortoff.Service.Test;

[TestClass]
public class CsvParserTests : TestBase
{
    private IFileParser<StockCsvRecord> service;

    [TestInitialize]
    public void Setup()
    {
        var logger = Substitute.For<ILogger<CsvFileParser<StockCsvRecord>>>();

        service = Substitute.ForPartsOf<CsvFileParser<StockCsvRecord>>(logger);
    }

    [TestMethod]
    public void ParseFile_Ok()
    {
        //Arrange
        var bytes = Encoding.UTF8.GetBytes("Date,Open,High,Low,Close,Volume\n2010-06-29,3.8,5,3.508,4.778,93916380");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "import.csv");

        //Act
        var result = service.ParseFile(file);

        //Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
    }
}
