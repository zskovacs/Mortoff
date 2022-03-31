using AutoMapper;
using Mortoff.Domain.Entities;
using Mortoff.Domain.Import;

namespace Mortoff.Application.Stock;
internal class StockMapper : Profile
{
    public StockMapper()
    {
        CreateMap<StockCsvRecord, DataEntity>();
    }
}
