using CustomJSONData;
using CustomJSONData.CustomBeatmap;
using HarmonyLib;
using UnityEngine;

namespace SoundExtensions.HarmonyPatches
{
    [HarmonyPatch(typeof(NoteCutSoundEffect), "Init")]
    internal static class NoteCutSoundEffectInit
    {
        private static void Prefix(ref AudioClip audioClip, NoteController noteController)
        {
            if (noteController.noteData is CustomNoteData customNoteData)
            {
                var soundId = ((int?)Trees.at(customNoteData.customData, "_soundID")).GetValueOrDefault(-1);

                if (SoundExtensionsController.Instance.Sounds.TryGetValue(soundId, out AudioClip soundClip))
                    audioClip = soundClip;
            }
        }
    }
}