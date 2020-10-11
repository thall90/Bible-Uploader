using System.Collections.Generic;

namespace BibleUpload.Data.Models.Domain
{
    public class EsvBook
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<EsvChapter> Chapters { get; set; }
    }
}