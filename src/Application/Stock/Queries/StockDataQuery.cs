using Mortoff.Application.Stock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortoff.Application.Stock.Queries;
public record StockDataQuery(string Name) : IRequest<StockDataViewModel>;



