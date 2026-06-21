using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace InventoryApp.Utils
{
    public static class LocalizationManager
    {
        private static Dictionary<string, Dictionary<string, string>> _strings;
        private static bool _loaded = false;

        // Fallback English strings (hardcoded)
        private static readonly Dictionary<string, Dictionary<string, string>> _fallbackStrings = 
            new Dictionary<string, Dictionary<string, string>>
        {
            ["main"] = new Dictionary<string, string>
            {
                ["WindowTitle"] = "Inventory Collection Tool",
                ["LastName"] = "Last Name",
                ["FirstName"] = "First Name",
                ["MiddleName"] = "Middle Name",
                ["CollectButton"] = "Collect Data"
            },
            ["messages"] = new Dictionary<string, string>
            {
                ["SuccessTitle"] = "Success",
                ["SuccessMessage"] = "Data saved successfully",
                ["ErrorTitle"] = "Error",
                ["ErrorMessage"] = "An error occurred while saving data",
                ["FlashDriveMessage"] = "You can now remove the flash drive"
            },
            ["validation"] = new Dictionary<string, string>
            {
                ["RequiredField"] = "This field is required",
                ["InvalidCharacters"] = "Only Cyrillic characters are allowed"
            }
        };

        public static void Load()
        {
            _strings = new Dictionary<string, Dictionary<string, string>>();
            
            // Deep copy fallback strings
            foreach (var section in _fallbackStrings)
            {
                _strings[section.Key] = new Dictionary<string, string>(section.Value);
            }

            // Try to load lang.ini from exe directory
            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string iniPath = Path.Combine(exePath, "lang.ini");

            if (File.Exists(iniPath))
            {
                try
                {
                    ParseIniFile(iniPath);
                    _loaded = true;
                }
                catch (Exception ex)
                {
                    // If parsing fails, continue with fallback strings
                    Console.WriteLine($"Failed to load lang.ini: {ex.Message}");
                }
            }
        }

        private static void ParseIniFile(string filePath)
        {
            string currentSection = "";

            foreach (string line in File.ReadAllLines(filePath))
            {
                string trimmedLine = line.Trim();

                // Skip empty lines and comments
                if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith(";"))
                    continue;

                // Section header
                if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                {
                    currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2);
                    if (!_strings.ContainsKey(currentSection))
                    {
                        _strings[currentSection] = new Dictionary<string, string>();
                    }
                    continue;
                }

                // Key=Value pair
                int equalsIndex = trimmedLine.IndexOf('=');
                if (equalsIndex > 0 && !string.IsNullOrEmpty(currentSection))
                {
                    string key = trimmedLine.Substring(0, equalsIndex).Trim();
                    string value = trimmedLine.Substring(equalsIndex + 1).Trim();
                    _strings[currentSection][key] = value;
                }
            }
        }

        public static string Get(string section, string key)
        {
            if (!_loaded && _strings == null)
            {
                Load();
            }

            if (_strings.TryGetValue(section, out var sectionDict))
            {
                if (sectionDict.TryGetValue(key, out var value))
                {
                    return value;
                }
            }

            // Return key as fallback
            return $"{section}.{key}";
        }
    }
}