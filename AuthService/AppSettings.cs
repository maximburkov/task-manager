using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService
{
    public class AppSettings
    {
        public Logging Logging { get; set; }

        public string SecretKey { get; set; }

        public Auth Auth { get; set; }

        public string Salt { get; set; }
    }


    public class Logging
    {
        public bool IncludeScopes { get; set; }
        public Loglevel LogLevel { get; set; }
    }

    public class Auth
    {
        /// <summary>
        /// Access Token Lifetime in minutes
        /// </summary>
        public int AccessTokenLifetime { get; set; }

        /// <summary>
        /// Rferesh Token Lifetime in minutes
        /// </summary>
        public int RefreshTokenLifetime { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string System { get; set; }
        public string Microsoft { get; set; }
    }
}
