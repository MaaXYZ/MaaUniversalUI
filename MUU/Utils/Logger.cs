using Serilog;
using System.IO;
using System.Runtime.InteropServices;

namespace MUU.Utils;

public class Logger
{
    public static readonly Serilog.Core.Logger Log;

    static Logger()
    {
        Log = new LoggerConfiguration()
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

        LogInit();
    }

    public static void LogInit()
    {
        Log.Debug("-----------------------------");
        Log.Debug("MUU Process Start");
        Log.Debug("{@arch} {@os}", RuntimeInformation.OSArchitecture, RuntimeInformation.OSDescription);
        Log.Debug("Working {@working}", Directory.GetCurrentDirectory());
        Log.Debug("Logging {@filename}", _filename);
        Log.Debug("-----------------------------");
    }

    private const string _filename = "./debug/muu.log";
}