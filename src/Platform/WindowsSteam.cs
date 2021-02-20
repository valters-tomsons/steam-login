using System;
using Microsoft.Win32;
using SteamLogin.Interfaces;

namespace SteamLogin.Platform
{
    public class WindowsSteam : ISteamConfiguration
    {
        public void SetAutoLoginUsername(string username)
        {
            using RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Valve\Steam");
            key.SetValue("AutoLoginUser", username);
        }

        public Uri GetSteamExecutablePath()
        {
            using RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Valve\Steam");
            var path = key.GetValue("SteamExe").ToString();
            return new Uri(path, UriKind.Absolute);
        }
    }
}
