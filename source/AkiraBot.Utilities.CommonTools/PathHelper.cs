using AkiraBot.Utilities.CommonTools.Models;

namespace AkiraBot.Utilities.CommonTools;

public static class PathHelper
{
    static PathHelper()
    {
        PathList = new PathList();
    }
    
    /// <summary>
    /// List of all need paths
    /// </summary>
    public static PathList PathList { get; }
    
    /// <summary>
    /// Create folders
    /// </summary>
    /// <param name="filePaths"></param>
    public static void CheckForPathExists(params string[] filePaths)
    {
        foreach (var path in filePaths)
        {
            if (Directory.Exists(path)) continue;

            var i = Directory.CreateDirectory(path);
        }
    }
    
    /// <summary>
    /// Create files
    /// </summary>
    /// <param name="filePaths"></param>
    public static void CheckForFileExists(params string[] filePaths)
    {
        foreach (var path in filePaths)
        {
            if (File.Exists(path)) continue;

            using var file = File.Create(path);
            file.Close();
        }
    }
}