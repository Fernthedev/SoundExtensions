using CustomJSONData;
using CustomJSONData.CustomBeatmap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace SoundExtensions
{
    public class SoundExtensionsController : MonoBehaviour
    {
        public static SoundExtensionsController Instance { get; private set; }

        // Alright so like, I know this is dumb. But, I'm lazy and don't want to write my own AudioClip loader. I'm sorry...
        private CachedMediaAsyncLoader _cachedMediaAsyncLoader;

        public Dictionary<int, AudioClip> Sounds { get; private set; } = new Dictionary<int, AudioClip>();

        private void Awake()
        {
            if (Instance != null)
            {
                Plugin.Log?.Warn($"Instance of {GetType().Name} already exists, destroying.");

                DestroyImmediate(this);
                return;
            }

            Plugin.Log?.Debug($"{name}: Awake()");

            DontDestroyOnLoad(this);

            Instance = this;
        }

        public async void Init(IDifficultyBeatmap difficultyBeatmap, IPreviewBeatmapLevel previewBeatmapLevel)
        {
            // Thanks, Auros!
            if (_cachedMediaAsyncLoader == null)
                _cachedMediaAsyncLoader = Resources.FindObjectsOfTypeAll<CachedMediaAsyncLoader>().First(gameObject => gameObject.isActiveAndEnabled);

            var beatmapData = difficultyBeatmap.beatmapData;
            if (difficultyBeatmap.beatmapData is CustomBeatmapData customBeatmapData &&
                previewBeatmapLevel is CustomPreviewBeatmapLevel customPreviewBeatmapLevel)
            {
                var sounds = (List<object>)Trees.at(customBeatmapData.beatmapCustomData, "_sounds");
                if (sounds != null)
                {
                    Sounds.Clear(); // Clear our sounds out, to be safe...

                    Plugin.Log?.Debug($"{name}: Found sounds in {customPreviewBeatmapLevel.songName}, loading {sounds.Count} sounds!");

                    var i = 0;

                    try
                    {
                        List<Task> tasks = new List<>();
                        foreach (var sound in sounds.Cast<string>())
                        {
                            try
                            {
                                var soundPath = Path.Combine(customPreviewBeatmapLevel.customLevelPath, sound);
                                // Is this the best way to do this?
                                var audioClipTask = Task.Run(async () =>
                                {
                                    var audioClip = await _cachedMediaAsyncLoader.LoadAudioClipAsync(soundPath, CancellationToken.None);

                                    Sounds.Add(i, audioClip);

                                    Plugin.Log?.Debug($"{name}: Loaded sound \"{sound}\" {Path.Combine(customPreviewBeatmapLevel.customLevelPath, sound)}");
                                });

                                tasks.Add(audioClipTask);

                            }
                            catch (Exception e)
                            {
                                Plugin.Log?.Error($"{name}: An exception occured while loading sound \"{sound}\": {e}");
                            }

                            i++;
                        }

                        Task.WaitAll(tasks.ToArray());
                    }
                    catch (Exception e)
                    {
                        Plugin.Log?.Error($"{name}: An exception occured while looping through sounds: {e}");
                    }
                }
            }
        }

        private void OnDestroy()
        {
            Plugin.Log?.Debug($"{name}: OnDestroy()");

            if (Instance == this)
                Instance = null;
        }
    }
}
