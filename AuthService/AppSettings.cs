using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService
{
    public class AppSettings
    {
        public Logging Logging { get; set; }

        public Auth Auth { get; set; }
    }


    public class Logging
    {
        public bool IncludeScopes { get; set; }
        public Loglevel LogLevel { get; set; }
    }

    public class Auth
    {
        public string Secret { get; set; }

        public int Lifetime { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string System { get; set; }
        public string Microsoft { get; set; }
    }
}
