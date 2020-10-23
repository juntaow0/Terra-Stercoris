using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AudioCue", menuName = "AudioCue")]
public class AudioCue : ScriptableObject {

    public AudioClip[] clips;
}
