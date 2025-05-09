using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace HellBlaze.Models;

public class SharedLoadout
{
    public List<DiverLoadout> DiverLoadouts { get; set; } = new();
    public Factors Factors { get; set; } = new();

    public string GenerateShortId()
    {
        var json = JsonSerializer.Serialize(this);
        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(json));
        return Convert.ToBase64String(hash).Substring(0, 8)
                      .Replace('/', '_').Replace('+', '-');
    }

    public class DiverLoadout
    {
        public string Name { get; set; } = string.Empty;
        public Kit Kit { get; set; } = new();
    }
}