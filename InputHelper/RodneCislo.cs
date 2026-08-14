namespace InputHelper
{
    /// <summary>
    /// Validuje formát rodného čísla (9 číslic do roku 1954, 10 číslic
    /// od roku 1954 dál, s volitelným lomítkem po šesté číslici).
    /// U desetimístných čísel navíc ověřuje kontrolní součet dělitelností 11.
    /// </summary>
    public static class RodneCislo
    {
        /// <summary>
        /// Ověří formát rodného čísla a vrátí ho normalizované
        /// (s lomítkem, bez mezer).
        /// </summary>
        /// <param name="vstup">Rodné číslo, s lomítkem nebo bez.</param>
        /// <returns>Normalizované rodné číslo ve formátu "RRMMDD/XXXX" nebo "RRMMDD/XXX".</returns>
        /// <exception cref="ArgumentException">Neplatný formát nebo kontrolní součet.</exception>
        public static string Zkontroluj(string vstup)
        {
            if (string.IsNullOrWhiteSpace(vstup))
                throw new ArgumentException("Zadej rodné číslo (např. 900101/1234).");

            string cisliceJen = new([.. vstup.Where(char.IsDigit)]);

            if (cisliceJen.Length != 9 && cisliceJen.Length != 10)
                throw new ArgumentException("Rodné číslo musí mít 9 číslic (do roku 1954) nebo 10 číslic (od roku 1954).");

            string datumovaCast = cisliceJen[..6];
            string koncovka = cisliceJen[6..];

            int mesic = int.Parse(datumovaCast.Substring(2, 2));
            int mesicUpraveny = mesic > 50 ? mesic - 50 : mesic; // ženy mají měsíc +50
            if (mesicUpraveny is < 1 or > 12)
                throw new ArgumentException("Rodné číslo obsahuje neplatný měsíc narození.");

            int den = int.Parse(datumovaCast.Substring(4, 2));
            if (den is < 1 or > 31)
                throw new ArgumentException("Rodné číslo obsahuje neplatný den narození.");

            if (cisliceJen.Length == 10)
            {
                long celeCislo = long.Parse(cisliceJen);
                if (celeCislo % 11 != 0)
                    throw new ArgumentException("Rodné číslo neprošlo kontrolou dělitelnosti jedenácti (překlep?).");
            }

            return $"{datumovaCast}/{koncovka}";
        }
    }
}