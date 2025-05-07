using HellBlaze.Constants;

namespace HellBlaze.Models;

public class Factors
{
    public decimal BaseFactor { get; set; } = Config.BaseFactor;
    public int MaxAllowedDupesStratagems { get; set; } = 0;
    public bool OneBackpack { get; set; } = true;
    public bool OneSupportWeapon { get; set; } = true;
    public bool MustIncludeSupportWeapon { get; set; } = true;
    public decimal ExplosiveFactor { get; set; } = 0;
    public decimal FireFactor { get; set; } = 0;
    public decimal AntiTankFactor { get; set; } = 0;
    public decimal LaserFactor { get; set; } = 0;
    public decimal PrecisionFactor { get; set; } = 0;
    public decimal ElectricalFactor { get; set; } = 0;
    public decimal GasFactor { get; set; } = 0;
    public decimal SmokeFactor { get; set; } = 0;
    public decimal AntiHordeFactor { get; set; } = 0;
    public decimal MineFactor { get; set; } = 0;
    public decimal TurretFactor { get; set; } = 0;
}