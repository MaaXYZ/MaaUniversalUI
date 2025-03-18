using System;
using System.IO;
using System.Runtime.InteropServices;
using Serilog;
using Serilog.Enrichers.CallerInfo;

namespace MUU.Utils;

public class Logger
{
    public static readonly Serilog.Core.Logger log;

    static Logger()
    {
        const string logTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}][{Level:u3}][Px{ProcessIdHex}][Tx{ThreadIdHex}][{SourceFile}][L{LineNumber}][{Method}] {Message:lj}{NewLine}{Exception}";

        log = new LoggerConfiguration()
        .Enrich.WithProperty("ProcessIdHex", Environment.ProcessId.ToString("X4"))
        .Enrich.WithProperty("ThreadIdHex", Environment.CurrentManagedThreadId.ToString("X4"))
        .Enrich.WithCallerInfo(
            includeFileInfo: true,
            filePathDepth: 1,
            allowedAssemblies: ["MUU"])
        .MinimumLevel.Debug()
        .WriteTo.Console(outputTemplate: logTemplate)
        .WriteTo.File(_filename,
            outputTemplate: logTemplate,
            rollingInterval: RollingInterval.Day,
            rollOnFileSizeLimit: true,
            retainedFileCountLimit: 7)
        .CreateLogger();

        LogInit();
    }

    public static void LogInit()
    {
        log.Debug("-----------------------------");
        log.Debug("MUU Process Start");
        log.Debug("{arch} {os}", RuntimeInformation.OSArchitecture, RuntimeInformation.OSDescription);
        log.Debug("Working {curDir}", Directory.GetCurrentDirectory());
        log.Debug("Logging {logFile}", _filename);
        log.Debug("-----------------------------");
    }

    private const string _filename = "./debug/muu.log";
}