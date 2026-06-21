using System.Text;

namespace InventoryApp.Utils
{
    public static class Transliterator
    {
        /// <summary>
        /// Transliterates non-Latin text to Latin
        /// </summary>
        public static string Transliterate(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var result = new StringBuilder();

            foreach (char c in text)
            {
                // If it's already a Latin letter, keep as-is
                if (IsLatinLetter(c))
                {
                    result.Append(c);
                }
                else
                {
                    // Try to transliterate
                    string translit = TransliterationManager.GetTransliteration(c);
                    if (translit != null)
                    {
                        result.Append(translit);
                    }
                    else if (char.IsLetterOrDigit(c))
                    {
                        // Keep other letters and digits as-is
                        result.Append(c);
                    }
                    // Skip other characters (spaces, punctuation, etc.)
                }
            }

            return result.ToString().ToLowerInvariant();
        }

        /// <summary>
        /// Gets first letter of the name (for initials)
        /// </summary>
        public static string GetInitial(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            char firstChar = name.Trim()[0];
            return Transliterate(firstChar.ToString());
        }

        /// <summary>
        /// Checks if character is a Latin letter
        /// </summary>
        private static bool IsLatinLetter(char c)
        {
            return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
        }
    }
}