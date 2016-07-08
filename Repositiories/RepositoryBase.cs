using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Arnis.Web.Repositiories
{
    public abstract class RepositoryBase
    {
        private const string EndpointUri = "https://arnisdb.documents.azure.com:443/";
        private const string PrimaryKey = "Qz1iWZxei7SN7CKlkUB8rEf4ECHjDtMjdtQcjXEQbDKEr5gDqlocsLXdqQ2EyIj17EEIcwyZ0VIy33uCmyM75g==";
        private DocumentClient _client;

        protected RepositoryBase()
        {
            InitializeDB().Wait();
        }

        protected string Database => "arnisdb";
        protected DocumentClient Client => _client;

        private async Task InitializeDB()
        {
            this._client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
        }        
    }
}