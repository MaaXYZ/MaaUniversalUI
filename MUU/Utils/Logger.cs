using Serilog;
using System.IO;
using System.Runtime.InteropServices;

namespace MUU.Utils;

public static class Logger
{
    public static Serilog.Core.Logger log = new LoggerConfiguration()
#if DEBUG
        .MinimumLevel.Verbose()
#else
        .MinimumLevel.Debug()
#endif
        .WriteTo.Console()
        .WriteTo.File(_filename,
            rollingInterval: RollingInterval.Day,
            rollOnFileSizeLimit: true,
            retainedFileCountLimit: 7)
        .CreateLogger();

    public static void LogInit()
    {
        log.Debug("-----------------------------");
        log.Debug("MUU Process Start");
        log.Debug("{@arch} {@os}", RuntimeInformation.OSArchitecture, RuntimeInformation.OSDescription);
        log.Debug("Working {@working}", Directory.GetCurrentDirectory());
        log.Debug("Logging {@filename}", _filename);
        log.Debug("-----------------------------");
    }

    public static void LogExit()
    {
        log.Debug("-----------------------------");
        log.Debug("Close log");
        log.Debug("-----------------------------");
    }

    private const string _filename = "./debug/muu.log";
}