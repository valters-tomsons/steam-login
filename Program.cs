using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace steam_login
{
    internal static class Program
    {
        private static readonly string homePath = Environment.GetEnvironmentVariable("HOME");
        private static readonly string regFile = $"{homePath}/.steam/registry.vdf";

        private static void Main(string[] args)
        {
            if(!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Console.WriteLine("This program works only under Linux.");
                return;
            }

            string username;

            if(args.Length == 0)
            {
                Console.WriteLine("Enter Steam username to login with: ");
                username = Console.ReadLine();
            }
            else
            {
                username = args[0];
            }

            if(username.Length > 3)
            {
                WriteUsername(username, regFile);

                var runNative = false;

                if (args[1] != null)
                {
                    runNative = args[1].ToLower().Equals("native");
                }

                RestartSteam(runNative);
            }
            else
            {
                Console.WriteLine("Username too short.");
            }
        }

        private static void WriteUsername(string username, string file)
        {
            if(File.Exists(file))
            {
                Console.WriteLine($"Writing '{username}' to '{file}'");

                string[] fileBuffer = File.ReadAllLines(file);

                for(int i = 0; i < fileBuffer.Length; i++)
                {
                    if(fileBuffer[i].Contains("AutoLoginUser"))
                    {
                        fileBuffer[i] = $"					\"AutoLoginUser\"		\"{username}\"";
                        Console.WriteLine(fileBuffer[i]);
                    }
                }

                File.WriteAllLines(file, fileBuffer);

                Console.WriteLine($"{file} written!");
            }
            else
            {
                Console.WriteLine($"'{file}' doesn't exist!");
                Console.WriteLine("Nothing was written.");
            }
        }

        private static void RestartSteam(bool native = false)
        {
            TerminateSteam();
            StartSteam(native);
        }

        private static void TerminateSteam()
        {
            foreach(Process proc in Process.GetProcessesByName("steam"))
            {
                System.Console.WriteLine($"Killing process PID: {proc.Id}");
                proc.Kill();
            }
        }

        private static void StartSteam(bool native = false)
        {
            string steamFlavor = "runtime";
            if(native)
            {
                steamFlavor = "native";
            }

            //Start steam detached from terminal and without output
            Process proc = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"nohup steam-{steamFlavor} > /dev/null &\"",
                }
            };

            proc.Start();
        }
    }
}
