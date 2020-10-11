using BibleUpload.Data.Interfaces;
using BibleUpload.Data.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BibleUpload.Data.Context.Interfaces
{
    public interface IEsvBibleContext: IDbContext
    {
        DbSet<EsvBook> Books { get; set; }
        
        DbSet<EsvChapter> Chapters { get; set; }
        
        DbSet<EsvVerse> Verses { get; set; }
    }
}