using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;

namespace EntitySubSystemBase;

public partial class EntitySubSystemBase
{
    
    public static MemoryFunctionVoid<CCSPlayerPawnBase>? CCSPlayerPawnBase_PostThinkFunc;

    public void InitializeVirtualFunctions() {

        CCSPlayerPawnBase_PostThinkFunc = new (GameData.GetSignature("CCSPlayerPawnBase_PostThink"));

        //hook the post pawn think
        CCSPlayerPawnBase_PostThinkFunc.Hook(PawnPostThinkFunc, HookMode.Pre);
    }

    public void DeinitializeVirtualFunctions() {

        CCSPlayerPawnBase_PostThinkFunc?.Unhook(PawnPostThinkFunc, HookMode.Pre);
    }

    /// <summary>
    /// Entry point for the thinking logic of entities that are most likely to interect with a player and not another entity
    /// </summary>
    public HookResult PawnPostThinkFunc(DynamicHook hook) {

        if (Server.TickCount % 16 != 0) return HookResult.Continue;

        var playerPawnBase = hook.GetParam<CCSPlayerPawnBase>(0);
        if (playerPawnBase is null || playerPawnBase.IsValid is not true) return HookResult.Continue;

        var playerPawn = playerPawnBase.As<CCSPlayerPawn>();
        
        if (playerPawn is null 
                        || playerPawn.IsValid is not true || playerPawn.LifeState is not (byte)LifeState_t.LIFE_ALIVE) return HookResult.Continue;

        EntityTouch.OnPlayerEntityThink(playerPawn);

        return HookResult.Continue;

    }

}