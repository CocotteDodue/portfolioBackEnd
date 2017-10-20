using System.Collections.Generic;

namespace PortfolioBackEnd.DTO
{
    public class TechnologyDto
    {
        public string Name { get; set; }
        public IEnumerable<TechnologyVersionDto> Versions { get; set; }
    }
}
