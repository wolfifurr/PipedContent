using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MetalPipe.Patches
{
    [HarmonyPatch(typeof(PhysicsSound))]
    internal class PhysicsSoundPatches
    {
        [HarmonyPatch(typeof(PhysicsSound), "OnCollisionEnter")]
        [HarmonyPrefix]
        static void OverrideAudioPatch(ref SFX_Instance[] ___impactSounds)
        {
            List<SFX_Instance> sounds = ___impactSounds.ToList();
            SFX_Instance tempSfx = ___impactSounds[0];
            MetalPipe.SFX.ForEach(delegate (AudioClip sound)
            {
                SFX_Instance val = UnityEngine.Object.Instantiate<SFX_Instance>(tempSfx);
                ((UnityEngine.Object)val).name = ((UnityEngine.Object)sound).name;
                val.clips = (AudioClip[])(object)new AudioClip[1] { sound };
                sounds.Add(val);
            });
            ___impactSounds = sounds.ToArray();
        }
    }
}
