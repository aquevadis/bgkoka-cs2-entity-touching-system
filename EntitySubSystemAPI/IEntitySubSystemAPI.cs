using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Capabilities;

namespace EntitySubSystemAPI;

public interface IEntitySubSystemAPI {

    /// <summary>
    /// event OnEntityTouchedByPlayer
    /// </summary>
    public event Action<CEntityInstance, CCSPlayerPawnBase> OnPlayerTouchEntity;

    /// <summary>
    /// Enable touch capabilities for an entity
    /// </summary>
    /// <param name="entity">entity that will fire when touched</param>
    public void StartTouch(CEntityInstance entity);

    /// <summary>
    /// Remove touch capabilities for an entity
    /// </summary>
    /// <param name="entity">entity that will no longer fire when touched</param>
    public void RemovTouch(CEntityInstance entity);

}
