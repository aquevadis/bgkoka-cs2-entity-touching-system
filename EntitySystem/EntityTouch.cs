using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace EntitySubSystemBase;

public static class EntityTouch {

    /// <summary>
    /// Global list for all entities that have enabled touching capabilities
    /// </summary>
    internal static readonly List</*entity index*/uint> _entities_have_touch = new();

    //construct
    static EntityTouch()
    {
    }

    public static void StartTouch(this CEntityInstance entity) {

        if (entity is null || entity.ValidateEntity() is not true) return;

        //avoid adding the entity twice
        if (_entities_have_touch.Contains(entity.Index) is true) return;

        _entities_have_touch.Add(entity.Index);

        //debug log:
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[EntitySubSystem][StartTouchOnEntity] Started Touch On entity index {entity.Index} (added to list)");
        Console.ResetColor();
        
    }

    public static void RemoveTouch(this CEntityInstance entity) {

        if (entity is null || entity.ValidateEntity() is not true) return;

        if (_entities_have_touch.Contains(entity.Index) is not true) return;

        _entities_have_touch.Add(entity.Index);

        //debug log:
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[EntitySubSystem][RemoveTouchOnEntity] Removed Touch On entity index {entity.Index} (added to list)");
        Console.ResetColor();

    }

    public static void OnPlayerEntityThink(CCSPlayerPawn playerPawn) {

        foreach (var entity_has_touch in _entities_have_touch)
        {
            
            var entity = Utilities.GetEntityFromIndex<CBaseEntity>((int)entity_has_touch);
            if (entity is null || entity.ValidateEntity() is not true) continue;
            
            if (entity.AbsOrigin is null || playerPawn.AbsOrigin is null) continue;

            if (Entities.Collides(entity.AbsOrigin, playerPawn.AbsOrigin)) {

                var player = playerPawn.OriginalController.Value;
                if (player is null || player.ValidateEntity() is not true) continue;

                //debug log:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[OnEntityTouchByPlayer] {entity.DesignerName} touched by {player.DesignerName}");
                Console.ResetColor();

            }
        }
        
    }


}