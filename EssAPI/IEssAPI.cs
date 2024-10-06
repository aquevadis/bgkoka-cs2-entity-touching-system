using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using static EssAPI.IEssAPI;

namespace EssAPI;

public interface IEssAPI {

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

    /// <summary>
    /// event OnEntityTouchedByPlayer
    /// </summary>
    public event Action<CEntityInstance, CCSPlayerPawnBase> EntityTouchedByPlayer;
}
