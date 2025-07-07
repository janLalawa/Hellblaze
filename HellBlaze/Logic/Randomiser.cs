using HellBlaze.Models;

namespace HellBlaze.Logic;

public class Randomiser(GameData gameData, int? seed = null)
{
    private readonly Random _random = seed.HasValue ? new Random(seed.Value) : new Random();


    private decimal CalculateWeight(Stratagem stratagem, Factors factors)
    {
        var weight = factors.BaseFactor;

        if (factors.ExplosiveFactor > 0 && stratagem.Explosive > 0)
            weight += stratagem.Explosive * factors.ExplosiveFactor;

        if (factors.FireFactor > 0 && stratagem.Fire > 0)
            weight += stratagem.Fire * factors.FireFactor;

        if (factors.AntiTankFactor > 0 && stratagem.AntiTank > 0)
            weight += stratagem.AntiTank * factors.AntiTankFactor;

        if (factors.LaserFactor > 0 && stratagem.Laser > 0)
            weight += stratagem.Laser * factors.LaserFactor;

        if (factors.PrecisionFactor > 0 && stratagem.Precision > 0)
            weight += stratagem.Precision * factors.PrecisionFactor;

        if (factors.ElectricalFactor > 0 && stratagem.Electrical > 0)
            weight += stratagem.Electrical * factors.ElectricalFactor;

        if (factors.GasFactor > 0 && stratagem.Gas > 0)
            weight += stratagem.Gas * factors.GasFactor;

        if (factors.SmokeFactor > 0 && stratagem.Smoke > 0)
            weight += stratagem.Smoke * factors.SmokeFactor;

        if (factors.AntiHordeFactor > 0 && stratagem.AntiHorde > 0)
            weight += stratagem.AntiHorde * factors.AntiHordeFactor;

        if (factors.TurretFactor > 0 && stratagem.Turret > 0)
            weight += stratagem.Turret * factors.TurretFactor;

        if (factors.MineFactor > 0 && stratagem.Mine > 0)
            weight += stratagem.Mine * factors.MineFactor;

        if (factors.EagleFactor > 0 && stratagem.Eagle > 0)
            weight += stratagem.Eagle * factors.EagleFactor;

        if (factors.OrbitalFactor > 0 && stratagem.Orbital > 0)
            weight += stratagem.Orbital * factors.OrbitalFactor;

        if (factors.SupportFactor > 0 && stratagem.Support > 0)
            weight += stratagem.Support * factors.SupportFactor;

        return weight;
    }

    private T GetWeightedRandomItem<T>(IEnumerable<T> items, Func<T, decimal> weightSelector)
    {
        var itemList = items.ToList();
        if (itemList.Count == 0)
            return default!;

        decimal totalWeight = 0;
        var weights = new List<decimal>();

        foreach (var weight in itemList.Select(weightSelector))
        {
            weights.Add(weight);
            totalWeight += weight;
        }

        if (totalWeight <= 0)
            return itemList[_random.Next(itemList.Count)];

        var randomValue = (decimal)_random.NextDouble() * totalWeight;

        decimal cumulativeWeight = 0;
        for (var i = 0; i < itemList.Count; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue < cumulativeWeight)
                return itemList[i];
        }

        return itemList.Last();
    }

    private Stratagem GetRandomStratagem(Factors factors, List<Stratagem> availableStratagems)
    {
        return GetWeightedRandomItem(availableStratagems, stratagem => CalculateWeight(stratagem, factors));
    }

    private Weapon GetRandomWeapon<T>(Factors factors, List<T> weapons) where T : Weapon
    {
        if (weapons == null || weapons.Count == 0)
            return new Weapon();
        return GetWeightedRandomItem(weapons, weapon =>
        {
            var weight = factors.BaseFactor;

            if (factors.ExplosiveFactor > 0 && weapon.Explosive > 0)
                weight += weapon.Explosive * factors.ExplosiveFactor;

            if (factors.FireFactor > 0 && weapon.Fire > 0)
                weight += weapon.Fire * factors.FireFactor;

            if (factors.AntiTankFactor > 0 && weapon.AntiTank > 0)
                weight += weapon.AntiTank * factors.AntiTankFactor;

            if (factors.LaserFactor > 0 && weapon.Laser > 0)
                weight += weapon.Laser * factors.LaserFactor;

            if (factors.PrecisionFactor > 0 && weapon.Precision > 0)
                weight += weapon.Precision * factors.PrecisionFactor;

            if (factors.ElectricalFactor > 0 && weapon.Electrical > 0)
                weight += weapon.Electrical * factors.ElectricalFactor;

            if (factors.GasFactor > 0 && weapon.Gas > 0)
                weight += weapon.Gas * factors.GasFactor;

            if (factors.SmokeFactor > 0 && weapon.Smoke > 0)
                weight += weapon.Smoke * factors.SmokeFactor;

            if (factors.AntiHordeFactor > 0 && weapon.AntiHorde > 0)
                weight += weapon.AntiHorde * factors.AntiHordeFactor;

            if (factors.SupportFactor > 0 && weapon.Support > 0)
                weight += weapon.Support * factors.SupportFactor;

            return weight;
        });
    }

    private Throwable GetRandomThrowable(Factors factors, List<Throwable> throwables)
    {
        if (throwables == null || throwables.Count == 0)
            return new Throwable();
        return GetWeightedRandomItem(throwables, throwable =>
        {
            var weight = factors.BaseFactor;

            if (factors.ExplosiveFactor > 0 && throwable.Explosive > 0)
                weight += throwable.Explosive * factors.ExplosiveFactor;

            if (factors.FireFactor > 0 && throwable.Fire > 0)
                weight += throwable.Fire * factors.FireFactor;

            if (factors.AntiTankFactor > 0 && throwable.AntiTank > 0)
                weight += throwable.AntiTank * factors.AntiTankFactor;

            if (factors.LaserFactor > 0 && throwable.Laser > 0)
                weight += throwable.Laser * factors.LaserFactor;

            if (factors.PrecisionFactor > 0 && throwable.Precision > 0)
                weight += throwable.Precision * factors.PrecisionFactor;

            if (factors.ElectricalFactor > 0 && throwable.Electrical > 0)
                weight += throwable.Electrical * factors.ElectricalFactor;

            if (factors.GasFactor > 0 && throwable.Gas > 0)
                weight += throwable.Gas * factors.GasFactor;

            if (factors.SmokeFactor > 0 && throwable.Smoke > 0)
                weight += throwable.Smoke * factors.SmokeFactor;

            if (factors.AntiHordeFactor > 0 && throwable.AntiHorde > 0)
                weight += throwable.AntiHorde * factors.AntiHordeFactor;

            if (factors.SupportFactor > 0 && throwable.Support > 0)
                weight += throwable.Support * factors.SupportFactor;

            return weight;
        });
    }

    private Armor GetRandomArmor(HashSet<string>? disabledWarbonds = null)
    {
        if (gameData.Armors.Count == 0)
            return new Armor();

        var availableArmors = gameData.Armors
                                      .Where(a => disabledWarbonds == null || !disabledWarbonds.Contains(a.Warbond))
                                      .ToList();

        if (availableArmors.Count == 0)
            return new Armor();

        var randomIndex = _random.Next(availableArmors.Count);
        return availableArmors[randomIndex];
    }


    private Booster? GetRandomBooster(HashSet<string> usedBoosters, HashSet<string>? disabledWarbonds = null)
    {
        if (gameData.Boosters.Count == 0)
            return null;

        var availableBoosters = gameData.Boosters
                                        .Where(b => !usedBoosters.Contains(b.Name)
                                                    && (disabledWarbonds == null || !disabledWarbonds.Contains(b.Warbond)))
                                        .ToList();

        if (availableBoosters.Count == 0)
            return null;

        var randomIndex = _random.Next(availableBoosters.Count);
        return availableBoosters[randomIndex];
    }


    private Kit? GetRandomKit(Factors factors, bool isReady = false, List<Stratagem>? excludedStratagems = null, HashSet<string>? usedBoosters = null, HashSet<string>? disabledWarbonds = null)
    {
        if (isReady) return null;

        var availablePrimaries = gameData.PrimaryWeapons
                                         .Where(w => disabledWarbonds == null || !disabledWarbonds.Contains(w.Warbond))
                                         .ToList();

        var availableSecondaries = gameData.SecondaryWeapons
                                           .Where(w => disabledWarbonds == null || !disabledWarbonds.Contains(w.Warbond))
                                           .ToList();

        var availableThrowables = gameData.Throwables
                                          .Where(t => disabledWarbonds == null || !disabledWarbonds.Contains(t.Warbond))
                                          .ToList();

        var kit = new Kit
        {
            Primary = GetRandomWeapon(factors, availablePrimaries),
            Secondary = GetRandomWeapon(factors, availableSecondaries),
            Throwable = GetRandomThrowable(factors, availableThrowables),
            Armor = GetRandomArmor(disabledWarbonds)
        };

        if (usedBoosters != null)
        {
            var availableBoosters = new HashSet<string>(usedBoosters);

            kit.Booster = GetRandomBooster(availableBoosters, disabledWarbonds);
            if (kit.Booster != null) usedBoosters.Add(kit.Booster.Name);
        }

        var availableStratagems = gameData.Stratagems
                                          .Where(s => disabledWarbonds == null || !disabledWarbonds.Contains(s.Warbond))
                                          .ToList();

        if (excludedStratagems != null)
            foreach (var excluded in excludedStratagems)
                availableStratagems.RemoveAll(s => s.Name == excluded.Name);

        var hasBackpack = false;
        var hasSupportWeapon = false;

        for (var i = 0; i < 4; i++)
        {
            var validOptions = availableStratagems.Where(s =>
                                                             (!factors.OneBackpack || !s.Backpack || !hasBackpack)
                                                             &&
                                                             (!factors.OneSupportWeapon || !s.SupportWeapon || !hasSupportWeapon)
            ).ToList();

            if (validOptions.Count == 0)
                break;

            var selectedStratagem = GetRandomStratagem(factors, validOptions);

            if (selectedStratagem.Backpack)
                hasBackpack = true;
            if (selectedStratagem.SupportWeapon)
                hasSupportWeapon = true;

            switch (i)
            {
                case 0: kit.Stratagem1 = selectedStratagem; break;
                case 1: kit.Stratagem2 = selectedStratagem; break;
                case 2: kit.Stratagem3 = selectedStratagem; break;
                case 3: kit.Stratagem4 = selectedStratagem; break;
            }

            availableStratagems.Remove(selectedStratagem);
        }

        if (!hasSupportWeapon && factors.MustIncludeSupportWeapon) EnsureSupportWeapon(kit, factors, disabledWarbonds);


        return kit;
    }

    private void EnsureSupportWeapon(Kit kit, Factors factors, HashSet<string>? disabledWarbonds)
    {
        var supportWeapons = gameData.Stratagems
                                     .Where(s => s.SupportWeapon &&
                                                 (disabledWarbonds == null || !disabledWarbonds.Contains(s.Warbond)))
                                     .ToList();

        if (supportWeapons.Count == 0)
            return;

        var supportWeapon = GetRandomStratagem(factors, supportWeapons);

        var replaceableSlots = new List<int>();

        if (!factors.OneBackpack || kit.Stratagem1.Backpack != true) replaceableSlots.Add(0);
        if (!factors.OneBackpack || kit.Stratagem2.Backpack != true) replaceableSlots.Add(1);
        if (!factors.OneBackpack || kit.Stratagem3.Backpack != true) replaceableSlots.Add(2);
        if (!factors.OneBackpack || kit.Stratagem4.Backpack != true) replaceableSlots.Add(3);

        if (replaceableSlots.Count <= 0) return;

        var slotToReplace = _random.Next(replaceableSlots.Count);

        switch (replaceableSlots[slotToReplace])
        {
            case 0: kit.Stratagem1 = supportWeapon; break;
            case 1: kit.Stratagem2 = supportWeapon; break;
            case 2: kit.Stratagem3 = supportWeapon; break;
            case 3: kit.Stratagem4 = supportWeapon; break;
        }
    }


    public List<Kit> GetSquadKits(State state, Factors factors)
    {
        var kits = new List<Kit>();
        var stratagemCounts = new Dictionary<string, int>();
        var usedBoosters = new HashSet<string>();

        foreach (var player in state.Players)
        {
            List<Stratagem>? excludedStratagems = null;

            if (factors.MaxAllowedDupesStratagems > 0)
            {
                excludedStratagems = [];

                foreach (var entry in stratagemCounts)
                {
                    if (entry.Value < factors.MaxAllowedDupesStratagems) continue;
                    var stratagem = gameData.Stratagems.FirstOrDefault(s => s.Name == entry.Key);
                    if (stratagem != null) excludedStratagems.Add(stratagem);
                }
            }

            var kit = GetRandomKit(
                factors,
                player.IsReady,
                excludedStratagems,
                usedBoosters,
                player.DisabledWarbonds) ?? player.Kit;

            if (player is { IsReady: true, Kit.Booster: not null })
                usedBoosters.Add(player.Kit.Booster.Name);

            kits.Add(kit);

            if (factors.MaxAllowedDupesStratagems <= 0) continue;
            UpdateStratagemCount(stratagemCounts, kit.Stratagem1);
            UpdateStratagemCount(stratagemCounts, kit.Stratagem2);
            UpdateStratagemCount(stratagemCounts, kit.Stratagem3);
            UpdateStratagemCount(stratagemCounts, kit.Stratagem4);
        }

        return kits;
    }


    private void UpdateStratagemCount(Dictionary<string, int> counts, Stratagem stratagem)
    {
        if (counts.TryGetValue(stratagem.Name, out var value))
            counts[stratagem.Name] = ++value;
        else
            counts[stratagem.Name] = 1;
    }
}