using System;

namespace CryptoNews.DAL.Entities
{
    public abstract class BaseEntity<T> : IBaseEntity<T>
    {
        public T Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<Guid>
    { }
}
