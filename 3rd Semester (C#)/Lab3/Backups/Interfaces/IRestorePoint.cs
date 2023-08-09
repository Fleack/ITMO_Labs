namespace Backups.Interfaces;

public interface IRestorePoint
{
    string Name { get; }
    DateTime DateAndTime { get; }
    IReadOnlyList<IStorage> Storages { get; }

    void AddStorage(IStorage storage);
}