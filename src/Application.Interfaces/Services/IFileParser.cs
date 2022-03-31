using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortoff.Application.Interfaces;
public interface IFileParser<T>
{
    IEnumerable<T> ParseFile(IFormFile file);
}
