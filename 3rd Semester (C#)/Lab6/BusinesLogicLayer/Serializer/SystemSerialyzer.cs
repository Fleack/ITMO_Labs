using DataAccessLayer.Manager;
using Newtonsoft.Json;

namespace BusinessLayer.Serializer;

public class SystemSerialyzer
{
    private const string FileName = "SystemSettings.json";
    private JsonSerializerSettings _jsonSettings;

    public SystemSerialyzer(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new BllException($"Failed to construct SystemSerialyzer. Given value path: {path} can not be null or white space");

        SettingsPath = Path.Combine(path, FileName);
        _jsonSettings = new JsonSerializerSettings();
        _jsonSettings.Formatting = Formatting.Indented;
        _jsonSettings.TypeNameHandling = TypeNameHandling.Auto;
    }

    public string SettingsPath { get; private set; }

    public void SetSettingsPath(string new_path)
    {
        if (string.IsNullOrWhiteSpace(new_path))
            throw new BllException($"Failed to SetSettingsPath. Given value new_path: {new_path} can not be null or white space");

        SettingsPath = Path.Combine(new_path, FileName);
    }

    public void Serialize(DalManager dalManager)
    {
        string serialized_to_json = JsonConvert.SerializeObject(dalManager, _jsonSettings);
        File.WriteAllText(SettingsPath, serialized_to_json);
    }

    public DalManager Deserialize()
    {
        DalManager? backupsSetting = JsonConvert.DeserializeObject<DalManager>(File.ReadAllText(SettingsPath), _jsonSettings);
        if (backupsSetting is null)
            throw new BllException($"Failed to Deserialize. SettingsPath: {SettingsPath} is incorrect!");

        return backupsSetting;
    }
}
