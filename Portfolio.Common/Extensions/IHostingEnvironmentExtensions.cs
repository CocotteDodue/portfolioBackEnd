using Microsoft.AspNetCore.Hosting;
using System;

namespace Portfolio.Common.Extensions
{
    public static class IHostingEnvironmentExtensions
    {
        public static bool IsAnyProductionEnv(this IHostingEnvironment env)
        {
            return env.EnvironmentName.IndexOf("prod", StringComparison.InvariantCultureIgnoreCase) > -1;
        }
    }
}
