namespace BibleUpload.Data.Models.Domain
{
    public class EsvVerse
    {
        public int Id { get; set; }
        
        public int Number { get; set; }

        public int EsvChapterId { get; set; }

        public string Value { get; set; }
    }
}