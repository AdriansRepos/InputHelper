using System.Globalization;

namespace InputHelper
{
    public static class TitleCase
    {
        /// <summary>
        /// Převede text do českého formátu TitleCase.
        /// </summary>
        /// <param name="text">Vstupní text.</param>
        /// <returns>Text převedený do TitleCase.</returns>
        public static string ToTitleCase(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Zadej platný text, vstup nemůže být prázdný.");
            try
            {
                var culture = new CultureInfo("cs-CZ");
                var ti = culture.TextInfo;
                return ti.ToTitleCase(text.ToLower());
            }
            catch
            {
                // Záložní řešení pro systémy bez cs-CZ lokalizace (např. slovenská Windows)
                return string.Join(" ", text.ToLower().Split(' ')
                    .Select(slovo => slovo.Length > 0
                        ? char.ToUpper(slovo[0]) + slovo[1..]
                        : slovo));
            }
        }
    }
}