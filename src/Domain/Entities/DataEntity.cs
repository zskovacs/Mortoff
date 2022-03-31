using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortoff.Domain.Entities;
public class DataEntity : Entity<long>
{
    public DateTime Date { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }

    public long Volume { get; set; }


    public int StockId { get; set; }
    public StockEntity Stock { get; set; }
}
