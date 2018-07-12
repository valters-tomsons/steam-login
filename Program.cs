using System;
using System.IO;

namespace steam_autologin
{
    class Program
    {
        static string homePath = Environment.GetEnvironmentVariable("HOME");
        static string regFile = $"{homePath}/.steam/registry.vdf";

        static void Main(string[] args)
        {
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
    }
}
