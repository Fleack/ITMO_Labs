using Backups.Extra.Tools;
using Newtonsoft.Json;

namespace Backups.Extra.Models;

public class BackupsSettingSerializer
{
    private const string FileName = "BackupsExtraSettings.json";
    private JsonSerializerSettings _jsonSettings;

    public BackupsSettingSerializer(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new BackupsExtraException($"Failed to construct BackupsSettingSerializer. Given value path: {path} can not be null or white space");
        }

        SettingsPath = Path.Combine(path, FileName);
        _jsonSettings = new JsonSerializerSettings();
        _jsonSettings.Formatting = Formatting.Indented;
        _jsonSettings.TypeNameHandling = TypeNameHandling.Auto;
    }

    public string SettingsPath { get; private set; }

    public void SetSettingsPath(string new_path)
    {
        if (string.IsNullOrWhiteSpace(new_path))
        {
            throw new BackupsExtraException($"Failed to SetNewPath. Given value new_path: {new_path} can not be null or white space");
        }

        SettingsPath = Path.Combine(new_path, FileName);
    }

    public void Serialize(BackupsSetting backupsSetting, Backups.Extra.Logger.Logger logger)
    {
        string serialized_to_json = JsonConvert.SerializeObject(backupsSetting, _jsonSettings);
        File.WriteAllText(SettingsPath, serialized_to_json);
        logger.LogSerialized(backupsSetting, SettingsPath);
    }

    public BackupsSetting Deserialize(Backups.Extra.Logger.Logger logger)
    {
        BackupsSetting? backupsSetting = JsonConvert.DeserializeObject<BackupsSetting>(File.ReadAllText(SettingsPath), _jsonSettings);
        if (backupsSetting is null)
        {
            throw new BackupsExtraException($"Failed to Deserialize. SettingsPath: {SettingsPath} is incorrect!");
        }

        logger.LogDeserialized(backupsSetting, SettingsPath);
        return backupsSetting;
    }
}
