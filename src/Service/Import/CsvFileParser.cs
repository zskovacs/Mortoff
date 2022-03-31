using Mortoff.Application.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using Microsoft.Extensions.Logging;

namespace Mortoff.Service.Import;
public class CsvFileParser<T> : IFileParser<T>
{
    private readonly ILogger _logger;

    public CsvFileParser(ILogger<CsvFileParser<T>> logger)
    {
        _logger = logger;
    }

    public IEnumerable<T> ParseFile(IFormFile file)
    {

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,



            BadDataFound = context =>
                _logger.LogWarning($"Bad data found at row {context.Field}\r\n" +
                         $"Raw data: {context.RawRecord}"),
            MissingFieldFound = context =>                        
                _logger.LogWarning($"Missing Field Found at line {context.Index}\r\n" +
                         $"Field at index {context.Index} does not exist\r\n" +
                         $"Raw record: {context.Context}"),
            //Delimiter = ","
        };

        using var stream = file.OpenReadStream();
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, config);

        return csv.GetRecords<T>().ToList();
    }
}
