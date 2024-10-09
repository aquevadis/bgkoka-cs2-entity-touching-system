using CounterStrikeSharp.API.Core;
using static EntitySubSystemAPI.IEntitySubSystemAPI;

namespace EntitySubSystemBase;

public partial class EntitySubSystemBase {

    /// <summary>
    /// Global list for all entities that have enabled touching capabilities
    /// </summary>
    internal static readonly List<CEntityInstance> _entities_have_touch = new();

    /// <summary>
    /// Enable touch capabilities for an entity
    /// </summary>
    /// <param name="entity">entity that will fire when touched</param>
    public static void StartTouch(CEntityInstance entity) {

        if (entity is null || entity.ValidateEntity() is not true) return;

        //avoid adding the entity twice
        if (_entities_have_touch.Contains(entity) is true) return;

        _entities_have_touch.Add(entity);
        
    }

    /// <summary>
    /// Remove touch capabilities for an entity
    /// </summary>
    /// <param name="entity">entity that will no longer fire when touched</param>
    public static void RemoveTouch(CEntityInstance entity) {

        if (entity is null || entity.ValidateEntity() is not true) return;

        if (_entities_have_touch.Contains(entity) is not true) return;

        _entities_have_touch.Remove(entity);

    }

    /// <summary>
    /// Initiated from each player
    /// Called in Core/Hooks.cs
    /// </summary>
    /// <param name="playerPawn">the playerPawn that is currently thinking</param>
    public void OnPlayerEntityThink(CCSPlayerPawnBase playerPawnBase) {

        //only valid,alive and player pawns can think
        if (playerPawnBase is null  || playerPawnBase.IsValid is not true 
        || playerPawnBase.LifeState is not (byte)LifeState_t.LIFE_ALIVE
        || playerPawnBase.DesignerName.Contains("player") is not true) return;

        //cycle through the thinkinh entities to fire touch
        foreach (var entity_has_touch in _entities_have_touch)
        {
            
            var base_touching_entity = entity_has_touch.As<CBaseEntity>();
            if (base_touching_entity is null || base_touching_entity.ValidateEntity() is not true) continue;
            
            if (base_touching_entity.AbsOrigin is null || playerPawnBase.AbsOrigin is null) continue;

            if (Entities.Collides(base_touching_entity.AbsOrigin, playerPawnBase.AbsOrigin)) {

                OnEntityTouchedByPlayerBase(base_touching_entity, playerPawnBase);

            }
        }
        
    }

    /// <summary>
    /// forward event to API to be invoked to all consumers
    /// </summary>
    /// <param name="touchedEntity">Entity that has been touched</param>
    /// <param name="touchingPlayerPawnBase">The player pawn that touched the entity</param>
    public void OnEntityTouchedByPlayerBase(CEntityInstance touchedEntity, CCSPlayerPawnBase touchingPlayerPawnBase) {
        
       Api.PlayerTouchEntity(touchedEntity, touchingPlayerPawnBase);

    }

}