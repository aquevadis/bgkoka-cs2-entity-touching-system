using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using EssAPI;

namespace EntitySubSystemBase;

public class EssAPI : IEssAPI {

    public event Action<CEntityInstance, CCSPlayerPawnBase>? EntityTouchedByPlayer;

    /// <summary>
    /// Enable touch capabilities for an entity
    /// </summary>
    /// <param name="entity">entity that will fire when touched</param>
    public void StartTouch(CEntityInstance entity) {
        
        entity.StartTouch();

        //debug log:
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[EntitySubSystem][API] Started Touch On entity index {entity.Index} (added to list)");
        Console.ResetColor();
    }

    /// <summary>
    /// Remove touch capabilities for an entity
    /// </summary>
    /// <param name="entity">entity that will no longer fire when touched</param>
    public void RemovTouch(CEntityInstance entity) {

        entity.RemoveTouch();

        //debug log:
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[EntitySubSystem][API] Remove Touch On entity index {entity.Index} (added to list)");
        Console.ResetColor();
    }

    public virtual void OnEntityTouchedByPlayer(CEntityInstance touchedEntity, CCSPlayerPawnBase touchingPlayerPawnBase) {

        EntityTouchedByPlayer?.Invoke(touchedEntity, touchingPlayerPawnBase);

        //debug log:
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[OnEntityTouchByPlayer][API] {touchedEntity.DesignerName} touched by {touchingPlayerPawnBase.DesignerName}");
        Console.ResetColor();

    }

}
