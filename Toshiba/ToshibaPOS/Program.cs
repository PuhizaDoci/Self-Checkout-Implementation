using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ToshibaPOS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ToshibaPosSinkronizimi.SinkronizimiClass.Starto(true);

            SetNLogConfig();

            Application.Run(new POSForm());

        }
        private static void SetNLogConfig()
        {
            var applicationPath = Assembly.GetExecutingAssembly().Location;
            var directoryPath = Path.Combine(
                Path.GetDirectoryName(applicationPath) ?? string.Empty, "Logs");
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var fileTarget = new FileTarget
            {
                FileName = "${basedir}/Logs/${logger}.log",
                Layout = "${date} --|-- ${message}",
                MaxArchiveDays = 30,
                MaxArchiveFiles = 30
            };
            var logConsole = new ConsoleTarget("logconsole");

            var rule1 = new LoggingRule("*", LogLevel.Trace, fileTarget);
            var rule5 = new LoggingRule("*", LogLevel.Debug, logConsole);

            var config = new LoggingConfiguration();
            config.AddTarget("log1", fileTarget);
            config.LoggingRules.Add(rule1);
            config.LoggingRules.Add(rule5);

            LogManager.Configuration = config;
        }
    }
}
