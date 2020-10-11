using System;
using System.IO;

namespace BibleUpload.Infrastructure.Constants
{
    public static class Paths
    {
        public static readonly DirectoryInfo BasePath = new DirectoryInfo(Environment.CurrentDirectory)
            .Parent?
            .Parent?
            .Parent;

        public static readonly string CsvPath = Path.Combine(BasePath.FullName, "Resources", "esv_all.csv");
    }
}