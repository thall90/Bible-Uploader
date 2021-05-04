using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BibleUpload.Data.Models.Interfaces;
using BibleUpload.Services.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;

namespace BibleUpload.Services
{
    public class CsvParserService : ICsvParserService
    {
        public List<TModel> GetRecords<TModel>(
            string path)
            where TModel : class, ICsvModel 
        {
            using var reader = new StreamReader(path);
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };
            using var csv = new CsvReader(reader, configuration);
            
            return csv.GetRecords<TModel>().ToList();
        }

        public List<TMappedModel> GetMappedRecords<TModel, TMappedModel>(
            string path,
            Func<List<TModel>, List<TMappedModel>> mapper)
            where TModel : class, ICsvModel
            where TMappedModel : class, IDomainModel
        {
            var models = GetRecords<TModel>(path);

            return mapper.Invoke(models);
        }
    }
}