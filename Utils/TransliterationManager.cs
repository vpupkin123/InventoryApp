using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace InventoryApp.Utils
{
    public static class TransliterationManager
    {
        private static Dictionary<char, string> _translitMap;
        private static bool _loaded = false;

        // Fallback mapping (hardcoded)
        private static readonly Dictionary<char, string> _fallbackMap = new Dictionary<char, string>
        {
            ['а'] = "a", ['б'] = "b", ['в'] = "v", ['г'] = "g", ['д'] = "d",
            ['е'] = "e", ['ё'] = "yo", ['ж'] = "zh", ['з'] = "z", ['и'] = "i",
            ['й'] = "y", ['к'] = "k", ['л'] = "l", ['м'] = "m", ['н'] = "n",
            ['о'] = "o", ['п'] = "p", ['р'] = "r", ['с'] = "s", ['т'] = "t",
            ['у'] = "u", ['ф'] = "f", ['х'] = "kh", ['ц'] = "ts", ['ч'] = "ch",
            ['ш'] = "sh", ['щ'] = "sch", ['ъ'] = "", ['ы'] = "y", ['ь'] = "",
            ['э'] = "e", ['ю'] = "yu", ['я'] = "ya"
        };

        public static void Load()
        {
            _translitMap = new Dictionary<char, string>(_fallbackMap);

            // Try to load translit.ini from exe directory
            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string iniPath = Path.Combine(exePath, "translit.ini");

            if (File.Exists(iniPath))
            {
                try
                {
                    ParseIniFile(iniPath);
                    _loaded = true;
                }
                catch (Exception ex)
                {
                    // If parsing fails, continue with fallback mapping
                    Console.WriteLine($"Failed to load translit.ini: {ex.Message}");
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
                    continue;
                }

                // Key=Value pair (only in [map] section)
                if (currentSection == "map")
                {
                    int equalsIndex = trimmedLine.IndexOf('=');
                    if (equalsIndex > 0)
                    {
                        string key = trimmedLine.Substring(0, equalsIndex).Trim();
                        string value = trimmedLine.Substring(equalsIndex + 1).Trim();
                        
                        if (key.Length == 1)
                        {
                            _translitMap[key[0]] = value;
                        }
                    }
                }
            }
        }

        public static string GetTransliteration(char c)
        {
            if (!_loaded && _translitMap == null)
            {
                Load();
            }

            if (_translitMap.TryGetValue(c, out string translit))
            {
                return translit;
            }

            return null;
        }
    }
}