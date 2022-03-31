using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortoff.Domain.Entities;
public interface IEntity<T>
{
    T Id { get; set; }
}

public abstract class EntityBase
{
}

public abstract class Entity<T> : EntityBase, IEntity<T>
{
    public virtual T Id { get; set; }
}
