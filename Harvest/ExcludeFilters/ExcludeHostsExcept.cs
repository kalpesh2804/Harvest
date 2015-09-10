using System;
using System.Collections.Generic;
using System.Linq;

namespace Harvest.ExcludeFilters
{
    public class ExcludeHostsExcept : IExcludeFilter
    {
        private readonly IEnumerable<string> _hosts;

        public ExcludeHostsExcept(params string[] hosts)
        {
            _hosts = hosts;
        }

        public bool IsMatch(Uri url)
        {
            // Do not exclude hosts in _host
            if (_hosts.Contains(url.Host))
            {
                return false;
            }
            else if (url.Host.Count(x => x == '.') > 1 && url.Host.StartsWith("www.")) // Removing wwww from host name to compare host
            {
                String host1 = url.Host.Substring(url.Host.IndexOf('.') + 1);
                
                if (_hosts.Contains(host1))
                {
                    return false;
                }
            }

            // Exclude
            return true;
        }
    }
}
