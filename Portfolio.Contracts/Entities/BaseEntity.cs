using System;

namespace Portfolio.Contracts.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
