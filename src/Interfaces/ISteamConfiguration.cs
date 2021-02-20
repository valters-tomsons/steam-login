using System;

namespace SteamLogin.Interfaces
{
    public interface ISteamConfiguration
    {
        Uri GetSteamExecutablePath();
        void SetAutoLoginUsername(string username);
    }
}
