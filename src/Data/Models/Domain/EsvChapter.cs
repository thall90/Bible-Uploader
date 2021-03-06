using System.Collections.Generic;

namespace BibleUpload.Data.Models.Domain
{
    public class EsvChapter
    {
        public int Id { get; set; }
        
        public int Number { get; set; }

        public int EsvBookId { get; set; }

        public ICollection<EsvVerse> Verses { get; set; }
    }
}