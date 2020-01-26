using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.QueryParameters
{
    public abstract class QueryParameters 
    {
        public int? Take { get; set; }

        public int? Offset { get; set; }

        /// <summary>
        /// Defines if any parameter is defined
        /// </summary>
        /// <returns></returns>
        public abstract bool HasValues();
    }
}
