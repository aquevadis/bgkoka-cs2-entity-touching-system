using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Entities;
using static EntitySubSystemBase.EntityTouch;
using static EntitySubSystemBase.EntitySystem;

namespace EntitySubSystemBase;

public partial class EntitySubSystemBase
{
    
    private void RegisterListeners()
    {
        //register listeners
        RegisterListener<Listeners.OnClientAuthorized>(OnClientAuthorized);
        RegisterListener<Listeners.OnClientDisconnect>(OnClientDisconnected);
        RegisterListener<Listeners.OnTick>(OnTick);
        RegisterListener<Listeners.OnEntityCreated>(OnEntityCreatedBase);
        RegisterListener<Listeners.OnEntityDeleted>(OnEntityDeletedBase);

        //register event listeners
        RegisterEventHandler<EventRoundEnd>(OnRoundEnd);
        RegisterEventHandler<EventItemEquip>(OnItemEquip);

    }

    private void DeregisterListeners()
    {
        //remove listeners
        RemoveListener<Listeners.OnClientAuthorized>(OnClientAuthorized);
        RemoveListener<Listeners.OnClientDisconnect>(OnClientDisconnected);
        RemoveListener<Listeners.OnTick>(OnTick);
        RemoveListener<Listeners.OnEntityCreated>(OnEntityCreatedBase);
        RemoveListener<Listeners.OnEntityDeleted>(OnEntityDeletedBase);

        //deregister event listeners
        DeregisterEventHandler<EventRoundEnd>(OnRoundEnd);
        DeregisterEventHandler<EventItemEquip>(OnItemEquip);

    }

    public void OnTick() {

        

    }

    public static void OnEntityCreatedBase(CEntityInstance entity)
	{
        if (entity.ValidateEntity() is not true) return;
        
        if (entity.DesignerName.Contains("ak47"))
            entity.StartTouch();
        
        OnEntityCreated(entity);
    }

    public static void OnEntityDeletedBase(CEntityInstance entity)
	{
        OnEntityRemoved(entity);
    }

    private void OnClientAuthorized(int slot, SteamID steamid)
    {

        var player = Utilities.GetPlayerFromSlot(slot);
        if (player is null || player.IsValid is not true) return;

        _cachedPlayers.Add(player.Slot);
    }

    private void OnClientDisconnected(int slot)
    {

        var player = Utilities.GetPlayerFromSlot(slot);
        if (player is null || player.IsValid is not true) return;

        if (_cachedPlayers.Contains(player.Slot))
            _cachedPlayers.Remove(player.Slot); 
    }

    private HookResult OnItemPickUpPre(EventItemPickup @event, GameEventInfo info)
    {
        var player = @event.Userid;
        if (player == null || player.IsValid is not true || player.PawnIsAlive is not true || player.IsHLTV is true) return HookResult.Continue;
        
        var playerPawn = player!.PlayerPawn.Value;
        if (playerPawn == null || playerPawn.IsValid is not true) return HookResult.Continue;

        var weapon = playerPawn.WeaponServices?.ActiveWeapon.Value;
        if (weapon is null || weapon.ValidateEntity() is not true) return HookResult.Continue;

        if (_entities_have_touch.Contains(weapon.Index) is true) 
            _entities_have_touch.Remove(weapon.Index);

        return HookResult.Continue;
    }

    private HookResult OnItemEquip(EventItemEquip @event, GameEventInfo info)
    {

        var player = @event.Userid;
        if (player == null || player.IsValid is not true || player.PawnIsAlive is not true || player.IsHLTV is true) return HookResult.Continue;
        
        var playerPawn = player!.PlayerPawn.Value;
        if (playerPawn == null || playerPawn.IsValid is not true) return HookResult.Continue;

        var weapons = playerPawn.WeaponServices?.MyWeapons;
        if (weapons is null ) return HookResult.Continue;

        foreach (var weapon in weapons) {

            if (weapon is null || weapon.IsValid is not true) continue;

            if (_entities_have_touch.Contains(weapon.Index) is true) 
                _entities_have_touch.Remove(weapon.Index);
        }

        return HookResult.Continue;

    }

    private HookResult OnRoundEnd(EventRoundEnd @event, GameEventInfo info)
    {

        ClearAllManagedEntities();

        return HookResult.Continue;

    }


}