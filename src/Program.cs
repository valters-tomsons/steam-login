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
            ISteamConfiguration steamConfig = null;

            if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                steamConfig = new LinuxSteam();
            }
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                steamConfig = new WindowsSteam();
            }

            if(args.Length < 1 || string.IsNullOrWhiteSpace(args[0]))
            {
                Console.WriteLine("No username given");
                return;
            }

            var username = args[0];
            Console.WriteLine($"Logging in {username}");

            var app = new SteamLogin(steamConfig);
            app.Login(username);
        }
    }
}