using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SoundExtensions.Configuration;
using System.Reflection;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace SoundExtensions
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal const string CAPABILITY = "Sound Extensions";
        internal const string HARMONYID = "com.nyamimi.BeatSaber.SoundExtensions";

        internal static IPALogger Log { get; private set; }

        internal static Harmony HarmonyInstance { get; private set; } = new Harmony(HARMONYID);

        [Init]
        public void Init(IPALogger logger, Config config)
        {
            Log = logger;

            SoundExtensionsConfig.Instance = config.Generated<SoundExtensionsConfig>();

            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

            SongCore.Collections.RegisterCapability(CAPABILITY);

            Log.Info("SoundExtensions initialized.");
        }

        [OnStart]
        public void OnApplicationStart() =>
            new GameObject("SoundExtensionsController").AddComponent<SoundExtensionsController>();

        [OnExit]
        public void OnApplicationQuit() =>
            HarmonyInstance.UnpatchAll(HARMONYID);
    }
}
