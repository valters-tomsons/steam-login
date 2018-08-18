using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace steam_autologin
{
    class Program
    {
        static string homePath = Environment.GetEnvironmentVariable("HOME");
        static string regFile = $"{homePath}/.steam/registry.vdf";

        static void Main(string[] args)
        {
            if(!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                System.Console.WriteLine("This program works only under Linux.");
                return;
            }

            string username;

            if(args.Length == 0)
            {
                Console.WriteLine("Enter Steam username to login with: ");
                username = Console.ReadLine();
            }
            else{
                username = args[0];
            }

            if(username.Length > 3)
            {
                WriteUsername(username, regFile);
                RestartSteam();
            }
            else{
                System.Console.WriteLine("Username too short.");
            }

            
        }

        static void WriteUsername(string username, string file)
        {
            if(File.Exists(file))
            {
                System.Console.WriteLine($"Writing '{username}' to '{file}'");

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

                System.Console.WriteLine($"{file} written!");
            }
            else{
                System.Console.WriteLine($"'{file}' doesn't exist!");
                System.Console.WriteLine("Nothing was written.");
            }
        }

        static void RestartSteam()
        {
            TerminateSteam();
            StartSteam();
        }

        static void TerminateSteam()
        {
            foreach(Process proc in Process.GetProcessesByName("steam"))
            {
                System.Console.WriteLine($"Killing process PID: {proc.Id}");
                proc.Kill();
            }
        }

        static void StartSteam(bool native = false)
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
