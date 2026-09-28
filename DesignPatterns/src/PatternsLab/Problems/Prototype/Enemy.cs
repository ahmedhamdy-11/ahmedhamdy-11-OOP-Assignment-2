namespace PatternsLab.Problems.Prototype;

public class Weapon
{
    public string Name { get; set; } = string.Empty;
    public int Damage { get; set; }

    public Weapon Clone()
    {
        return new Weapon
        {
            Name = Name,
            Damage = Damage
        };
    }
}

public abstract class Enemy
{
    private string _modelData;

    public string Name { get; set; } = string.Empty;
    public int Health { get; set; }
    public Weapon Weapon { get; set; } = new();
    public List<string> Abilities { get; set; } = new();
    public string ModelId => _modelData;

    protected Enemy()
    {
        Console.WriteLine("   ...loading 3D model (slow)...");
        Thread.Sleep(500);
        _modelData = "MODEL_" + Guid.NewGuid().ToString("N")[..6];
    }

    protected Enemy(Enemy target)
    {
        _modelData = target._modelData;
        Name = target.Name;
        Health = target.Health;
        Weapon = target.Weapon?.Clone()!;
        Abilities = target.Abilities != null ? new List<string>(target.Abilities) : new List<string>();
    }

    public abstract Enemy Clone();
}

public class Orc : Enemy
{
    public Orc()
    {
        Name = "Orc";
        Health = 100;
        Weapon = new Weapon { Name = "Axe", Damage = 25 };
        Abilities.Add("Rage");
    }

    protected Orc(Orc target) : base(target)
    {
    }

    public override Enemy Clone() => new Orc(this);
}

public class Elf : Enemy
{
    public Elf()
    {
        Name = "Elf";
        Health = 70;
        Weapon = new Weapon { Name = "Bow", Damage = 18 };
        Abilities.Add("Stealth");
    }

    protected Elf(Elf target) : base(target)
    {
    }

    public override Enemy Clone() => new Elf(this);
}

public static class EnemyCopyHelper
{
    public static Enemy CopyEnemy(Enemy e)
    {
        return e.Clone();
    }
}

public class EnemyRegistry
{
    private readonly Dictionary<string, Enemy> _prototypes = new();

    public void Register(string key, Enemy prototype)
    {
        _prototypes[key] = prototype;
    }

    public Enemy Get(string key)
    {
        if (_prototypes.TryGetValue(key, out var prototype))
        {
            return prototype.Clone();
        }
        throw new KeyNotFoundException($"Prototype with key '{key}' not found.");
    }
}
