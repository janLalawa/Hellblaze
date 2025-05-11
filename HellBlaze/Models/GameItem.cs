using System.Text.Json.Serialization;

namespace HellBlaze.Models;

public class GameItem
{
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    [JsonPropertyName("warbond")] public string Warbond { get; set; } = string.Empty;
}

public class Weapon : GameItem
{
    [JsonPropertyName("ap")] public decimal AP { get; set; }
    [JsonPropertyName("type")] public string? Type { get; set; } = null;
    [JsonPropertyName("fire")] public decimal Fire { get; set; } = 0;
    [JsonPropertyName("precision")] public decimal Precision { get; set; } = 0;
    [JsonPropertyName("electrical")] public decimal Electrical { get; set; } = 0;
    [JsonPropertyName("laser")] public decimal Laser { get; set; } = 0;
    [JsonPropertyName("explosive")] public decimal Explosive { get; set; } = 0;
    [JsonPropertyName("anti_tank")] public decimal AntiTank { get; set; } = 0;
    [JsonPropertyName("gas")] public decimal Gas { get; set; } = 0;
    [JsonPropertyName("smoke")] public decimal Smoke { get; set; } = 0;
    [JsonPropertyName("anti_horde")] public decimal AntiHorde { get; set; } = 0;
    [JsonPropertyName("support")] public decimal Support { get; set; } = 0;

    public decimal TotalWeight => Fire + Precision + Electrical + Laser + Explosive + AntiTank + Gas + Smoke + AntiHorde + Support;
}

public class Stratagem : GameItem
{
    [JsonPropertyName("icon")] public string Icon { get; set; } = "missingicon.svg";
    [JsonPropertyName("backpack")] public bool Backpack { get; set; } = false;
    [JsonPropertyName("support_weapon")] public bool SupportWeapon { get; set; }
    [JsonPropertyName("category")] public string Category { get; set; } = string.Empty;
    [JsonPropertyName("explosive")] public decimal Explosive { get; set; } = 0;
    [JsonPropertyName("fire")] public decimal Fire { get; set; } = 0;
    [JsonPropertyName("anti_tank")] public decimal AntiTank { get; set; } = 0;
    [JsonPropertyName("laser")] public decimal Laser { get; set; } = 0;
    [JsonPropertyName("precision")] public decimal Precision { get; set; } = 0;
    [JsonPropertyName("electrical")] public decimal Electrical { get; set; } = 0;
    [JsonPropertyName("gas")] public decimal Gas { get; set; } = 0;
    [JsonPropertyName("anti_horde")] public decimal AntiHorde { get; set; } = 0;
    [JsonPropertyName("smoke")] public decimal Smoke { get; set; } = 0;
    [JsonPropertyName("mine")] public decimal Mine { get; set; } = 0;
    [JsonPropertyName("turret")] public decimal Turret { get; set; } = 0;
    [JsonPropertyName("eagle")] public decimal Eagle { get; set; } = 0;
    [JsonPropertyName("orbital")] public decimal Orbital { get; set; } = 0;
    [JsonPropertyName("support")] public decimal Support { get; set; } = 0;

    public decimal TotalWeight => Fire + Precision + Electrical + Laser + Explosive + AntiTank + Gas + Smoke + AntiHorde + Mine + Turret + Eagle + Orbital + Support;
}

public class Throwable : GameItem
{
    [JsonPropertyName("destroys_spawners")] public bool DestroysSpawners { get; set; }
    [JsonPropertyName("fire")] public decimal Fire { get; set; } = 0;
    [JsonPropertyName("explosive")] public decimal Explosive { get; set; } = 0;
    [JsonPropertyName("smoke")] public decimal Smoke { get; set; } = 0;
    [JsonPropertyName("gas")] public decimal Gas { get; set; } = 0;
    [JsonPropertyName("anti_tank")] public decimal AntiTank { get; set; } = 0;
    [JsonPropertyName("laser")] public decimal Laser { get; set; } = 0;
    [JsonPropertyName("precision")] public decimal Precision { get; set; } = 0;
    [JsonPropertyName("electrical")] public decimal Electrical { get; set; } = 0;
    [JsonPropertyName("anti_horde")] public decimal AntiHorde { get; set; } = 0;
    [JsonPropertyName("support")] public decimal Support { get; set; } = 0;

    public decimal TotalWeight => Fire + Precision + Electrical + Laser + Explosive + AntiTank + Gas + Smoke + AntiHorde + Support;
}

public class Armor : GameItem
{
    [JsonPropertyName("icon")] public string Icon { get; set; } = string.Empty;
    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
}

public class Booster : GameItem
{
    [JsonPropertyName("icon")] public string Icon { get; set; } = "missingbooster.svg";
    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
    [JsonPropertyName("price")] public int Price { get; set; }
}

public class ArmorData
{
    [JsonPropertyName("armors")] public List<Armor> Armors { get; set; } = [];
}

public class BoosterData
{
    [JsonPropertyName("boosters")] public List<Booster> Boosters { get; set; } = [];
}

public class PrimaryWeaponsData
{
    [JsonPropertyName("primary")] public List<Weapon> Primary { get; set; } = [];
}

public class SecondaryWeaponsData
{
    [JsonPropertyName("secondary")] public List<Weapon> Secondary { get; set; } = [];
}

public class StratagemsData
{
    [JsonPropertyName("stratagems")] public List<Stratagem> Stratagems { get; set; } = [];
}

public class ThrowablesData
{
    [JsonPropertyName("throwable")] public List<Throwable> Throwable { get; set; } = [];
}

public class Rank
{
    [JsonPropertyName("title")] public string Name { get; set; } = "Cadet";
    [JsonPropertyName("iconPath")] public string Icon { get; set; } = "images/ranks/CadetIcon.svg";
}

public class RanksData
{
    [JsonPropertyName("ranks")] public required List<Rank> Ranks { get; set; }
}