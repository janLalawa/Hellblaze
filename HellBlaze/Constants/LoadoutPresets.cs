using HellBlaze.Models;
using MudBlazor;

namespace HellBlaze.Constants;

public static class LoadoutPresets
{
    public enum PresetType
    {
        ILikeDots,
        HazyClouds,
        BoomSquad,
        ShockAndAwe,
        Rammstein,
        MixedLoadout,
        Recon,
        WarCrimeWednesday,
        SupportSquad,
        CyborgUprising,
        Reset,
        DailyChallenge
    }


    public static readonly Dictionary<PresetType, PresetInfo> Presets = new()
    {
        {
            PresetType.ILikeDots,
            new PresetInfo(
                "I like Damage over Time",
                Color.Warning,
                new Factors { FireFactor = 8, GasFactor = 8, TurretFactor = 1, OrbitalFactor = 2, MaxAllowedDupesStratagems = 2 },
                "Throw it and run",
                "🔥"
            )
        },
        {
            PresetType.HazyClouds,
            new PresetInfo(
                "Hazy Clouds",
                Color.Success,
                new Factors { GasFactor = 3, SmokeFactor = 3, MaxAllowedDupesStratagems = 2 },
                "Hobox the Hellpod.",
                "🌱"
            )
        },
        {
            PresetType.BoomSquad,
            new PresetInfo(
                "Boom Squad",
                Color.Error,
                new Factors { ExplosiveFactor = 6, MineFactor = 2, OrbitalFactor = 2, AntiTankFactor = 2, MaxAllowedDupesStratagems = 2 },
                "エクスプロージョン!",
                "💥"
            )
        },
        {
            PresetType.ShockAndAwe,
            new PresetInfo(
                "Shock And Awe",
                Color.Info,
                new Factors { ExplosiveFactor = 2, ElectricalFactor = 6, AntiTankFactor = 2, MaxAllowedDupesStratagems = 2 },
                "zzzzzap",
                "⚡"
            )
        },
        {
            PresetType.Rammstein,
            new PresetInfo(
                "Rammstein Concert",
                Color.Tertiary,
                new Factors { LaserFactor = 4, SmokeFactor = 2, AntiTankFactor = 3, FireFactor = 5, ExplosiveFactor = 3, SupportFactor = 2, MaxAllowedDupesStratagems = 2 },
                "Wollt ihr das Bett in Flammen sehen?",
                "🎸"
            )
        },
        {
            PresetType.MixedLoadout,
            new PresetInfo(
                "Mixed Loadout",
                Color.Primary,
                new Factors { AntiTankFactor = 4, AntiHordeFactor = 4, TurretFactor = 1, PrecisionFactor = 1.5m, ExplosiveFactor = 1, SupportFactor = 1.5m },
                "Hopefully this one is good!",
                "👌️"
            )
        },
        {
            PresetType.Recon,
            new PresetInfo(
                "Recon",
                Color.Dark,
                new Factors { SmokeFactor = 8, PrecisionFactor = 9, AntiTankFactor = 2, SupportFactor = 6, MaxAllowedDupesStratagems = 3 },
                "Feel free to pick some recon armour for this one",
                "️👻"
            )
        },
        {
            PresetType.WarCrimeWednesday,
            new PresetInfo(
                "War Crime Wednesday",
                Color.Error,
                new Factors { FireFactor = 5, GasFactor = 5, MineFactor = 3, MaxAllowedDupesStratagems = 4 },
                "Geneva Convention? More like Geneva Suggestion!",
                "☠️"
            )
        },
        {
            PresetType.SupportSquad,
            new PresetInfo(
                "Support Squad",
                Color.Success,
                new Factors { SupportFactor = 8, MustIncludeSupportWeapon = true, MaxAllowedDupesStratagems = 1, OneBackpack = false, OneSupportWeapon = false, EagleFactor = 2 },
                "I'm not carrying ammo, I'm deploying tactical resupply points!",
                "⚒️"
            )
        },
        {
            PresetType.CyborgUprising,
            new PresetInfo(
                "Cyborg Uprising",
                Color.Secondary,
                new Factors { LaserFactor = 8, ElectricalFactor = 5, TurretFactor = 3 },
                "Automatons are on the wrong side of history, you know.",
                "🤖"
            )
        },
        {
            PresetType.Reset,
            new PresetInfo(
                "Reset All Factors",
                Color.Dark,
                new Factors(),
                "All factors reset to default!",
                ""
            )
        }
    };

    public static Factors GetDailyChallengeFactors()
    {
        var today = DateTime.UtcNow.Date;
        var seed = today.Year * 10000 + today.Month * 100 + today.Day;
        var random = new Random(seed);

        var factors = new Factors
        {
            BaseFactor = random.Next(0, 10),
            MaxAllowedDupesStratagems = 0,
            OneBackpack = random.Next(0, 2) == 1,
            OneSupportWeapon = random.Next(0, 2) == 1,
            MustIncludeSupportWeapon = random.Next(0, 2) == 1,
            ExplosiveFactor = random.Next(0, 10),
            FireFactor = random.Next(0, 10),
            AntiTankFactor = random.Next(0, 10),
            LaserFactor = random.Next(0, 10),
            PrecisionFactor = random.Next(0, 10),
            ElectricalFactor = random.Next(0, 10),
            GasFactor = random.Next(0, 10),
            SmokeFactor = random.Next(0, 10),
            AntiHordeFactor = random.Next(0, 10),
            MineFactor = random.Next(0, 10),
            TurretFactor = random.Next(0, 10),
            EagleFactor = random.Next(0, 10),
            OrbitalFactor = random.Next(0, 10),
            SupportFactor = random.Next(0, 10)
        };

        return factors;
    }

    public static (string Message, string Emoji) GetDailyChallengeNotification()
    {
        var today = DateTime.UtcNow.Date;
        var seed = today.Year * 10000 + today.Month * 100 + today.Day;
        var random = new Random(seed);

        var messages = new[]
        {
            "Helldivers Daily Challenge Activated!",
            "Super Earth Commands: Complete Today's Mission!",
            "For Democracy: Daily Challenge Engaged!",
            "Daily Orders From Command: Good Luck Soldier!",
            "Daily Challenge: Make Super Earth Proud!"
        };

        var emojis = new[] { "🌟", "⭐", "🎖️", "🏆", "🔥", "⚔️" };

        return (messages[random.Next(messages.Length)], emojis[random.Next(emojis.Length)]);
    }

    public static Factors GetFactors(PresetType preset)
    {
        if (Presets.TryGetValue(preset, out var info))
            return new Factors
            {
                FireFactor = info.Factors.FireFactor,
                GasFactor = info.Factors.GasFactor,
                ElectricalFactor = info.Factors.ElectricalFactor,
                AntiTankFactor = info.Factors.AntiTankFactor,
                LaserFactor = info.Factors.LaserFactor,
                ExplosiveFactor = info.Factors.ExplosiveFactor,
                MineFactor = info.Factors.MineFactor,
                SmokeFactor = info.Factors.SmokeFactor,
                EagleFactor = info.Factors.EagleFactor,
                OrbitalFactor = info.Factors.OrbitalFactor,
                SupportFactor = info.Factors.SupportFactor,
                AntiHordeFactor = info.Factors.AntiHordeFactor,
                PrecisionFactor = info.Factors.PrecisionFactor,
                TurretFactor = info.Factors.TurretFactor,
                OneBackpack = info.Factors.OneBackpack,
                OneSupportWeapon = info.Factors.OneSupportWeapon,
                MaxAllowedDupesStratagems = info.Factors.MaxAllowedDupesStratagems,
                MustIncludeSupportWeapon = info.Factors.MustIncludeSupportWeapon
            };

        return new Factors();
    }


    public static (string Message, string Emoji) GetNotification(PresetType preset)
    {
        if (Presets.TryGetValue(preset, out var info))
            return (info.Message, info.Emoji);

        return ("Unknown preset", "");
    }

    public static IEnumerable<(PresetType Type, PresetInfo Info)> GetAllPresets()
    {
        return Presets.Select(kvp => (kvp.Key, kvp.Value));
    }

    public class PresetInfo
    {
        public PresetInfo(string displayName, Color buttonColor, Factors factors, string message, string emoji)
        {
            DisplayName = displayName;
            ButtonColor = buttonColor;
            Factors = factors;
            Message = message;
            Emoji = emoji;
        }

        public string DisplayName { get; }
        public Color ButtonColor { get; }
        public Factors Factors { get; }
        public string Message { get; }
        public string Emoji { get; }
    }
}