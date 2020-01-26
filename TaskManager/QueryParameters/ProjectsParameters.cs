using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.QueryParameters
{
    public class ProjectsParameters : QueryParameters
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }

        public override bool HasValues() => !string.IsNullOrWhiteSpace(Code) || !string.IsNullOrWhiteSpace(Name) ||
                                          !string.IsNullOrWhiteSpace(Id) || !Take.HasValue || Offset.HasValue;

        /// <summary>
        /// Defines if parameters has only key-values
        /// </summary>
        /// <returns></returns>
        public bool HasOnlyKeys() => !string.IsNullOrWhiteSpace(Id) && !string.IsNullOrWhiteSpace(Code) 
            && !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Id) && !Take.HasValue && Offset.HasValue;
    }
}
