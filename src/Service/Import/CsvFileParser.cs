using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mortoff.Application.Interfaces;
using Mortoff.Domain.Exceptions;
using System.Globalization;

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

        csv.Read();
        csv.ReadHeader();
        try
        {
            csv.ValidateHeader<T>();
        }
        catch (HeaderValidationException e)
        {
            throw new BadRequestException($"Hiányzó header: {string.Join(",", e.InvalidHeaders.SelectMany(x => x.Names))}");
        }

        return csv.GetRecords<T>().ToList();
    }
}
