using System;
using System.Diagnostics;
using SteamLogin.Interfaces;

namespace SteamLogin
{
    public class SteamLogin
    {
        private readonly ISteamConfiguration _steamConfig;

        public SteamLogin(ISteamConfiguration steamConfig)
        {
            _steamConfig = steamConfig;
        }

        public void Login(string username)
        {
            _steamConfig.SetAutoLoginUsername(username);

            TerminateSteam();

            var steamPath = _steamConfig.GetSteamExecutablePath();

            if(steamPath is null)
            {
                Console.WriteLine("Failed to find Steam executable, not launching...");
                return;
            }

            StartSteam(steamPath);
        }

        private void TerminateSteam()
        {
            foreach (Process proc in Process.GetProcessesByName("steam"))
            {
                Console.WriteLine($"Killing steam process {proc.Id}");
                proc.Kill();
            }
        }

        private void StartSteam(Uri path)
        {
            var startInfo = new ProcessStartInfo(path.LocalPath);
            // startInfo.ArgumentList.Add("-silent");

            Process proc = new Process()
            {
                StartInfo = startInfo
            };

            proc.Start();
        }
    }
}