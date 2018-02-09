using System.Collections.Generic;

namespace Portfolio.Api.DTO
{
    public class TechnologyDto
    {
        public string Name { get; set; }
        public IEnumerable<TechnologyVersionDto> Versions { get; set; }
    }
}
