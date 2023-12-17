namespace Template.FormsApp.Helpers;

using Xamarin.Essentials;
using Xamarin.Forms;

public static class CrashReportHelper
{
    public static void LogException(Exception e)
    {
#pragma warning disable CA1031
        try
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, "dump.log");

            var log = new StringBuilder();
            log.AppendLine($"Time: {DateTime.Now:yyyy/MM/dd HH:mm:ss}");
            log.AppendLine("Exception:");
            log.AppendLine(e.ToString());

            File.WriteAllText(path, log.ToString());
        }
        catch
        {
            // Ignore
        }
#pragma warning restore CA1031
    }

    public static async ValueTask ShowReport()
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, "dump.log");

        if (!File.Exists(path))
        {
            return;
        }

        var log = await File.ReadAllTextAsync(path);

        await Application.Current.MainPage.DisplayAlert("Crash report", log, "Close");

        File.Delete(path);
    }

    public static string? GetReport()
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, "dump.log");

        return !File.Exists(path) ? null : File.ReadAllText(path);
    }
}
