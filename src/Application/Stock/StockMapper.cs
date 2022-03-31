using AutoMapper;
using Mortoff.Domain.Entities;
using Mortoff.Domain.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortoff.Application.Stock;
internal class StockMapper : Profile
{
    public StockMapper()
    {
        CreateMap<StockCsvRecord, DataEntity>();        
    }
}
