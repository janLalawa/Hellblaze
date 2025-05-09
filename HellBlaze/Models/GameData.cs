using System.Text.Json;

namespace HellBlaze.Models;

public class GameData
{
    private readonly IWebHostEnvironment _environment;
    private readonly SemaphoreSlim _loadLock = new(1, 1);
    private bool _isLoaded;

    public GameData(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public List<Weapon> PrimaryWeapons { get; private set; } = [];
    public List<Weapon> SecondaryWeapons { get; private set; } = [];
    public List<Stratagem> Stratagems { get; private set; } = [];
    public List<Throwable> Throwables { get; private set; } = [];
    public List<Rank> Ranks { get; private set; } = [];
    public List<Armor> Armors { get; private set; } = [];
    public List<Booster> Boosters { get; private set; } = [];

    public async Task EnsureDataLoadedAsync()
    {
        if (_isLoaded)
            return;

        await _loadLock.WaitAsync();
        try
        {
            if (!_isLoaded)
            {
                await LoadDataAsync();
                _isLoaded = true;
            }
        }
        finally
        {
            _loadLock.Release();
        }
    }

    private async Task LoadDataAsync()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        PrimaryWeapons = await LoadJsonAsync<PrimaryWeaponsData>("json/primary.json", options)
            .ContinueWith(t => t.Result.Primary);

        SecondaryWeapons = await LoadJsonAsync<SecondaryWeaponsData>("json/secondary.json", options)
            .ContinueWith(t => t.Result.Secondary);

        Stratagems = await LoadJsonAsync<StratagemsData>("json/stratagems.json", options)
            .ContinueWith(t => t.Result.Stratagems);

        Throwables = await LoadJsonAsync<ThrowablesData>("json/throwable.json", options)
            .ContinueWith(t => t.Result.Throwable);

        Ranks = await LoadJsonAsync<RanksData>("json/ranks.json", options)
            .ContinueWith(t => t.Result.Ranks);

        Armors = await LoadJsonAsync<ArmorData>("json/armour.json", options)
            .ContinueWith(t => t.Result.Armors);

        Boosters = await LoadJsonAsync<BoosterData>("json/booster.json", options)
            .ContinueWith(t => t.Result.Boosters);
    }

    private async Task<T> LoadJsonAsync<T>(string path, JsonSerializerOptions options)
    {
        var fullPath = Path.Combine(_environment.WebRootPath, path);

        if (!File.Exists(fullPath)) throw new FileNotFoundException($"Could not find file at path: {fullPath}");

        using var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
        return await JsonSerializer.DeserializeAsync<T>(fileStream, options) ??
               throw new InvalidOperationException("Failed to deserialize json");
    }
}