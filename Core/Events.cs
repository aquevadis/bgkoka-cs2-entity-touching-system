﻿using CounterStrikeSharp.API;
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
        RegisterListener<Listeners.OnEntityCreated>(OnEntityCreatedBase);
        RegisterListener<Listeners.OnEntityDeleted>(OnEntityDeletedBase);

        //register event listeners
        RegisterEventHandler<EventRoundEnd>(OnRoundEnd);
        RegisterEventHandler<EventItemEquip>(OnItemEquipPre, HookMode.Pre);

    }

    private void DeregisterListeners()
    {
        //remove listeners
        RemoveListener<Listeners.OnEntityCreated>(OnEntityCreatedBase);
        RemoveListener<Listeners.OnEntityDeleted>(OnEntityDeletedBase);

        //deregister event listeners
        DeregisterEventHandler<EventRoundEnd>(OnRoundEnd);
        DeregisterEventHandler<EventItemEquip>(OnItemEquipPre, HookMode.Pre);

    }

    public static void OnEntityCreatedBase(CEntityInstance entity)
	{
        OnEntityCreated(entity);
    }

    public static void OnEntityDeletedBase(CEntityInstance entity)
	{
        OnEntityRemoved(entity);
    }

    private HookResult OnItemEquipPre(EventItemEquip @event, GameEventInfo info)
    {

        var player = @event.Userid;
        if (player == null || player.IsValid is not true || player.PawnIsAlive is not true || player.IsHLTV is true) return HookResult.Continue;
        
        var playerPawn = player!.PlayerPawn.Value;
        if (playerPawn == null || playerPawn.IsValid is not true) return HookResult.Continue;

        var weapon = playerPawn.WeaponServices?.ActiveWeapon.Value;
        if (weapon is null || weapon.ValidateEntity() is not true) return HookResult.Continue;
        
        if (_entities_have_touch.Contains(weapon.As<CEntityInstance>()) is true) 
            _entities_have_touch.Remove(weapon.As<CEntityInstance>());

        return HookResult.Continue;
    }

    private HookResult OnRoundEnd(EventRoundEnd @event, GameEventInfo info)
    {

        ClearAllManagedEntities();
        return HookResult.Continue;
    }


}