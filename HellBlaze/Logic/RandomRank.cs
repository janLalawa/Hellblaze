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
        if (_gameData.Ranks == null || _gameData.Ranks.Count == 0) return new Rank();
        return _gameData.Ranks[new Random().Next(_gameData.Ranks.Count)];
    }
}