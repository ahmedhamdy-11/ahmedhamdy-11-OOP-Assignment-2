using System.Diagnostics;
using PatternsLab.Problems.Builder;
using PatternsLab.Problems.Prototype;
using PatternsLab.Problems.Singleton;

Console.WriteLine("=== SINGLETON: AFTER ===\n");

var db = new DatabaseService();
var ui = new UiService();

Console.WriteLine();

db.Config.Theme = "Dark";
Console.WriteLine("Admin changed theme to Dark.");

ui.Render();

Console.WriteLine($"\nSame config object? {ReferenceEquals(db.Config, ui.Config)}");
Console.WriteLine($"Times config was loaded from disk: {AppConfig.LoadCount}");

Console.WriteLine("\n=== PROTOTYPE: AFTER ===\n");

var sw = Stopwatch.StartNew();
var army = new List<Enemy>();

// Create prototype once (slow loading happens only once)
var orcPrototype = new Orc();

for (int i = 1; i <= 5; i++)
{
    var orc = orcPrototype.Clone();
    orc.Name = $"Orc-{i}";
    army.Add(orc);
}
Console.WriteLine($"Created 5 orcs in {sw.ElapsedMilliseconds} ms\n");

Enemy original = new Orc();
original.Name = "Boss Orc";
Enemy copy = EnemyCopyHelper.CopyEnemy(original);

Console.WriteLine($"\nOriginal model id: {original.ModelId}");
Console.WriteLine($"Copy model id:     {copy.ModelId}");

copy.Weapon.Damage = 999;
Console.WriteLine($"\nWe changed the COPY's weapon damage to 999.");
Console.WriteLine($"Original's weapon damage is now: {original.Weapon.Damage}");

copy.Abilities.Add("Fire Breath");
Console.WriteLine($"Original abilities: {string.Join(", ", original.Abilities)}");

Console.WriteLine("\nTesting Prototype Registry (Bonus):");
var registry = new EnemyRegistry();
registry.Register("OrcPrototype", new Orc());
registry.Register("ElfPrototype", new Elf());
var clonedFromRegistry = registry.Get("OrcPrototype");
Console.WriteLine($"Cloned from registry: {clonedFromRegistry.Name}, ModelId: {clonedFromRegistry.ModelId}");

Console.WriteLine("\n=== BUILDER: AFTER ===\n");

var liveReg = RegistrationCallSites.CreateLiveStudent();
var videoReg = RegistrationCallSites.CreateVideosOnly();

Console.WriteLine(liveReg);
Console.WriteLine(videoReg);

Console.WriteLine("\nTesting Builder Validation:");
try
{
    var invalidReg = new CourseRegistrationBuilder()
        .WithStudentEmail("invalid@mail.com")
        .WithCourseCode("SEF-101")
        .AsVideosOnly()
        .WithGroupCode("INVALID_GROUP")
        .Build();
}
catch (Exception ex)
{
    Console.WriteLine($"Caught expected exception for invalid registration: {ex.Message}");
}
