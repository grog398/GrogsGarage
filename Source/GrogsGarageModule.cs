using System;
using Celeste.Mod.GrogsGarage.Entities.Ports;
using Celeste.Mod.GrogsGarage.Entities;

namespace Celeste.Mod.GrogsGarage;

public class GrogsGarageModule : EverestModule {
    public static GrogsGarageModule Instance { get; private set; }

    public override Type SettingsType => typeof(GrogsGarageModuleSettings);
    public static GrogsGarageModuleSettings Settings => (GrogsGarageModuleSettings) Instance._Settings;

    public override Type SessionType => typeof(GrogsGarageModuleSession);
    public static GrogsGarageModuleSession Session => (GrogsGarageModuleSession) Instance._Session;

    public override Type SaveDataType => typeof(GrogsGarageModuleSaveData);
    public static GrogsGarageModuleSaveData SaveData => (GrogsGarageModuleSaveData) Instance._SaveData;

    public GrogsGarageModule() {
        Instance = this;
#if DEBUG
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(GrogsGarageModule), LogLevel.Verbose);
#else
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(GrogsGarageModule), LogLevel.Info);
#endif
    }

    public override void Load()
    {
        DeathRedirectController.Load();
        ForceLoadSpinners.Load();
    }

    public override void Unload() {
        DeathRedirectController.Unload();
        ForceLoadSpinners.Unload();
    }
}