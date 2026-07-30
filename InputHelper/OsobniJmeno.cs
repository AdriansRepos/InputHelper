namespace InputHelper
{
    public static class OsobniJmeno
    {
        /// <summary>
        /// Rozdělí zadané celé jméno na křestní jméno a příjmení podle
        /// první mezery a oboje převede do TitleCase.
        /// </summary>
        /// <param name="celeJmeno">Vstupní text ve formátu "Jméno Příjmení".</param>
        /// <returns>Dvojice (jméno, příjmení).</returns>
        /// <exception cref="ArgumentException">Vstup neobsahuje mezeru oddělující jméno a příjmení.</exception>
        public static (string jmeno, string prijmeni) Rozdel(string celeJmeno)
        {
            string[] casti = celeJmeno.Trim().Split(' ', 2);

            if (casti.Length < 2 || string.IsNullOrWhiteSpace(casti[1]))
                throw new ArgumentException("Zadej jméno i příjmení odděleně mezerou (např. \"Jan Novák\").");

            string jmeno = TitleCase.ToTitleCase(casti[0]);
            string prijmeni = TitleCase.ToTitleCase(casti[1]);
            return (jmeno, prijmeni);
        }
    }
}