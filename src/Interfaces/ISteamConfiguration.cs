using System;
using System.Threading.Tasks;

namespace SteamLogin.Interfaces
{
    public interface ISteamConfiguration
    {
        Uri? GetSteamExecutablePath();
        Task SetAutoLoginUsername(string username);
    }
}
