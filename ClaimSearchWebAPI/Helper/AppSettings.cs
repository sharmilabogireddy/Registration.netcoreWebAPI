using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaimSearchWebAPI.Helper
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string Salt { get; set; }
    }
}
