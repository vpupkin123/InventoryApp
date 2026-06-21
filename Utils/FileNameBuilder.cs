using System;

namespace InventoryApp.Utils
{
    public static class FileNameBuilder
    {
        /// <summary>
        /// Builds filename in format: lastname_fm_yyyyMMdd_HHmmss.json
        /// </summary>
        /// <param name="lastName">Last name (required)</param>
        /// <param name="firstName">First name (required)</param>
        /// <param name="middleName">Middle name (optional)</param>
        /// <returns>Filename with extension</returns>
        public static string BuildFileName(string lastName, string firstName, string middleName)
        {
            // Transliterate last name
            string transliteratedLastName = Transliterator.Transliterate(lastName);
            
            // Get initials
            string firstInitial = Transliterator.GetInitial(firstName);
            string middleInitial = Transliterator.GetInitial(middleName);

            // Get timestamp
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            // Build filename: lastname_fm_yyyyMMdd_HHmmss.json
            string fileName = $"{transliteratedLastName}_{firstInitial}{middleInitial}_{timestamp}";

            // Add .json extension
            return $"{fileName}.json";
        }
    }
}