# InputHelper

Knihovna pro validaci a úpravu textových vstupů od uživatele – jména, příjmení, názvy měst a další texty, u kterých má smysl vynutit gramaticky správný formát (velké počáteční písmeno).

## Filozofie

Knihovna sama vyhodnocuje neplatný vstup a vyhazuje výjimku se srozumitelnou zprávou. V kombinaci s `TextHelper.UpravaVstupu.ZeptejSeAUprav` tak programátor nemusí psát vlastní validační logiku ani ošetřovat chybové stavy – knihovna se o vyzvání uživatele k opravě postará sama.

## Třídy

### `TitleCase`
Převede libovolný text na formát s velkým počátečním písmenem u každého slova, podle pravidel české (cs-CZ) gramatiky. Vhodné pro jména, příjmení a obecné texty bez speciálních výjimek.

```csharp
TitleCase.ToTitleCase("jan novák")  // "Jan Novák"
```

Vyhodí `ArgumentException`, pokud je vstup prázdný nebo obsahuje jen bílé znaky.

### `MestoTitleCase`
Rozšíření `TitleCase` s ohledem na specifika českých názvů měst – předložky uprostřed názvu (nad, pod, u, v, na...) zůstávají malé, zatímco zbytek se převede na velké počáteční písmeno. Podporuje i pomlčková spojení (Frýdek-Místek).

```csharp
MestoTitleCase.ZpracujNazevMesta("ústí nad labem")  // "Ústí nad Labem"
```

Vyhodí `ArgumentException`, pokud je vstup prázdný nebo obsahuje jen bílé znaky.

### `OsobniJmeno`
Rozdělí jeden vstupní řetězec na křestní jméno a příjmení podle první mezery a oboje převede přes `TitleCase`. Vhodné tam, kde chce aplikace zadávat jméno a příjmení jedním vstupem místo dvou samostatných otázek.

```csharp
var (jmeno, prijmeni) = OsobniJmeno.Rozdel("jan novák");
// jmeno = "Jan", prijmeni = "Novák"
```

Vyhodí `ArgumentException`, pokud vstup neobsahuje mezeru oddělující jméno a příjmení (tedy chybí buď jméno, nebo příjmení).

## Použití

```csharp
using InputHelper;

string mesto = UpravaVstupu.ZeptejSeAUprav(
    "", "město",
    v => v,
    s => MestoTitleCase.ZpracujNazevMesta(s),
    jeNove: true);
```

Pro jméno a příjmení jedním vstupem:

```csharp
using InputHelper;

Console.Write("Jméno a příjmení: ");
var (jmeno, prijmeni) = UpravaVstupu.ZeptejSeAUprav(
    (jmeno: "", prijmeni: ""),
    "jméno a příjmení",
    v => $"{v.jmeno} {v.prijmeni}",
    s => OsobniJmeno.Rozdel(s),
    jeNove: true);
```