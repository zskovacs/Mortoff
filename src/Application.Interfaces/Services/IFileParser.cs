using Microsoft.AspNetCore.Http;

namespace Mortoff.Application.Interfaces;
public interface IFileParser<T>
{
    IEnumerable<T> ParseFile(IFormFile file);
}
