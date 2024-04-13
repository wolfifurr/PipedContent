using BepInEx;
using BepInEx.Logging;
using MetalPipe.Patches;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

namespace MetalPipe
{



    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInProcess("Content Warning.exe")]
    public class MetalPipe : BaseUnityPlugin
    {
        private const string modGUID = "Wolfifurr.PipedContent";
        private const string modName = "PipedContent By Wolfifurr";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static MetalPipe Instance;

        internal static ManualLogSource mls;

        internal static List<AudioClip> SFX;
        internal static AssetBundle Bundle;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("Test mod has awaken");

            SFX = new List<AudioClip>();
            string FolderLoc = Instance.Info.Location;
            FolderLoc = FolderLoc.TrimEnd("PipedContent.dll".ToCharArray());
            mls.LogMessage(FolderLoc);
            Bundle = AssetBundle.LoadFromFile(FolderLoc + "pipe");
            if (Bundle != null)
            {
                mls.LogMessage("Asset bundle success");
                SFX = Bundle.LoadAllAssets<AudioClip>().ToList();
            }
            else
            {
                mls.LogError("Failed to load asset bundle");
                mls.LogMessage(SFX[0].name);
            }

            harmony.PatchAll(typeof(MetalPipe));
            harmony.PatchAll(typeof(PhysicsSoundPatches));
        }
    }
}
