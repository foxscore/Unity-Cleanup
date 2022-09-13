// See https://aka.ms/new-console-template for more information

using CommandLine;
using uc;

var arguments = Parser.Default.ParseArguments<Options>(args);

if (arguments.Errors.Any() || arguments.Tag == ParserResultType.NotParsed)
    Environment.Exit(1);

if (!Directory.Exists(arguments.Value.Path))
{
    Console.WriteLine($"Directory not found: {arguments.Value.Path}");
    Environment.Exit(1);
}

ScanDirectory(arguments.Value.Path, arguments.Value.Recursive);

// This tool checks if a directory is a unity project by checking if it contains the "Assets", "Packages", and "ProjectSettings" directories.
// If it is, it will delete the "Library", "Logs", "obj", "Temp" directories.
// Otherwise, it will go through the subdirectories and repeat the process, IF the recursive option is set.

void ScanDirectory(string directory, bool recursive)
{
    // Check if this is a unity project and runs the cleanup if it is
    
    var assets = Path.Combine(directory, "Assets");
    var packages = Path.Combine(directory, "Packages");
    var projectSettings = Path.Combine(directory, "ProjectSettings");
    
    if (Directory.Exists(assets) && Directory.Exists(packages) && Directory.Exists(projectSettings))
        CleanUp(directory);
    else
        // This is not a unity project, go through the subdirectories
        if (recursive)
            foreach (var subdirectory in Directory.GetDirectories(directory))
                ScanDirectory(subdirectory, true);
}

void CleanUp(string directory)
{
    // Delete the "Library", "Logs", "obj", "Temp" directories
    var dirName = Path.GetFileName(directory);
    
    var library = Path.Combine(directory, "Library");
    var logs = Path.Combine(directory, "Logs");
    var obj = Path.Combine(directory, "obj");
    var temp = Path.Combine(directory, "Temp");
    
    var hasLibrary = Directory.Exists(library);
    var hasLogs = !arguments.Value.KeepLogs && Directory.Exists(logs);
    var hasObj = !arguments.Value.KeepObj && Directory.Exists(obj);
    var hasTemp = !arguments.Value.KeepTemp && Directory.Exists(temp);
    
    if (!hasLibrary && !hasLogs && !hasObj && !hasTemp) return;
    
    Console.Write($"Cleaning up {dirName}");
    
    if (Directory.Exists(library))
        Directory.Delete(library, true);
    
    if (!arguments.Value.KeepLogs && Directory.Exists(logs))
        Directory.Delete(logs, true);
    
    if (!arguments.Value.KeepObj && Directory.Exists(obj))
        Directory.Delete(obj, true);
    
    if (!arguments.Value.KeepTemp && Directory.Exists(temp))
        Directory.Delete(temp, true);
    
    // Clear the line
    Console.SetCursorPosition(0, Console.CursorTop);
    
    Console.WriteLine($"Cleaned up {dirName} ");
}