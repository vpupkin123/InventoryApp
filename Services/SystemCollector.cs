using System;
using System.Collections.Generic;
using InventoryApp.Models;

namespace InventoryApp.Services
{
    public static class SystemCollector
    {
        /// <summary>
        /// Collects all system information
        /// </summary>
        public static SystemInfo Collect()
        {
            var result = new SystemInfo
            {
                Os = new OsInfo(),
                Network = new NetworkInfo()
            };

            // Computer name
            result.ComputerName = WmiHelper.GetSingleValue(
                "SELECT * FROM Win32_ComputerSystem",
                "Name"
            );

            // OS information
            var osData = WmiHelper.ExecuteQuery("SELECT * FROM Win32_OperatingSystem");
            if (osData.Count > 0)
            {
                var os = osData[0];
                result.Os.Caption = os.ContainsKey("Caption") ? os["Caption"]?.ToString() ?? "N/A" : "N/A";
                result.Os.Version = os.ContainsKey("Version") ? os["Version"]?.ToString() ?? "N/A" : "N/A";
                result.Os.BuildNumber = os.ContainsKey("BuildNumber") ? os["BuildNumber"]?.ToString() ?? "N/A" : "N/A";
            }
            else
            {
                result.Os.Caption = "N/A";
                result.Os.Version = "N/A";
                result.Os.BuildNumber = "N/A";
            }

            // IP address
            result.Network.IpAddress = GetActiveIPAddress();

            return result;
        }

        /// <summary>
        /// Gets active IP address (where IPEnabled=true)
        /// </summary>
        private static string GetActiveIPAddress()
        {
            try
            {
                var results = WmiHelper.ExecuteQuery(
                    "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = True"
                );

                foreach (var adapter in results)
                {
                    if (adapter.ContainsKey("IPAddress") && adapter["IPAddress"] != null)
                    {
                        var ipArray = adapter["IPAddress"] as string[];
                        if (ipArray != null && ipArray.Length > 0)
                        {
                            // Return first IP (usually IPv4)
                            return ipArray[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get IP address: {ex.Message}");
            }

            return "N/A";
        }
    }
}