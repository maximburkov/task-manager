using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.QueryParameters
{
    public abstract class QueryParameters 
    {
        public abstract bool HasValues { get; }

        public int? Page { get; set; }

        public int? PageSize { get; set; }
    }
}
