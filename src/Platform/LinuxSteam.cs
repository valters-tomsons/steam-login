using System;
using System.IO;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using SteamLogin.Interfaces;

namespace SteamLogin.Platform
{
    [SupportedOSPlatform(platformName: "Linux")]
    public class LinuxSteam : ISteamConfiguration
    {
        public Uri GetSteamExecutablePath()
        {
            return new Uri("/usr/bin/steam", UriKind.Absolute);
        }

        public async Task SetAutoLoginUsername(string username)
        {
            var homePath = Environment.GetEnvironmentVariable("HOME");

            if(homePath is null)
            {
                return;
            }

            var filePath = $"{homePath}/.steam/registry.vdf";

            if (File.Exists(filePath))
            {
                Console.WriteLine($"Writing '{username}' to '{filePath}'");

                string[] fileBuffer = await File.ReadAllLinesAsync(filePath).ConfigureAwait(false);

                for (int i = 0; i < fileBuffer.Length; i++)
                {
                    if (fileBuffer[i].Contains("AutoLoginUser"))
                    {
                        fileBuffer[i] = $"					\"AutoLoginUser\"		\"{username}\"";
                        Console.WriteLine(fileBuffer[i]);
                    }
                }

                await File.WriteAllLinesAsync(filePath, fileBuffer).ConfigureAwait(false);
            }
        }
    }
}