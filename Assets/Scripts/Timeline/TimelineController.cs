using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineController : MonoBehaviour {

    public List<PlayableDirector> playableDirectors;
    public List<TimelineAsset> timelines;
    public static bool InCutscene { get; private set; } = false;

    public void Play() {
        InCutscene = true;
        foreach (PlayableDirector playableDirector in playableDirectors) {
            playableDirector.Play();
        }
    }

    public void Pause() {
        InCutscene = false;
        foreach (PlayableDirector playableDirector in playableDirectors) {
            playableDirector.Pause();
        }
    }

    public void Resume() {
        InCutscene = true;
        foreach (PlayableDirector playableDirector in playableDirectors) {
            playableDirector.Resume();
        }
    }

    public void PlayFromTimelines(int index) {
        TimelineAsset selectedAsset;

        if (timelines.Count <= index) {
            selectedAsset = timelines[timelines.Count - 1];
        } else {
            selectedAsset = timelines[index];
        }

        playableDirectors[0].Play(selectedAsset);
    }
}