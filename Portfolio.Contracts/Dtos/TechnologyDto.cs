using System.Collections.Generic;

namespace Portfolio.Contratcs.Dtos
{
    public class TechnologyDto
    {
        public string Name { get; set; }
        public IEnumerable<TechnologyVersionDto> Versions { get; set; }
    }
}
