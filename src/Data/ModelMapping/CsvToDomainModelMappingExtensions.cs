using System.Collections.Generic;
using System.Linq;
using BibleUpload.Data.Models.Csv;
using BibleUpload.Data.Models.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BibleUpload.Data.ModelMapping
{
    public static class CsvToDomainModelMappingExtensions
    {
        public static List<BibleBookChapter> ToBibleBookChapters(
            this ICollection<CsvBibleVerse> csvBibleVerses)
        {
            var books = csvBibleVerses.GroupBy(
                verse => (verse.Book, verse.Chapter),
                (tuple, verses) => tuple.ToBibleBookChapter(verses))
                .ToList();
            
            return books;
        }

        public static BibleBookChapter ToBibleBookChapter(
            this (string book, int chapter) bookChapterTuple,
            IEnumerable<CsvBibleVerse> csvBibleVerse)
        {
            var (book, chapter) = bookChapterTuple;

            return new BibleBookChapter
            {
                Book = book,
                Chapter = chapter,
                Verses = csvBibleVerse.Select(x => x.ToBibleVerse()).ToList(),
            };
        }

        public static BibleVerse ToBibleVerse(
            this CsvBibleVerse csvBibleVerse)
        {
            return new BibleVerse
            {
                Verse = csvBibleVerse.Verse,
                Text = csvBibleVerse.Content,
            };
        }

        public static List<EsvBook> ToBookList(
            this IEnumerable<CsvBibleVerse> csvBibleVerses)
        {
            var bookNames = csvBibleVerses
                .Select(x => x.Book)
                .Distinct();

            return bookNames
                .Select(x => new EsvBook { Name = x })
                .ToList();
        }

        public static List<EsvChapter> ToChapterList(
            this IEnumerable<CsvBibleVerse> csvBibleVerses,
            IDictionary<string, EsvBook> bookLookup)
        {
            var chapterBookTupleSet = csvBibleVerses.Select(x => (x.Book, x.Chapter))
                .ToHashSet();
            
            var chapters = chapterBookTupleSet.Select(
                x => new EsvChapter
                {
                    Number = x.Chapter,
                    EsvBookId = bookLookup[x.Book].Id,
                });

            return chapters.ToList();
        }

        public static List<EsvVerse> ToVerseList(
            this IEnumerable<CsvBibleVerse> csvBibleVerses,
            IDictionary<string, EsvBook> bookLookup,
            IDictionary<(int bookId, int number), EsvChapter> chapterLookup)
        {
            var verses = csvBibleVerses.Select(
                x => new EsvVerse
                {
                    Number = x.Verse,
                    EsvChapterId = chapterLookup[(bookLookup[x.Book].Id, x.Chapter)].Id,
                    Value = x.Content,
                });

            return verses.ToList();
        }
    }
}