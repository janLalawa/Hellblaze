using System.Collections.Concurrent;
using HellBlaze.Models;

namespace HellBlaze.Services;

public class LoadoutService
{
    private readonly ConcurrentDictionary<string, SharedLoadout> _loadouts = new();

    public string SaveLoadout(SharedLoadout loadout)
    {
        var id = loadout.GenerateShortId();
        _loadouts[id] = loadout;
        return id;
    }

    public SharedLoadout? GetLoadout(string id)
    {
        _loadouts.TryGetValue(id, out var loadout);
        return loadout;
    }
}