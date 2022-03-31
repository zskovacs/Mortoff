using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortoff.Application.Common.Validators;
internal class FileValidator : AbstractValidator<IFormFile>
{
    public FileValidator()
    {
        RuleFor(x => x).NotNull();
    }

    public FileValidator(long fileSizeInBytes, List<string> allowedMimeType)
    {
        RuleFor(x => x).NotNull().WithMessage("Nem található a feltöltött file");
        RuleFor(x => x.Length).ExclusiveBetween(0, fileSizeInBytes).WithMessage($"A file mérete meghaladja a maximum megengedettet: {fileSizeInBytes / 1024 / 1024} MB");
        RuleFor(x => x.ContentType).NotNull().Must(x => allowedMimeType.Contains(x)).WithMessage($"Csak a megengedett file típusok engedélyezettek: {string.Join(",", allowedMimeType)}");
    }
}
