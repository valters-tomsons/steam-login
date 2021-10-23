using System;
using System.IO;
using System.Runtime.Versioning;
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

        public async void SetAutoLoginUsername(string username)
        {
            string homePath = Environment.GetEnvironmentVariable("HOME");
            string file = $"{homePath}/.steam/registry.vdf";

            if (File.Exists(file))
            {
                Console.WriteLine($"Writing '{username}' to '{file}'");

                string[] fileBuffer = await File.ReadAllLinesAsync(file).ConfigureAwait(false);

                for (int i = 0; i < fileBuffer.Length; i++)
                {
                    if (fileBuffer[i].Contains("AutoLoginUser"))
                    {
                        fileBuffer[i] = $"					\"AutoLoginUser\"		\"{username}\"";
                        Console.WriteLine(fileBuffer[i]);
                    }
                }

                await File.WriteAllLinesAsync(file, fileBuffer).ConfigureAwait(false);
            }
        }
    }
}