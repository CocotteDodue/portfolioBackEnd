using System;

namespace PortfolioBackEnd.Entities
{
    public class TechnologyVersion:BaseEntity
    {
        public string NickName { get; set; }
        public string MajorBuild { get; set; }
        public DateTime releaseDate { get; set; }
    }
}
