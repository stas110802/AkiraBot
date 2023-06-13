namespace AkiraBot.Utilities.CommonTools.Models;

public sealed class PathList
{
    public string OrderPath => $"{LogsPath}orders\\";
    public string ErrorsPath => $"{LogsPath}errors\\";
    public string LaunchesPath => $"{LogsPath}launches\\";
    public string ConfigsPath => $"{ProjectPath}\\configs\\";
    public string SqlQueryPath => $"{ProjectPath}\\sqlQuery\\";
    public string ImagesPath => $"{ProjectPath}\\images\\";
    private string LogsPath => $"{ProjectPath}\\logs\\";
    private string ProjectPath =>
        Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\.."));// ..\

    public string GetProjectPath()
    {
        return ProjectPath;
    }
}