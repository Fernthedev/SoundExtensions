using HarmonyLib;

namespace SoundExtensions.HarmonyPatches
{
    [HarmonyPatch(typeof(StandardLevelScenesTransitionSetupDataSO), "Init")]
    internal static class StandardLevelScenesTransitionSetupDataSOInit
    {
        private static void Postfix(IDifficultyBeatmap difficultyBeatmap, IPreviewBeatmapLevel previewBeatmapLevel)
        {
            SoundExtensionsController.Instance.Init(difficultyBeatmap, previewBeatmapLevel);
        }
    }
}