using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityModManagerNet;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.JsonSystem;
using BlackGoat.Feats.Damnation;
using BlackGoat.Components;

namespace BlackGoat;

#if DEBUG
[EnableReloading]
#endif
public static class Main {
    internal static Harmony HarmonyInstance;
    internal static UnityModManager.ModEntry.ModLogger log;

    public static bool Load(UnityModManager.ModEntry modEntry) {
        log = modEntry.Logger;
#if DEBUG
        modEntry.OnUnload = OnUnload;
#endif
        modEntry.OnGUI = OnGUI;
        HarmonyInstance = new Harmony(modEntry.Info.Id);
        HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
        return true;
    }

    public static void OnGUI(UnityModManager.ModEntry modEntry) {

    }

#if DEBUG
    public static bool OnUnload(UnityModManager.ModEntry modEntry) {
        HarmonyInstance.UnpatchAll(modEntry.Info.Id);
        return true;
    }
#endif
    [HarmonyPatch(typeof(BlueprintsCache))]
    public static class BlueprintsCaches_Patch {
        private static bool Initialized = false;

        [HarmonyPriority(Priority.First)]
        [HarmonyPatch(nameof(BlueprintsCache.Init)), HarmonyPostfix]
        public static void Init_Postfix() {
            try {
                if (Initialized) {
                    log.Log("Already initialized blueprints cache.");
                    return;
                }
                Initialized = true;

                log.Log("Patching blueprints.");
                SoullessGaze.Configure();
                SoullessGaze1.Configure();
                SoullessGaze2.Configure();
                SoullessGaze3.Configure();
                MaskofVirtue.Configure();
                MaskofVirtue1.Configure();
                //mov2
                Fiendskin.Configure();
                Fiendskin1.Configure();
                Fiendskin2.Configure();
                Fiendskin3.Configure();
                Fiendskin4.Configure();
                Maleficium.Configure();
                Maleficium1.Configure();
                Maleficium2.Configure();
                Maleficium3.Configure();
                Maleficium4.Configure();
                // Insert your mod's patching methods here
                // Example
                // SuperAwesomeFeat.Configure()
            } catch (Exception e) {
                log.Log(string.Concat("Failed to initialize.", e));
            }
        }
    }
}
