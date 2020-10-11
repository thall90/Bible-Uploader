using System.Collections.Generic;
using BibleUpload.Data.Models.Interfaces;

namespace BibleUpload.Data.Models.Domain
{
    public class BibleBookChapter : IDomainModel
    {
        public object _id { get; set; }

        public string Book { get; set; }

        public int Chapter { get; set; }

        public ICollection<BibleVerse> Verses { get; set; }
    }
}