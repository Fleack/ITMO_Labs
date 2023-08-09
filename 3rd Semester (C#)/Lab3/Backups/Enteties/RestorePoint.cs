using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Enteties;

public class RestorePoint : IRestorePoint
{
    private List<IStorage> _storages;

    public RestorePoint(string name, DateTime dataAndTime, List<IStorage> storages, int cnt)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BackupsException($"Given value {name} can not be null or white space");
        }

        if (storages is null)
        {
            throw new BackupsException($"Given value {storages} can not be null");
        }

        string dateTime_str = dataAndTime.ToString();
        dateTime_str = dateTime_str.Replace('.', '-');
        dateTime_str = dateTime_str.Replace(':', '-');
        dateTime_str = dateTime_str.Replace(' ', '%');

        Name = name + "_[" + dateTime_str + "]_" + cnt.ToString();
        DateAndTime = dataAndTime;
        _storages = storages;
    }

    public string Name { get; }

    public DateTime DateAndTime { get; }

    public IReadOnlyList<IStorage> Storages => _storages;

    public void AddStorage(IStorage storage)
    {
        if (storage is null)
        {
            throw new BackupsException($"Given value [storage] can not be null");
        }

        _storages.Add(storage);
    }
}