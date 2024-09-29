using CounterStrikeSharp.API.Core;
using static EntitySubSystemBase.EntityTouch;

namespace EntitySubSystemBase;

public static class EntitySystem {

    static EntitySystem()
    {
    }

    public static void ClearAllManagedEntities() {

        //clear the list of _entities_has_touch
        if (_entities_have_touch.Count > 0)
            _entities_have_touch.Clear();

        //debug log:
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[EntitySubSystem] Cleared all managed entities");
        Console.ResetColor();

    }

    public static void OnEntityCreated(CEntityInstance entity) {

        if (entity.ValidateEntity() is not true) return;

        if (entity.DesignerName.Contains("ak47") is true)
            entity.StartTouch();

        //debug log:
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[EntitySubSystem][OnEntityCreated] Added entity index {entity.Index} to the list");
        Console.ResetColor();
    }

    public static void OnEntityRemoved(CEntityInstance entity) {

        if (entity.ValidateEntity() is not true) return;


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