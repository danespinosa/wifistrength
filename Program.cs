using ManagedNativeWifi;
using System;
using System.Linq;
using System.Threading;

namespace WiFiStrengthApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WiFi Network Scanner");
            Console.WriteLine("===================\n");
            
            bool continuousMode = args.Contains("--watch") || args.Contains("-w");
            
            if (continuousMode)
            {
                Console.WriteLine("Continuous monitoring mode (Press Ctrl+C to exit)\n");
                while (true)
                {
                    ScanAndDisplayNetworks();
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("WiFi Network Scanner - Continuous Mode");
                    Console.WriteLine("======================================\n");
                }
            }
            else
            {
                ScanAndDisplayNetworks();
            }
        }

        static void ScanAndDisplayNetworks()
        {
            try
            {
                var availableNetworks = NativeWifi.EnumerateAvailableNetworks()
                    .OrderByDescending(network => network.SignalQuality)
                    .ToList();

                if (!availableNetworks.Any())
                {
                    Console.WriteLine("No WiFi networks found. Make sure WiFi is enabled.");
                    return;
                }

                Console.WriteLine($"Found {availableNetworks.Count} network(s):\n");
                Console.WriteLine("{0,-35} {1,-10} {2,-15} {3,-10}", 
                    "SSID", "Signal %", "Signal Bar", "Auth Type");
                Console.WriteLine(new string('-', 75));

                foreach (var network in availableNetworks)
                {
                    string ssid = network.ProfileName;
                    if (string.IsNullOrEmpty(ssid))
                        ssid = network.Ssid.ToString();
                    
                    int signalQuality = network.SignalQuality;
                    string signalBar = GetSignalBars(signalQuality);
                    string authType = network.AuthenticationAlgorithm.ToString();

                    Console.WriteLine("{0,-35} {1,-10} {2,-15} {3,-10}", 
                        TruncateString(ssid, 34),
                        signalQuality,
                        signalBar,
                        authType);
                }

                Console.WriteLine();
                var connectedNetwork = NativeWifi.EnumerateConnectedNetworkSsids().FirstOrDefault();
                if (connectedNetwork != null)
                {
                    Console.WriteLine($"Currently connected to: {connectedNetwork}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error scanning networks: {ex.Message}");
                Console.WriteLine("\nNote: This application requires administrator privileges to scan WiFi networks.");
            }
        }

        static string GetSignalBars(int signalQuality)
        {
            int bars = signalQuality switch
            {
                >= 80 => 5,
                >= 60 => 4,
                >= 40 => 3,
                >= 20 => 2,
                _ => 1
            };

            return new string('█', bars) + new string('░', 5 - bars);
        }

        static string TruncateString(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength - 3) + "...";
        }
    }
}
