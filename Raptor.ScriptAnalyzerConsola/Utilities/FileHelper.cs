namespace Raptor.ScriptAnalyzerConsola.Utilities;

public static class FileHelper
{
    public static async Task<string> ReadFileAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("La ruta del archivo no puede estar vacía o ser nula.", nameof(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"El archivo '{filePath}' no existe.", filePath);

        return await File.ReadAllTextAsync(filePath);
    }

    public static async Task WriteFileAsync(string filePath, string v)
    {
        //guardar el archivo
        await File.WriteAllTextAsync(filePath, v);

    }
}
