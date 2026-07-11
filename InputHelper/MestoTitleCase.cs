namespace InputHelper
{
    /// <summary>
    /// Poskytuje převod textu na formát TitleCase s ohledem na
    /// specifika českých názvů měst (předložky uprostřed názvu
    /// zůstávají malé, např. "Ústí nad Labem", ne "Ústí Nad Labem").
    /// </summary>
    public static class MestoTitleCase
    {
        /// <summary>
        /// Předložky, které se v názvech měst nepíší velkým písmenem,
        /// pokud nejsou na úplném začátku názvu.
        /// </summary>
        private static readonly HashSet<string> MalePredlozky = new(StringComparer.OrdinalIgnoreCase)
        {
            "nad", "pod", "u", "v", "ve", "na", "za", "z", "ze", "od"
        };

        /// <summary>
        /// Převede název města na správný tvar s velkými písmeny
        /// na začátku slov, kromě předložek uprostřed názvu.
        /// Zachovává i pomlčková spojení (např. "Frýdek-Místek").
        /// </summary>
        /// <param name="nazevMesta">Vstupní název města v libovolném zápisu velikosti písmen.</param>
        /// <returns>Název města se správně velkými písmeny.</returns>
        public static string ZpracujNazevMesta(string nazevMesta)
        {
            if (string.IsNullOrWhiteSpace(nazevMesta))
                return string.Empty;

            string[] slova = nazevMesta.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < slova.Length; i++)
            {
                // Zpracuje i části spojené pomlčkou (Frýdek-Místek)
                slova[i] = ZpracujSlovo(slova[i], jePrvni: i == 0);
            }

            return string.Join(' ', slova);
        }

        /// <summary>
        /// Zpracuje jedno slovo (případně víc částí spojených pomlčkou).
        /// </summary>
        private static string ZpracujSlovo(string slovo, bool jePrvni)
        {
            string[] casti = slovo.Split('-');

            for (int i = 0; i < casti.Length; i++)
            {
                bool jePrvniCastCelehoNazvu = jePrvni && i == 0;

                if (!jePrvniCastCelehoNazvu && MalePredlozky.Contains(casti[i]))
                {
                    casti[i] = casti[i].ToLower();
                }
                else
                {
                    casti[i] = VelkePismenoNaZacatku(casti[i]);
                }
            }

            return string.Join('-', casti);
        }

        private static string VelkePismenoNaZacatku(string slovo)
        {
            if (string.IsNullOrEmpty(slovo))
                return slovo;

            return char.ToUpper(slovo[0]) + slovo[1..].ToLower();
        }
    }
}
