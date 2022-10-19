using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Vtd.Account.RestService.Lib.Infra.Bootstrap
{
    public static class ConfigExtensions
    {
        public static IConfigurationBuilder AddCombinedConfig(this IConfigurationBuilder builder,
            string environmentName)
        {
            string sharedConfigPath = FindSharedConfig(AppContext.BaseDirectory);
            var commonSettings = Path.Combine(sharedConfigPath, "commonSettings.json");
            var commonEnvSettings = Path.Combine(sharedConfigPath, $"commonSettings.{environmentName}.json");
            var appSettings = "appsettings.json";

            builder.AddJsonFile(commonSettings, optional: true)
                .AddJsonFile(commonEnvSettings, optional: true)
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(appSettings, optional: true);

            return builder;
        }

        public static IConfigurationBuilder AddCombinedConfig(this IConfigurationBuilder builder,
            IHostingEnvironment env)
        {
            string sharedConfigPath = FindSharedConfig(AppContext.BaseDirectory);
            var commonSettings = Path.Combine(sharedConfigPath, "commonSettings.json");
            var commonEnvSettings = Path.Combine(sharedConfigPath, $"commonSettings.{env.EnvironmentName}.json");
            var appSettings = "appsettings.json";

            builder.AddJsonFile(commonSettings, optional: true)
                .AddJsonFile(commonEnvSettings, optional: true)
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(appSettings, optional: true);

            return builder;
        }

        public static string FindSharedConfig(string childPath)
        {
            string gitRoot = FindGitRoot(childPath);
            string configDir = Path.Combine(gitRoot ?? string.Empty, "C:\\DotNetCoreProjects\\MicroServiceProjects\\VehicleTestDrive.API\\" + $"VehicleTestDrive.All{Path.DirectorySeparatorChar}config");
            return configDir;
        }

        public static string FindGitRoot(string dir)
        {
            var di = new DirectoryInfo(dir);

            while (true)
            {
                string gitDir = Path.Combine(di.FullName, ".git");

                if (Directory.Exists(gitDir))
                    break; // Found the root

                di = di.Parent;

                if (di == null)
                    break; // Traverse to root
            }

            return di?.FullName;
        }
    }
}
