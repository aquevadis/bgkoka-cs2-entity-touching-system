using CounterStrikeSharp.API.Core;
using static EntitySubSystemBase.EntityTouch;

namespace EntitySubSystemBase;

public static class EntitySystem {

    static EntitySystem()
    {
    }

    //callled in Core/Events.cs
    public static void ClearAllManagedEntities() {

        //clear the list of _entities_have_touch
        if (_entities_have_touch.Count > 0)
            _entities_have_touch.Clear();

        //debug log:
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[EntitySubSystem] Cleared all managed entities");
        Console.ResetColor();

    }

    //callled in Core/Events.cs
    public static void OnEntityCreated(CEntityInstance entity) {

        if (entity.ValidateEntity() is not true) return;

        //test touch for ak47 created entities
        if (entity.DesignerName.Contains("ak47") is true)
            entity.StartTouch();
  
    }

    //callled in Core/Events.cs
    public static void OnEntityRemoved(CEntityInstance entity) {

        if (entity.ValidateEntity() is not true) return;

        if (_entities_have_touch.Contains(entity) is not true) return;
        
        //remove the touching capability if the self entity is removed itself
        _entities_have_touch.Remove(entity);

    }

}