using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

    public static AudioManager instance {get; private set;}
    public int NumCues {get {return _audioCues.Count;}}
    private AudioSource source;
    System.Random random = new System.Random();

    [SerializeField] List<NamedAudioCue> _audioCues = new List<NamedAudioCue>();

    void Awake() {
        instance = this;
        _audioCues.Sort();
        source = GetComponent<AudioSource>();
    }

    public void Play(string cue) {
        int index = _audioCues.BinarySearch(new NamedAudioCue(cue));
        if(index >= 0) {
            PlayCue(_audioCues[index].cue);
        }
    }

    public void PlayCue(AudioCue cue) {
        if(cue.clips.Length > 0) {
            AudioClip clip = cue.clips[random.Next(cue.clips.Length)];
            source.PlayOneShot(clip);
            Debug.Log("Now playing: " + clip);
        }
    }

    [System.Serializable]
    struct NamedAudioCue : IComparable {
        public string name;
        public AudioCue cue;

        public int CompareTo(object obj) {
            return this.name.CompareTo(((NamedAudioCue) obj).name);
        }

        public override bool Equals(System.Object obj) {
            if (obj == null) {
                return false;
            }
            if (obj.GetType().Equals(typeof(string))) {
                return this.name.Equals(obj);
            }
            return base.Equals(obj);
        }

        public NamedAudioCue(string name, AudioCue cue) {
            this.name = name;
            this.cue = cue;
        }

        public NamedAudioCue(string name) {
            this.name = name;
            cue = null;
        }

        public static implicit operator string(NamedAudioCue cue) {
            return cue.name;
        }
    }
}