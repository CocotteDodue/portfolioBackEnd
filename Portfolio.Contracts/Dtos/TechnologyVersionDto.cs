using System;

namespace Portfolio.Contratcs.Dtos
{
    public class TechnologyVersionDto
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string MajorBuild { get; set; }
        public DateTime releaseDate { get; set; }
        public TechnologyDto Technology { get; set; }
        public int? TechnologyId { get; set; }
    }
}
