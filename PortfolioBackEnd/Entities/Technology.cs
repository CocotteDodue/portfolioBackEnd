using System.Collections.Generic;

namespace PortfolioBackEnd.Entities
{
    public class Technology: BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<TechnologyVersion> Versions { get; set; }
    }
}
