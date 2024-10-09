using CounterStrikeSharp.API.Core;
using EntitySubSystemAPI;

namespace EntitySubSystemBase;

public class EntitySubSystemAPI : IEntitySubSystemAPI {

    public event Action<CEntityInstance, CCSPlayerPawnBase>? OnPlayerTouchEntity;

    /// <summary>
    /// Enable touch capabilities for an entity
    /// </summary>
    /// <param name="entity">entity that will fire when touched</param>
    public void StartTouch(CEntityInstance entity) {
        
        EntitySubSystemBase.StartTouch(entity);

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

        EntitySubSystemBase.RemoveTouch(entity);

        //debug log:
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[EntitySubSystem][API] Remove Touch On entity index {entity.Index} (added to list)");
        Console.ResetColor();
    }

    public void PlayerTouchEntity(CEntityInstance touchedEntity, CCSPlayerPawnBase touchingPlayerPawnBase) {

        OnPlayerTouchEntity?.Invoke(touchedEntity, touchingPlayerPawnBase);

    }

}
