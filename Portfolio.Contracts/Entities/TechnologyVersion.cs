using System;

namespace Portfolio.Contracts.Entities
{
    public class TechnologyVersion: BaseEntity
    {
        public string NickName { get; set; }
        public string MajorBuild { get; set; }
        public DateTime releaseDate { get; set; }
        public int TechnologyId { get; set; }
        public Technology Technology { get; set; }
    }
}
