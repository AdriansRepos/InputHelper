# InputHelper

Knihovna pro validaci a úpravu textových vstupů od uživatele – jména, příjmení, názvy měst a další texty, u kterých má smysl vynutit gramaticky správný formát (velké počáteční písmeno).

## Filozofie

Knihovna sama vyhodnocuje neplatný vstup a vyhazuje výjimku se srozumitelnou zprávou. V kombinaci s `TextHelper.UpravaVstupu.ZeptejSeAUprav` tak programátor nemusí psát vlastní validační logiku ani ošetřovat chybové stavy – knihovna se o vyzvání uživatele k opravě postará sama.

## Třídy

### `TitleCase`
Převede libovolný text na formát s velkým počátečním písmenem u každého slova, podle pravidel české (cs-CZ) gramatiky. Vhodné pro jména, příjmení a obecné texty bez speciálních výjimek.

### `MestoTitleCase`
Rozšíření `TitleCase` s ohledem na specifika českých názvů měst – předložky uprostřed názvu (nad, pod, u, v, na...) zůstávají malé, zatímco zbytek se převede na velké počáteční písmeno. Podporuje i pomlčková spojení (Frýdek-Místek).

### `OsobniJmeno`
Rozdělí jeden vstupní řetězec na křestní jméno a příjmení podle první mezery a oboje převede přes `TitleCase`. Vhodné tam, kde chce aplikace zadávat jméno a příjmení jedním vstupem místo dvou samostatných otázek.

### `RodneCislo`
Ověří formát rodného čísla (9 číslic pro narození do roku 1954, 10 číslic od roku 1954 dál, s volitelným lomítkem po šesté číslici) a vrátí ho normalizované s lomítkem. U desetimístných čísel navíc kontroluje dělitelnost jedenácti.

## Použití

```csharp
TitleCase.ToTitleCase("jan novák")  // "Jan Novák"
```

Vyhodí `ArgumentException`, pokud je vstup prázdný nebo obsahuje jen bílé znaky.

---

Pro město, i víceslovné:

```csharp
using InputHelper;

MestoTitleCase.ZpracujNazevMesta("ústí nad labem")  // "Ústí nad Labem"

string mesto = UpravaVstupu.ZeptejSeAUprav(
    "", "město",
    v => v,
    s => MestoTitleCase.ZpracujNazevMesta(s),
    jeNove: true);
```

Vyhodí `ArgumentException`, pokud je vstup prázdný nebo obsahuje jen bílé znaky.

---

Pro jméno a příjmení zadáno v jednom řádku oddělené mezerou:

```csharp
using InputHelper;

var (jmeno, prijmeni) = OsobniJmeno.Rozdel("jan novák");
// jmeno = "Jan", prijmeni = "Novák"

Console.Write("Jméno a příjmení: ");
var (jmeno, prijmeni) = UpravaVstupu.ZeptejSeAUprav(
    (jmeno: "", prijmeni: ""),
    "jméno a příjmení",
    v => $"{v.jmeno} {v.prijmeni}",
    s => OsobniJmeno.Rozdel(s),
    jeNove: true);
```

Vyhodí `ArgumentException`, pokud vstup neobsahuje mezeru oddělující jméno a příjmení (tedy chybí buď jméno, nebo příjmení).

---

```csharp
using InputHelper;

RodneCislo.Zkontroluj("9001011234")   // "900101/1234"
RodneCislo.Zkontroluj("900101/1234")  // "900101/1234"

string rodneCislo = UpravaVstupu.ZeptejSeAUprav(
    "", "rodné číslo (např. 900101/1234)",
    v => v,
    s => RodneCislo.Zkontroluj(s),
    jeNove: true);
```

Vyhodí `ArgumentException`, pokud je vstup prázdný, má neplatnou délku, obsahuje neplatný měsíc/den narození, nebo neprojde kontrolou dělitelnosti jedenácti.

## Instalace jako NuGet balíček

Tato knihovna je dostupná také jako balíček v **GitHub Packages**. Můžete si ji snadno přidat do svého projektu:

1. Nastavte si jako zdroj balíčků váš GitHub (pokud ho ještě nemáte v NuGet.config).
2. V **Package Manager Console** spusťte příkaz:
   ```powershell
   Install-Package BarevneVypisyHelper
   ```
