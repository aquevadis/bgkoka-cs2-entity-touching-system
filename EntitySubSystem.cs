using System.Numerics;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Capabilities;
using EssAPI;

namespace EntitySubSystemBase;

[MinimumApiVersion(262)]

public partial class EntitySubSystemBase : BasePlugin, IPluginConfig<CoreConfig>
{
    public override string ModuleName => "Entity Sub System";
    public override string ModuleAuthor => "AquaVadis";
    public override string ModuleVersion => "1.0.3s";
    public required CoreConfig Config { get; set; }
    
    //register API
    public IEssAPI EntitySystemAPI { get; set; } = null!;

    public override void Load(bool hotReload)
    {

        //register api
        EntitySystemAPI = new EssAPI();
        Capabilities.RegisterPluginCapability(IEssAPI.Capability, () => EntitySystemAPI);

        InitializeVirtualFunctions();
        if(Config.DebugMode)
        SendMessageToInternalConsole(msg: "<=========== [EntitySubSystem] Initialize Virtual Functions(+Hooks) ===========>", print: PrintTo.ConsoleInfo);

        //register listeners
        RegisterListeners();
        if(Config.DebugMode)
        SendMessageToInternalConsole(msg: "<=========== [EntitySubSystem] Register Event Listeners ===========>", print: PrintTo.ConsoleInfo);

        if(Config.DebugMode)
        SendMessageToInternalConsole(msg: "<=========== [EntitySubSystem] LOADED SUCCESSFULLY ===========>", print: PrintTo.ConsoleSucess);
        
    }

    public override void Unload(bool hotReload)
    {

        DeinitializeVirtualFunctions();
        if(Config.DebugMode)
        SendMessageToInternalConsole(msg: "<=========== [EntitySubSystem] Deinitialize Virtual Functions(+Hooks) ===========>", print: PrintTo.ConsoleInfo);

        //register listeners
        DeregisterListeners();
        if(Config.DebugMode)
        SendMessageToInternalConsole(msg: "<=========== [EntitySubSystem] Unregister Event Listeners ===========>", print: PrintTo.ConsoleInfo);

        base.Unload(hotReload);

        if(Config.DebugMode)
        SendMessageToInternalConsole(msg: "<=========== [EntitySubSystem] UNLOADED SUCCESSFULLY ===========>", print: PrintTo.ConsoleSucess);  
    
    }

    public void OnConfigParsed(CoreConfig config)
    {
        // Once we've validated the config, we can set it to the instance
        Config = config;
    }

}