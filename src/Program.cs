using System;
using System.Runtime.InteropServices;
using SteamLogin.Interfaces;
using SteamLogin.Platform;

namespace SteamLogin
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if(args.Length < 1 || string.IsNullOrWhiteSpace(args[0]))
            {
                Console.WriteLine("No username given");
                return;
            }

            var username = args[0];
            Console.WriteLine($"Logging in {username}");

            ISteamConfiguration steamConfig = ResolvePlatform();
            var app = new SteamLogin(steamConfig);
            app.Login(username);
        }

        private static ISteamConfiguration ResolvePlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return new LinuxSteam();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new WindowsSteam();
            }

            throw new PlatformNotSupportedException();
        }
    }

}