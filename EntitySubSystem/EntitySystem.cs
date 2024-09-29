using CounterStrikeSharp.API.Core;

namespace EntitySubSystemBase;

public static class EntitySystem {

    /// <summary>
    /// Global list for all entities
    /// </summary>
    internal static readonly List</*entity index*/uint> _entities_indexes = new();

    static EntitySystem()
    {
    }

    public static void ClearAllManagedEntities() {

        //clear the list of _entities_has_touch
        if (EntityTouch._entities_have_touch.Count > 0)
            EntityTouch._entities_have_touch.Clear();

        if (_entities_indexes.Count > 0)
            _entities_indexes.Clear();

        //debug log:
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[EntitySubSystem] Cleared all managed entities");
        Console.ResetColor();

    }

    public static void OnEntityCreated(CEntityInstance entity) {

        if (entity.ValidateEntity() is not true) return;

        _entities_indexes.Add(entity.Index);

        //debug log:
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[EntitySubSystem][OnEntityCreated] Added entity index {entity.Index} to the list");
        Console.ResetColor();
    }

    public static void OnEntityRemoved(CEntityInstance entity) {

        if (entity.ValidateEntity() is not true) return;

        if (_entities_indexes.Contains(entity.Index) is not true) return;

        _entities_indexes.Remove(entity.Index);

        //debug log:
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[EntitySubSystem][OnEntityCreated] Removed entity index {entity.Index} from the list");
        Console.ResetColor();
    }

    // public CBaseEntity? Add()
    // {

    //     return default;
    // }
}