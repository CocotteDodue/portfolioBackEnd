using System;

namespace PortfolioBackEnd.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
    }
}
