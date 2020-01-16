using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace TaskManager.Services
{
    public class CosmosDBContext : IDbContext
    {
        private string _connectionString;
        private CloudStorageAccount _storageAccount;

        public CosmosDBContext(string connectionString)
        { 
            _connectionString = connectionString;
            _storageAccount = CloudStorageAccount.Parse(connectionString);
        }
    }
}
