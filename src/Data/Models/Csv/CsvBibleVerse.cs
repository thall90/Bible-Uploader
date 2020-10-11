using BibleUpload.Data.Models.Interfaces;

namespace BibleUpload.Data.Models.Csv
{
    public class CsvBibleVerse : ICsvModel
    {
        public string Book { get; set; }

        public int Chapter { get; set; }

        public int Verse { get; set; }

        public string Content { get; set; }
    }
}