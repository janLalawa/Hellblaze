namespace HellBlaze.Models;

public class Diver
{
    public string Name { get; set; } = "Player";
    public Rank Rank { get; set; }
    public bool IsReady { get; set; }
    public Kit Kit { get; set; } = new();
    public HashSet<string> DisabledWarbonds { get; set; } = new();
}

public class Kit
{
    public Weapon Primary { get; set; } = new();
    public Weapon Secondary { get; set; } = new();
    public Throwable Throwable { get; set; } = new();
    public Stratagem Stratagem1 { get; set; } = new();
    public Stratagem Stratagem2 { get; set; } = new();
    public Stratagem Stratagem3 { get; set; } = new();
    public Stratagem Stratagem4 { get; set; } = new();
    public Armor Armor { get; set; } = new();
    public Booster Booster { get; set; } = new();
}