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
    public float MusicVolume = 1.0f;

    private AudioClip lastPlaying;
    private MusicState _musicState;
    public MusicState musicState {get {return _musicState;} set {
        if(value == MusicState.Combat && _combatSong != null) {
            lastPlaying = source.clip;
            PlaySong(_combatSong);
        } else if(value == MusicState.Normal && _musicState != MusicState.Normal) {
            PlaySong(lastPlaying);
        }

        _musicState = value;
    }}
    
    [SerializeField] float musicFadeTime = 2.0f;
    [SerializeField] AudioClip _combatSong;
    [SerializeField] List<NamedAudioCue> _audioCues = new List<NamedAudioCue>();
    [SerializeField] List<Song> _songs = new List<Song>();

    void Awake() {
        instance = this;
        _audioCues.Sort();
        _songs.Sort();
        source = GetComponent<AudioSource>();
    }

    public void Play(string cue) {
        int index = _audioCues.BinarySearch(new NamedAudioCue(cue));
        if(index >= 0) {
            PlayCue(_audioCues[index].cue);
        } else {
            Debug.LogWarning("AudioCue " + cue + " not found");
        }
    }

    public void PlayCue(AudioCue cue) {
        if(cue.clips.Length > 0) {
            AudioClip clip = cue.clips[random.Next(cue.clips.Length)];
            source.PlayOneShot(clip);
        }
    }

    public void PlaySong(string song, bool start = true) {
        int index = _songs.BinarySearch(new Song(song));
        if(index >= 0) {
            PlaySong(_songs[index].clip, start);
        } else {
            Debug.LogWarning("Song " + song + " not found");
        }
    }

    public void PlaySong(AudioClip song = null, bool start = true) {
        StartCoroutine(ChangeSong(song, start));
    }

    public void StopSong(string song) {
        PlaySong(song, false);
    }

    public void StopSong(AudioClip nextSong = null) {
        StartCoroutine(ChangeSong(nextSong, false));
    }

    IEnumerator ChangeSong(AudioClip song, bool start = true) {
        float transitionTime = 0;
        if(source.isPlaying) {
            while(transitionTime < musicFadeTime) {
                transitionTime += Time.deltaTime;
                source.volume = Mathf.Lerp(MusicVolume, 0, transitionTime / musicFadeTime);
                yield return null;
            }
        } else {
            source.volume = 0;
        }
        if(song != null) {
            source.clip = song;
        }
        if(start && source.clip != null) {
            source.Play();
            transitionTime = 0;
            while(transitionTime < musicFadeTime) {
                transitionTime += Time.deltaTime;
                source.volume = Mathf.Lerp(0, MusicVolume, transitionTime / musicFadeTime);
                yield return null;
            }
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

    [System.Serializable]
    struct Song : IComparable {
        public string name;
        public AudioClip clip;

        public int CompareTo(object obj) {
            return this.name.CompareTo(((Song) obj).name);
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

        public Song(string name, AudioClip clip) {
            this.name = name;
            this.clip = clip;
        }

        public Song(string name) {
            this.name = name;
            clip = null;
        }

        public static implicit operator string(Song song) {
            return song.name;
        }
    }
}