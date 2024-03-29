﻿namespace Mortoff.Domain.Entities;

public class StockEntity : Entity<int>
{
    public StockEntity()
    {
        Datas = new HashSet<DataEntity>();
    }

    public string Name { get; set; }

    //public string Ticker { get; set; }

    public ICollection<DataEntity> Datas { get; set; }
}
