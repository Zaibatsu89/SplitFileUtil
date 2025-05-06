if (args.Length == 0)
{
    Console.WriteLine("Geef het pad naar het te splitsen bestand op als argument.");
    Console.WriteLine("Gebruik: SplitFile.exe <bestandsPad> [chunkGrootteMB]");
    return;
}

string inputFile = args[0];
long chunkSizeMB = 50; // Standaard chunk grootte in MB

if (args.Length > 1)
{
    if (!long.TryParse(args[1], out chunkSizeMB) || chunkSizeMB <= 0)
    {
        Console.WriteLine("Ongeldige chunk grootte. Gebruik een positief getal voor MB.");
        return;
    }
}

long chunkSize = chunkSizeMB * 1024 * 1024; // Converteer MB naar bytes

try
{
    SplitFile(inputFile, chunkSize);
    Console.WriteLine($"Bestand '{inputFile}' succesvol gesplitst in stukken van {chunkSizeMB} MB.");
}
catch (FileNotFoundException)
{
    Console.WriteLine($"Fout: Bestand '{inputFile}' niet gevonden.");
}
catch (IOException ex)
{
    Console.WriteLine($"Een I/O fout is opgetreden: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Een onverwachte fout is opgetreden: {ex.Message}");
}

static void SplitFile(string inputFile, long chunkSize)
{
    if (!File.Exists(inputFile))
    {
        throw new FileNotFoundException($"Bronbestand '{inputFile}' niet gevonden.");
    }

    FileInfo fileInfo = new(inputFile);
    long fileSize = fileInfo.Length;

    if (fileSize == 0)
    {
        Console.WriteLine($"Bestand '{inputFile}' is leeg. Er is niets te splitsen.");
        return;
    }

    int chunkNumber = 1;
    byte[] buffer = new byte[4096]; // Buffer grootte voor het lezen

    using FileStream sourceStream = new(inputFile, FileMode.Open, FileAccess.Read);
    long totalBytesRead = 0;
    while (totalBytesRead < fileSize)
    {
        string outputFileName = $"{Path.GetFileNameWithoutExtension(inputFile)}_part{chunkNumber}{Path.GetExtension(inputFile)}";
        string outputPath = Path.Combine(Path.GetDirectoryName(inputFile) ?? string.Empty, outputFileName);

        using (FileStream targetStream = new(outputPath, FileMode.Create, FileAccess.Write))
        {
            long bytesToWriteInChunk = Math.Min(chunkSize, fileSize - totalBytesRead);
            long currentChunkBytesWritten = 0;

            while (currentChunkBytesWritten < bytesToWriteInChunk)
            {
                int bytesToReadFromSource = (int)Math.Min(buffer.Length, bytesToWriteInChunk - currentChunkBytesWritten);
                int bytesRead = sourceStream.Read(buffer, 0, bytesToReadFromSource);

                if (bytesRead == 0) // Einde van de bron stream bereikt
                {
                    break;
                }

                targetStream.Write(buffer, 0, bytesRead);
                currentChunkBytesWritten += bytesRead;
                totalBytesRead += bytesRead;
            }
        }
        Console.WriteLine($"Deel {chunkNumber} gemaakt: {outputFileName}");
        chunkNumber++;
    }
}