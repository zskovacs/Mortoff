using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortoff.Application.Stock.Models;
public class StockListViewModel
{
    public string Name { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
}
