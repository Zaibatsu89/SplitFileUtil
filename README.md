# SplitFileUtil

SplitFileUtil is een eenvoudige en krachtige C#-tool voor het splitsen van grote bestanden in kleinere delen. Dit is handig voor het opslaan, versturen of archiveren van grote bestanden. De tool biedt een gebruiksvriendelijke interface en ondersteunt diverse bestandsformaten.

## Features

- Splits grote bestanden op in kleinere stukken van gewenste grootte
- Ondersteunt het samenvoegen van gesplitste delen tot het originele bestand
- Werkt met elk type bestand (tekst, binaire bestanden, etc.)
- Eenvoudig te gebruiken via command-line interface (CLI)
- Snel en efficiënt, ook bij zeer grote bestanden

## Installatie

1. 	Clone deze repository:
	```bash
	git clone https://github.com/Zaibatsu89/SplitFileUtil.git
	```
2.	Open het project in Visual Studio of een andere C# IDE.
3.	Herstel benodigde NuGet-packages (indien nodig).
4.	Bouw het project.

## Gebruik

### Bestand splitsen

```bash
SplitFileUtil.exe split -i "<pad/naar/groot-bestand>" -s <max-grootte-per-deel-in-bytes> -o "<output-map>"
```

**Voorbeeld**

```bash
SplitFileUtil.exe split -i "C:\data\bigfile.zip" -s 10485760 -o "C:\data\splits"
```
(Bovenstaand voorbeeld splitst het bestand in delen van 10 MB.)

### Delen samenvoegen

```bash
SplitFileUtil.exe merge -i "<pad/naar/delen-map>" -o "<pad/naar/uitvoer-bestand>"
```

**Voorbeeld**
```bash
SplitFileUtil.exe merge -i "C:\data\splits" -o "C:\data\bigfile_reconstructed.zip"
```

### Command-line opties
* `split`: Splits een bestand op in delen
* `-i`, `--input`: Pad naar het te splitsen bestand (verplicht)
* `-s`, `--size`: Maximale grootte van elk deel in bytes (verplicht)
* `-o`, `--output`: Outputdirectory voor de delen (verplicht)
* `merge`: Voegt delen weer samen tot één bestand
* `-i`, `--input`: Pad naar de map met bestanddelen (verplicht)
* `-o`, `--output`: Pad voor het samengevoegde bestand (verplicht)

## Bijdragen

Bijdragen zijn welkom! Open een issue of maak een pull request met verbeteringen of nieuwe features.

## Licentie

Dit project is gelicenseerd onder de MIT-licentie.

## Contact

Voor vragen of feedback kun je een issue aanmaken op GitHub of contact opnemen via [Zaibatsu89](https://github.com/Zaibatsu89).
