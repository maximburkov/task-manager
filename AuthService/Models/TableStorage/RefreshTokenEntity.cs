using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models.TableStorage
{
    public class RefreshTokenEntity : TableEntity
    {
        public DateTime ExcpirationDate { get; set; }
    }
}
