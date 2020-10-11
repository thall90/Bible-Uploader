using System;
using System.Collections.Generic;
using BibleUpload.Data.Models.Interfaces;

namespace BibleUpload.Services.Interfaces
{
    public interface ICsvParserService
    {
        List<TModel> GetRecords<TModel>(
            string path)
            where TModel : class, ICsvModel;

        public List<TMappedModel> GetMappedRecords<TModel, TMappedModel>(
            string path,
            Func<List<TModel>, List<TMappedModel>> mapper)
            where TModel : class, ICsvModel
            where TMappedModel : class, IDomainModel;
    }
}