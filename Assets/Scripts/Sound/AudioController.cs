using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public void Play(string cue) {
        AudioManager.instance.Play(cue);
    }

    public void PlaySong(string song) {
        AudioManager.instance.PlaySong(song, true);
    }

    public void StopSong() {
        AudioManager.instance.StopSong();
    }

    public void EnterCombat() {
        AudioManager.instance.musicState = MusicState.Combat;
    }

    public void LeaveCombat() {
        AudioManager.instance.musicState = MusicState.Normal;
    }
}
