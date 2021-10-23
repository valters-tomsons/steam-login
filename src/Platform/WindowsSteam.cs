using System;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Microsoft.Win32;
using SteamLogin.Interfaces;

namespace SteamLogin.Platform
{
    [SupportedOSPlatform(platformName: "Windows")]
    public class WindowsSteam : ISteamConfiguration
    {
        public async Task SetAutoLoginUsername(string username)
        {
            using RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Valve\Steam");
            key.SetValue("AutoLoginUser", username);
            await Task.CompletedTask;
        }

        public Uri? GetSteamExecutablePath()
        {
            using RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Valve\Steam");
            var path = key?.GetValue("SteamExe")?.ToString();

            if(path is null)
            {
                return null;
            }

            return new Uri(path, UriKind.Absolute);
        }
    }
}
