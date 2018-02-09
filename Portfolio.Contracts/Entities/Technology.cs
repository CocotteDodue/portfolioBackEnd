using System.Collections.Generic;

namespace Portfolio.Contracts.Entities
{
    public class Technology: BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<TechnologyVersion> Versions { get; set; }
    }
}
