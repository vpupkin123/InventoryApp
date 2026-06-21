using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using InventoryApp.Models;

namespace InventoryApp.Services
{
    public static class JsonExporter
    {
        /// <summary>
        /// Exports inventory report to JSON file in reports folder
        /// </summary>
        /// <param name="report">Inventory report data</param>
        /// <param name="fileName">Filename (without path)</param>
        /// <returns>Full path to created file</returns>
        public static string Export(InventoryReport report, string fileName)
        {
            // Get exe directory
            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            
            // Create reports folder path
            string reportsPath = Path.Combine(exePath, "reports");
            
            // Create reports folder if it doesn't exist
            if (!Directory.Exists(reportsPath))
            {
                Directory.CreateDirectory(reportsPath);
            }

            // Full file path
            string filePath = Path.Combine(reportsPath, fileName);

            // Serialize to JSON with indentation
            string json = JsonConvert.SerializeObject(report, Formatting.Indented);

            // Write to file with UTF-8 encoding
            File.WriteAllText(filePath, json, System.Text.Encoding.UTF8);

            return filePath;
        }
    }
}