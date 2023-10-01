// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Microsoft.Extensions.Configuration;

// ReSharper disable StringLiteralTypo

Trace.Listeners.Add(new TextWriterTraceListener(
    File.CreateText("log.txt")));
Trace.AutoFlush = true;

Debug.WriteLine("Debug is listening");
Trace.WriteLine("Trace is listening");

IConfigurationBuilder builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json",
        true, true);

IConfigurationRoot configuration = builder.Build();

TraceSwitch ts = new(
    "PacktSwitch",
    "This switch is set via a JSON config."
);
configuration.GetSection("PacktSwitch").Bind(ts);

Console.WriteLine($"The Trace Level is {ts.Level}");

Trace.WriteLineIf(ts.TraceError, "Trace error");
Trace.WriteLineIf(ts.TraceWarning, "Trace warning");
Trace.WriteLineIf(ts.TraceInfo, "Trace information");
Trace.WriteLineIf(ts.TraceVerbose, "Trace verbose");