using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortoff.Application.Interfaces;

public interface IAppdDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
