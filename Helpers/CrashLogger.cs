using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace TrafficDesktopApp.Helpers
{
    internal static class CrashLogger
    {
        internal static string LogException(Exception ex, string context = null)
        {
            try
            {
                var dir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "TrafficDesktopApp",
                    "Logs"
                );
                Directory.CreateDirectory(dir);

                var file = Path.Combine(dir, $"crash-{DateTime.Now:yyyyMMdd-HHmmss}.log");

                var sb = new StringBuilder();
                sb.AppendLine("=== TrafficDesktopApp crash log ===");
                sb.AppendLine($"UTC: {DateTime.UtcNow:o}");
                sb.AppendLine($"Local: {DateTime.Now:o}");
                sb.AppendLine($"Context: {context ?? "(none)"}");
                sb.AppendLine($"BaseDirectory: {AppDomain.CurrentDomain.BaseDirectory}");
                sb.AppendLine($"CurrentDirectory: {Environment.CurrentDirectory}");
                sb.AppendLine($"Executable: {Assembly.GetExecutingAssembly().Location}");
                sb.AppendLine($"Version: {Assembly.GetExecutingAssembly().GetName().Version}");
                sb.AppendLine();
                sb.AppendLine(ex?.ToString() ?? "(exception is null)");

                File.WriteAllText(file, sb.ToString(), Encoding.UTF8);
                return file;
            }
            catch
            {
                return null;
            }
        }
    }
}

