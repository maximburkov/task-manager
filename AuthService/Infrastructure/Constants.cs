using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Infrastructure
{
    public static class Constants
    {
        public const string KeyRegex = "^[^#?/\\\\\t\n\r]*$";

        public const string NotAllowedRegexMessage = "Following characters are not allowed for this filed: \\, /, #, ?, \\t, \\n, \\r";
    }
}
