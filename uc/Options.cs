using CommandLine;

namespace uc;

public class Options
{
    // Path
    [Option('p', "path", Required = false, HelpText = "Path of the directory to be processed.")]
    public string Path { get; set; } = Environment.CurrentDirectory;
    
    // Recursive
    [Option('r', "recursive", Required = false, HelpText = "Scan recursively.")]
    public bool Recursive { get; set; } = false;
    
    // Keep Logs
    [Option('l', "keep-logs", Required = false, HelpText = "Keep the Logs directory.")]
    public bool KeepLogs { get; set; } = false;
    
    // Keep obj
    [Option('o', "keep-obj", Required = false, HelpText = "Keep the obj directory.")]
    public bool KeepObj { get; set; } = false;
    
    // Keep Temp
    [Option('t', "keep-temp", Required = false, HelpText = "Keep the Temp directory.")]
    public bool KeepTemp { get; set; } = false;
}