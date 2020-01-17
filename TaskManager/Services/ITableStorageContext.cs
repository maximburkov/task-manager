using Microsoft.WindowsAzure.Storage;

namespace TaskManager.Services
{
    public interface ITableStorageContext
    {
        public CloudStorageAccount StorageAccount { get; set; }
    }
}