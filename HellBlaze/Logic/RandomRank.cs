using HellBlaze.Models;

namespace HellBlaze.Logic;

public class RandomRank
{
    private readonly GameData _gameData;

    public RandomRank(GameData gameData)
    {
        _gameData = gameData;
    }

    public async Task<Rank> GetRandomRankAsync()
    {
        await _gameData.EnsureDataLoadedAsync();
        return _gameData.Ranks.Count == 0 ? new Rank() : _gameData.Ranks[new Random().Next(_gameData.Ranks.Count)];
    }
}