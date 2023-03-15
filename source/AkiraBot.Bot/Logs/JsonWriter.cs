using AkiraBot.Utilities.CommonTools;
using Newtonsoft.Json;

namespace AkiraBot.Bot.Logs;

public sealed class JsonWriter
{
    /// <summary>
    /// write log info to json file
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="logInfo"></param>
    public void LogInfoAt<T>(string filePath, T logInfo)
    {
        // if the file not exists then create new file
        PathHelper.CheckForFileExists(filePath);
        
        var jsonData = File.ReadAllText(filePath);
        
        // De-serialize to object or create new list
        var logList = JsonConvert.DeserializeObject<List<T>>(jsonData)
                      ?? new List<T>();
            
        logList.Add(logInfo);
        
        jsonData = JsonConvert.SerializeObject(logList, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
    }
}