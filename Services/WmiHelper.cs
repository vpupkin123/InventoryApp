using System;
using System.Collections.Generic;
using System.Management;

namespace InventoryApp.Services
{
    public static class WmiHelper
    {
        /// <summary>
        /// Executes WMI query and returns list of results
        /// </summary>
        /// <param name="query">WMI query (e.g., "SELECT * FROM Win32_ComputerSystem")</param>
        /// <returns>List of dictionaries with property names and values</returns>
        public static List<Dictionary<string, object>> ExecuteQuery(string query)
        {
            var results = new List<Dictionary<string, object>>();

            try
            {
                using (var searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        var item = new Dictionary<string, object>();
                        
                        foreach (PropertyData prop in obj.Properties)
                        {
                            item[prop.Name] = prop.Value;
                        }
                        
                        results.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error but don't throw - we want to continue collecting other data
                Console.WriteLine($"WMI query failed: {query}\nError: {ex.Message}");
            }

            return results;
        }

        /// <summary>
        /// Gets single string value from WMI query
        /// </summary>
        public static string GetSingleValue(string query, string propertyName)
        {
            var results = ExecuteQuery(query);
            
            if (results.Count > 0 && results[0].ContainsKey(propertyName))
            {
                return results[0][propertyName]?.ToString() ?? "N/A";
            }
            
            return "N/A";
        }
    }
}