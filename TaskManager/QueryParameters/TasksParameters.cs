using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.QueryParameters
{
    public class TasksParameters : QueryParameters
    {
        public string Id { get; set; }

        public string Subject { get; set; }

        public string ProjectId { get; set; }

        public override bool HasValues() => !string.IsNullOrWhiteSpace(Id) || !string.IsNullOrWhiteSpace(ProjectId) ||
                                            !string.IsNullOrWhiteSpace(Subject) 
                                            || Take.HasValue;

        public override bool HasOnlyKeys() => !string.IsNullOrWhiteSpace(Id) && !string.IsNullOrWhiteSpace(ProjectId) &&
                                              string.IsNullOrWhiteSpace(Subject) &&
                                              !Take.HasValue;
    }
}
