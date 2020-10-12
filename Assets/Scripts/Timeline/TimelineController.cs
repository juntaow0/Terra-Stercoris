using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineController : MonoBehaviour {

    public static event Action<bool> OnTimelineStatus;

    public List<PlayableDirector> playableDirectors;
    public List<TimelineAsset> timelines;
    public bool PlayOnAwake;

    public static bool InCutscene { get; private set; } = false;

    private void Start() {
        if (PlayOnAwake) {
            Play();
        }
    }

    public void Play() {
        InCutscene = true;
        foreach (PlayableDirector playableDirector in playableDirectors) {
            playableDirector.Play();
        }
        OnTimelineStatus?.Invoke(false);
    }

    public void Pause() {
        InCutscene = false;
        foreach (PlayableDirector playableDirector in playableDirectors) {
            playableDirector.Pause();
        }
        OnTimelineStatus?.Invoke(true);
    }

    public void Resume() {
        InCutscene = true;
        foreach (PlayableDirector playableDirector in playableDirectors) {
            playableDirector.Resume();
        }
        OnTimelineStatus?.Invoke(false);
    }

    public void PlayFromTimelines(int index) {
        TimelineAsset selectedAsset;

        if (timelines.Count <= index) {
            selectedAsset = timelines[timelines.Count - 1];
        } else {
            selectedAsset = timelines[index];
        }
        playableDirectors[0].Play(selectedAsset);
        InCutscene = true;
        OnTimelineStatus?.Invoke(false);
    }
}