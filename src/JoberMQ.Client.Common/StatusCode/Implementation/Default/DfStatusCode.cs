using JoberMQ.Client.Common.Database.Factories;
using JoberMQ.Client.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Client.Common.StatusCode.Abstraction;
using JoberMQ.Client.Common.StatusCode.Enums;
using JoberMQ.Client.Common.StatusCode.Models;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace JoberMQ.Client.Common.StatusCode.Implementation.Default
{
    internal class DfStatusCode : IStatusCode
    {
        private readonly IMemRepository<string, StatusCodeModel> memRepo;
        private readonly StatusCodeMessageLanguageEnum statusCodeMessageLanguage;
        public DfStatusCode(ConcurrentDictionary<string, StatusCodeModel> statusCodeData, StatusCodeMessageLanguageEnum defaultStatusCodeMessageLanguage)
        {
            this.memRepo = MemFactory.CreateMem<string, StatusCodeModel>(Database.Enums.MemFactoryEnum.Default, Database.Enums.MemDataFactoryEnum.Data, statusCodeData);
            this.statusCodeMessageLanguage = defaultStatusCodeMessageLanguage;
        }

        public string GetStatusMessage(string statusCode) => GetStatusMessage(statusCode, statusCodeMessageLanguage);
        public string GetStatusMessage(string statusCode, StatusCodeMessageLanguageEnum language)
        {
            try
            {
                var message = memRepo.Get(statusCode).StatusCodeMessages.FirstOrDefault(x => x.Language == language).Message;
                return $"{statusCode} - {message}";
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
