using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BibleUpload.Data.Context.Interfaces;
using BibleUpload.Data.ModelMapping;
using BibleUpload.Data.Models.Csv;
using BibleUpload.Data.Models.Domain;
using BibleUpload.Infrastructure.Constants;
using BibleUpload.Infrastructure.Interfaces;
using BibleUpload.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibleUpload
{
    public class ServiceRunner : IServiceRunner
    {
        private readonly ICsvParserService _parserService;
        private readonly IEsvBibleContext _esvBibleContext;

        public ServiceRunner(
            ICsvParserService parserService,
            IEsvBibleContext esvBibleContext)
        {
            _parserService = parserService;
            _esvBibleContext = esvBibleContext;
        }

        public async Task Run(
            CancellationToken cancellationToken = default)
        {
            var models = _parserService.GetRecords<CsvBibleVerse>(Paths.CsvPath);
            
            await _esvBibleContext.Database.EnsureCreatedAsync(cancellationToken);

            await AddEsvBooksToDb(
                models,
                cancellationToken);

            var bookLookup = await RetrieveBookLookup(cancellationToken);

            await AddEsvChaptersToDb(
                bookLookup,
                models,
                cancellationToken);

            await AddEsvVersesToDb(
                bookLookup,
                models,
                cancellationToken);
        }

        private async Task AddEsvBooksToDb(
            IEnumerable<CsvBibleVerse> models,
            CancellationToken cancellationToken)
        {
            var books = models.ToBookList();

            // This is a HACK:
            //
            // Obviously, we would never do this in
            // a production environment, as it would
            // be super slow, bad practice, etc. 
            // 
            // We're doing it here, however, because
            // this is NOT a production environment, 
            // and I don't feel like figuring out a better way
            // to add these entries so that Ids are
            // created automatically in their natural order :)
            foreach (var esvBook in books)
            {
                await _esvBibleContext.AddAsync(
                    esvBook,
                    cancellationToken);

                await _esvBibleContext.SaveChangesAsync(cancellationToken);
            }
        }

        private async Task AddEsvChaptersToDb(
            IDictionary<string, EsvBook> bookLookup,
            IEnumerable<CsvBibleVerse> models,
            CancellationToken cancellationToken)
        {
            var chapters = models.ToChapterList(bookLookup);
            await _esvBibleContext.Chapters.AddRangeAsync(
                chapters,
                cancellationToken);
            await _esvBibleContext.SaveChangesAsync(cancellationToken);
        }

        private async Task<IDictionary<string, EsvBook>> RetrieveBookLookup(
            CancellationToken cancellationToken)
        {
            var bookLookup = await _esvBibleContext.Books
                .ToDictionaryAsync(
                    x => x.Name,
                    cancellationToken);

            return bookLookup;
        }

        private async Task AddEsvVersesToDb(
            IDictionary<string, EsvBook> bookLookup,
            IEnumerable<CsvBibleVerse> models,
            CancellationToken cancellationToken)
        {
            var chapterLookup = await RetrieveChapterLookup(cancellationToken);
            var verses = models.ToVerseList(bookLookup, chapterLookup);
            
            await _esvBibleContext.Verses.AddRangeAsync(verses, cancellationToken);
            await _esvBibleContext.SaveChangesAsync(cancellationToken);
        }

        private async Task<IDictionary<(int bookId, int number), EsvChapter>> RetrieveChapterLookup(
            CancellationToken cancellationToken)
        {
            var bookLookup = await _esvBibleContext.Chapters
                .ToDictionaryAsync(x => (x.EsvBookId, x.Number), cancellationToken);

            return bookLookup;
        }
    }
}